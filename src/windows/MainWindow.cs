using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ImageConverter {
    public partial class MainWindow : Form {
        private bool backgroundSelected = false;
        private bool glowSelected = false;
        private string pathToBackground = "";
        private string pathToGlow = "";
        private Bitmap backgroundImage = new Bitmap(1, 1);
        private Bitmap glowImage = new Bitmap(1, 1);
        private bool settingsOpen = false;
        private bool generatingXML = false;

        public MainWindow() {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e) {
            float dpi = this.CreateGraphics().DpiX;
            this.Icon = Icon.FromHandle(ImageConverter.Properties.Resources.IconInverted.GetHicon());
            if (!Settings.saveAndLoadPos) this.Location = new Point((int)(Settings.xPos * (dpi / 96)), (int)(Settings.yPos * (dpi / 96)));
            else this.Location = new Point(Settings.xPos, Settings.yPos);
            this.Text = "Image Converter " + Settings.version;

            ModeSelect.SelectedIndex = 0;
        }

        private void OnClose(object sender, FormClosedEventArgs e) {
            Settings.currentMonitor = Convert.ToInt32(Regex.Replace(Screen.FromControl(this).DeviceName, "[^0-9]", ""));
            Settings.xPos = this.Location.X;
            Settings.yPos = this.Location.Y;
            Settings.SaveOnClose();
        }

        private void ModeChanged(object sender, EventArgs e) {
            if(ModeSelect.SelectedIndex != 0 && (backgroundSelected || glowSelected) && !generatingXML) GenerateXML.Enabled = true;
            else GenerateXML.Enabled = false;
            CalculateWidthHeight();
        }

        private void Width_ValueChanged(object sender, EventArgs e) {
            CalculateWidthHeight();
        }

        private void Height_ValueChanged(object sender, EventArgs e) {
            CalculateWidthHeight();
        }

        private void OptimizeCheckChange(object sender, EventArgs e) {
            if (Optimize.Checked) Threshold.Enabled = true;
            else Threshold.Enabled = false;
        }

        private void GlowCheckChanged(object sender, EventArgs e) {
            if(Glow.Checked) {
                Optimize.Enabled = false;
                Optimize.Visible = false;
                OptimizeLabel.Visible = false;
                Threshold.Enabled = false;

                Darken.Enabled = true;
                Darken.Visible = true;
                DarkenLabel.Visible = true;

                SelectGlow.Visible = true;
                SelectBackground.Visible = true;
                SelectFile.Visible = false;
            } else {
                Optimize.Enabled = true;
                Optimize.Visible = true;
                OptimizeLabel.Visible = true;
                if (Optimize.Checked) Threshold.Enabled = true;

                Darken.Enabled = false;
                Darken.Visible = false;
                DarkenLabel.Visible = false;

                SelectGlow.Visible = false;
                SelectBackground.Visible = false;
                SelectFile.Visible = true;
            }
        }

        private void OpenSettingsClick(object sender, EventArgs e) {
            if(!settingsOpen) {
                Settings.xPos = this.Location.X;
                Settings.yPos = this.Location.Y;
                settingsOpen = true;
                Thread t = new Thread(SettingsWindow);
                t.IsBackground = true;
                t.Start();
                void SettingsWindow() {
                    new SettingsWindow().ShowDialog();
                    settingsOpen = false;
                }
            }
        }

        private void SelectFileClick(object sender, EventArgs e) {
            SelectBackgroundFile();
        }

        private void SelectBackgroundClick(object sender, EventArgs e) {
            SelectBackgroundFile();
        }

        private void SelectBackgroundFile() {
            pathToBackground = Util.FileChooser("Image Chooser (Background Image)", "", "All Files (*.*)|*.*|Supported Files (*.PNG;*.JPG;*.BMP;*.GIF;*.TIFF;*.EXIF)|*.PNG;*.JPG;*.BMP;*.GIF;*.TIFF;*.EXIF");
            if (pathToBackground != null) {
                try {
                    backgroundImage = Util.ReadImage(pathToBackground);
                    backgroundSelected = true;
                    SelectFile.ForeColor = Color.White;
                    SelectBackground.ForeColor = Color.White;
                } catch {

                }
            } else { backgroundSelected = false; backgroundImage = new Bitmap(1, 1); if (glowSelected) backgroundImage = new Bitmap(glowImage); 
                SelectFile.ForeColor = Color.Gainsboro;
                SelectBackground.ForeColor = Color.Gainsboro;
            }
            CalculateWidthHeight();
            if ((backgroundSelected || (glowSelected && Glow.Checked)) && ModeSelect.SelectedIndex != 0) GenerateXML.Enabled = true;
            else GenerateXML.Enabled = false;
        }

        private void SelectGlowClick(object sender, EventArgs e) {
            pathToGlow = Util.FileChooser("Image Chooser (Glow Image)", "", "All Files (*.*)|*.*|Supported Files (*.PNG;*.JPG;*.BMP;*.GIF;*.TIFF;*.EXIF)|*.PNG;*.JPG;*.BMP;*.GIF;*.TIFF;*.EXIF");
            if (pathToGlow != null) {
                try {
                    glowImage = Util.ReadImage(pathToGlow);
                    glowSelected = true;
                    SelectGlow.ForeColor = Color.White;
                } catch {

                }
            } else { glowSelected = false; glowImage = new Bitmap(1, 1); SelectGlow.ForeColor = Color.Gainsboro; }
            if (glowSelected && !backgroundSelected) backgroundImage = new Bitmap(glowImage);
            CalculateWidthHeight();
            if ((backgroundSelected || (glowSelected && Glow.Checked)) && ModeSelect.SelectedIndex != 0) GenerateXML.Enabled = true;
            else GenerateXML.Enabled = false;
        }

        private void GenerateClick(object sender, EventArgs e) {
            GenerateXML.Enabled = false;
            GenerateXML.Text = "Generating...";
            generatingXML = true;

            bool glow = Glow.Checked;
            bool optimize = Optimize.Checked;
            int optimizationThreshold = (int)Threshold.Value;
            string path = Settings.vehicleFolderPath + Settings.vehicleOutputName;
            if (Settings.useImageNameAsVehicleName && backgroundSelected) {
                path = Settings.vehicleFolderPath + Util.FileNameFromPath(pathToBackground) + ".xml";
            } else if (Settings.useImageNameAsVehicleName) {
                path = Settings.vehicleFolderPath + Util.FileNameFromPath(pathToGlow) + ".xml";
            }
            if (Settings.doBackups) Backups.AddBackup(path);

            if (File.Exists(pathToBackground) && backgroundSelected) backgroundImage = Util.ReadImage(pathToBackground);
            if (File.Exists(pathToGlow) && glowSelected) glowImage = Util.ReadImage(pathToGlow);
            if (!backgroundSelected) backgroundImage = new Bitmap(glowImage);
            CalculateWidthHeight();

            int newY = (int)Math.Max((((double)backgroundImage.Height / (double)backgroundImage.Width) * (double)Width.Value * 9),1);
            int newX = (int)Math.Max((((double)backgroundImage.Width / (double)backgroundImage.Height) * (double)Height.Value * 9),1);

            Bitmap bgResized = new Bitmap(1, 1);
            Bitmap gResized = new Bitmap(1, 1);
            Bitmap bgBitmap = new Bitmap(1, 1);
            Bitmap gBitmap = new Bitmap(1, 1);

            if (ModeSelect.SelectedIndex == 1) {
                bgResized = new Bitmap(backgroundImage, (int)Width.Value * 9, (int)Height.Value * 9);
                if(glow) gResized = new Bitmap(glowImage, bgResized.Width, bgResized.Height);
            } else if (ModeSelect.SelectedIndex == 2) {
                bgResized = new Bitmap(backgroundImage, (int)Width.Value * 9, newY);
                if (glow) gResized = new Bitmap(glowImage, bgResized.Width, bgResized.Height);
            } else if (ModeSelect.SelectedIndex == 3) {
                bgResized = new Bitmap(backgroundImage, newX, (int)Height.Value * 9);
                if (glow) gResized = new Bitmap(glowImage, bgResized.Width, bgResized.Height);
            } else if (ModeSelect.SelectedIndex == 4) {
                bgResized = new Bitmap(backgroundImage);
                if (glow) gResized = new Bitmap(glowImage, bgResized.Width, bgResized.Height);
            }

            bgBitmap = new Bitmap((int)(Math.Ceiling((double)bgResized.Width / 9) * 9), (int)(Math.Ceiling((double)bgResized.Height / 9) * 9));
            Graphics gBackground = Graphics.FromImage(bgBitmap);
            if(backgroundSelected) gBackground.FillRectangle(new SolidBrush(Color.White), new RectangleF(0, 0, bgResized.Width, bgResized.Height));
            gBackground.DrawImage(bgResized, 0, 0, bgResized.Width, bgResized.Height);

            if(glow) {
                gBitmap = new Bitmap(bgBitmap.Width, bgBitmap.Height);
                Graphics gGlow = Graphics.FromImage(gBitmap);
                gGlow.DrawImage(gResized, 0, 0, gResized.Width, gResized.Height);
            }
            if (!backgroundSelected) { gBitmap = new Bitmap(bgBitmap); bgBitmap = new Bitmap(1, 1); }

            ThreadStart starter = Generate;
            starter += () => {
                Invoke(new Action(() => { GenerateXML.Enabled = true; GenerateXML.Text = "Generate XML"; generatingXML = false; }));
            };
            Thread t = new Thread(starter);
            t.IsBackground = true;
            t.Start();
            void Generate() {
                if (!glow) Util.SaveTextFile(Generator.GenerateXML(bgBitmap, optimize, optimizationThreshold), path);
                else Util.SaveTextFile(Generator.GenerateXML(bgBitmap, gBitmap, Darken.Checked, backgroundSelected), path);
            }
        }

        private void CalculateWidthHeight() {
            if (backgroundSelected || glowSelected) {
                if(ModeSelect.SelectedIndex==0) {
                    Width.Enabled = false;
                    Height.Enabled = false;
                } else if(ModeSelect.SelectedIndex==1) {
                    Width.Enabled = true;
                    Height.Enabled = true;
                } else if(ModeSelect.SelectedIndex==2) {
                    Width.Enabled = true;
                    Height.Enabled = false;
                    double newHeight = ((double)backgroundImage.Height / (double)backgroundImage.Width) * (double)Width.Value * 9;
                    Height.Value = Math.Max((int)Math.Ceiling(Math.Floor(newHeight) / 9),1);
                } else if(ModeSelect.SelectedIndex==3) {
                    Width.Enabled = false;
                    Height.Enabled = true;
                    double newWidth = ((double)backgroundImage.Width / (double)backgroundImage.Height) * (double)Height.Value * 9;
                    Width.Value = Math.Max((int)Math.Ceiling(Math.Floor(newWidth) / 9),1);
                } else if(ModeSelect.SelectedIndex==4) {
                    Width.Enabled = false;
                    Height.Enabled = false;
                    Width.Value = (int)Math.Ceiling((double)backgroundImage.Width / 9);
                    Height.Value = (int)Math.Ceiling((double)backgroundImage.Height / 9);
                }
            } else {
                Width.Enabled = false;
                Height.Enabled = false;
                Width.Value = 1;
                Height.Value = 1;
            }
        }

        private void WidthKeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) e.SuppressKeyPress = true;
        }

        private void HeightKeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) e.SuppressKeyPress = true;
        }

        private void ThresholdKeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) e.SuppressKeyPress = true;
        }
    }
}