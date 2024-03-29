﻿using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

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

        public int modeSelectIndex { get; private set; }
        public int newWidth { private get; set; }
        public int newHeight { private get; set; }

        public MainWindow() {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e) {
            Text = "Image Converter " + Settings.version;

            Icon = Icon.FromHandle(Properties.Resources.IconInverted.GetHicon());
            
            if (!Settings.saveAndLoadPos) {
                Location = new Point(Settings.xPos, Settings.yPos);
                Screen s = Screen.FromPoint(Location);
                Rectangle workingArea = s.WorkingArea;
                Location = new Point() {
                    X = Math.Max(workingArea.X, workingArea.X + (workingArea.Width - Width) / 2),
                    Y = Math.Max(workingArea.Y, workingArea.Y + (workingArea.Height - Height) / 2)
                };
            } else {
                Location = new Point(Settings.xPos, Settings.yPos);
            }
            
            KeyPreview = true;
            ModeSelect.SelectedIndex = 0;

            if(CreateGraphics().DpiX == 96) {
                CutoutLabel.Text = "Cutout Background?";
            }
        }

        private void OnClose(object sender, FormClosedEventArgs e) {
            int currentMonitor = Array.IndexOf(Screen.AllScreens, Screen.FromControl(this)) + 1;
            if (currentMonitor == 0) { // (check for 0 instead of -1 because 1 is added to the index on the line before)
                currentMonitor = 1;
            }

            Settings.currentMonitor = currentMonitor;
            Settings.xPos = Location.X;
            Settings.yPos = Location.Y;
            Settings.SaveOnClose();
        }

        private void ModeChanged(object sender, EventArgs e) {
            GenerateXML.Enabled = ModeSelect.SelectedIndex != 0 && (backgroundSelected || glowSelected) && !generatingXML;
            Ruler.Enabled = (backgroundSelected || (glowSelected && Glow.Checked)) && ModeSelect.SelectedIndex != 0 && ModeSelect.SelectedIndex != 1 && ModeSelect.SelectedIndex != 4;
            modeSelectIndex = ModeSelect.SelectedIndex;
            CalculateWidthHeight();
        }

        private void Width_ValueChanged(object sender, EventArgs e) {
            CalculateWidthHeight();
        }

        private void Height_ValueChanged(object sender, EventArgs e) {
            CalculateWidthHeight();
        }

        private void OptimizeCheckChange(object sender, EventArgs e) {
            Threshold.Enabled = Optimize.Checked;
            CutoutBackground.Enabled = Optimize.Checked;
        }

        private void GlowCheckChanged(object sender, EventArgs e) {
            if(Glow.Checked) {
                Optimize.Enabled = false;
                Optimize.Visible = false;
                OptimizeLabel.Visible = false;
                Threshold.Enabled = false;
                CutoutBackground.Enabled = false;

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
                if (Optimize.Checked) {
                    Threshold.Enabled = true;
                    CutoutBackground.Enabled = true;
                }

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
                Settings.xPos = Location.X;
                Settings.yPos = Location.Y;
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
                    UnsupportedFileChoosen(pathToBackground);
                }
            } else { backgroundSelected = false; backgroundImage = new Bitmap(1, 1); if (glowSelected) backgroundImage = new Bitmap(glowImage); 
                SelectFile.ForeColor = Color.Gainsboro;
                SelectBackground.ForeColor = Color.Gainsboro;
            }
            CalculateWidthHeight();
            GenerateXML.Enabled = (backgroundSelected || (glowSelected && Glow.Checked)) && ModeSelect.SelectedIndex != 0;
            Ruler.Enabled = (backgroundSelected || (glowSelected && Glow.Checked)) && ModeSelect.SelectedIndex != 0 && ModeSelect.SelectedIndex != 1 && ModeSelect.SelectedIndex != 4;
        }

        private void SelectGlowClick(object sender, EventArgs e) {
            pathToGlow = Util.FileChooser("Image Chooser (Glow Image)", "", "All Files (*.*)|*.*|Supported Files (*.PNG;*.JPG;*.BMP;*.GIF;*.TIFF;*.EXIF)|*.PNG;*.JPG;*.BMP;*.GIF;*.TIFF;*.EXIF");
            if (pathToGlow != null) {
                try {
                    glowImage = Util.ReadImage(pathToGlow);
                    glowSelected = true;
                    SelectGlow.ForeColor = Color.White;
                } catch {
                    UnsupportedFileChoosen(pathToGlow);
                }
            } else { glowSelected = false; glowImage = new Bitmap(1, 1); SelectGlow.ForeColor = Color.Gainsboro; }
            if (glowSelected && !backgroundSelected) backgroundImage = new Bitmap(glowImage);
            CalculateWidthHeight();
            GenerateXML.Enabled = (backgroundSelected || (glowSelected && Glow.Checked)) && ModeSelect.SelectedIndex != 0;
            Ruler.Enabled = (backgroundSelected || (glowSelected && Glow.Checked)) && ModeSelect.SelectedIndex != 0 && ModeSelect.SelectedIndex != 1 && ModeSelect.SelectedIndex != 4;
        }

        private void UnsupportedFileChoosen(string path) {
            MessageBox.Show($"The file \"{path}\" is using a file format unsupported by the image converter. The supported formats are JPG, PNG, BMP, GIF (not animated), EXIF, and TIFF. If the image is one of those, it isn't saved under the format properly (for example, renaming the file extension doesn't work).\n\nTo fix the problem open the image in any image editing program of your choice (Paint or Paint 3D will work), and save the image with one of the supported formats.", "Unsupported File");
        }

        private void GenerateClick(object sender, EventArgs e) {
            GenerateXML.Enabled = false;
            GenerateXML.Text = "Generating...";
            generatingXML = true;

            bool glow = Glow.Checked;
            bool optimize = Optimize.Checked;
            bool cutout = CutoutBackground.Checked;
            int optimizationThreshold = (int)Threshold.Value;
            string path = Settings.vehicleFolderPath + Settings.vehicleOutputName;
            if (!optimize) cutout = false;
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

            int newY = (int)Math.Max(((double)backgroundImage.Height / backgroundImage.Width) * (double)WidthUDC.Value * 9, 1);
            int newX = (int)Math.Max(((double)backgroundImage.Width / backgroundImage.Height) * (double)HeightUDC.Value * 9, 1);

            Bitmap bgResized = null;
            Bitmap gResized = null;
            Bitmap bgBitmap = null;
            Bitmap gBitmap = null;

            if (ModeSelect.SelectedIndex == 1) {
                bgResized = new Bitmap(backgroundImage, (int)WidthUDC.Value * 9, (int)HeightUDC.Value * 9);
                if(glow) gResized = new Bitmap(glowImage, bgResized.Width, bgResized.Height);
            } else if (ModeSelect.SelectedIndex == 2) {
                bgResized = new Bitmap(backgroundImage, (int)WidthUDC.Value * 9, newY);
                if (glow) gResized = new Bitmap(glowImage, bgResized.Width, bgResized.Height);
            } else if (ModeSelect.SelectedIndex == 3) {
                bgResized = new Bitmap(backgroundImage, newX, (int)HeightUDC.Value * 9);
                if (glow) gResized = new Bitmap(glowImage, bgResized.Width, bgResized.Height);
            } else if (ModeSelect.SelectedIndex == 4) {
                bgResized = new Bitmap(backgroundImage);
                if (glow) gResized = new Bitmap(glowImage, bgResized.Width, bgResized.Height);
            }

            //offseting image
            int xOff = (int)(XOffset.Value + 9) % 9;
            int yOff = (int)(YOffset.Value + 9) % 9;

            bgBitmap = new Bitmap((int)(Math.Ceiling((double)(bgResized.Width + xOff) / 9) * 9), (int)(Math.Ceiling((double)(bgResized.Height + yOff) / 9) * 9));

            Graphics gBackground = Graphics.FromImage(bgBitmap);
            if(backgroundSelected) gBackground.FillRectangle(new SolidBrush(Color.White), new RectangleF(xOff, yOff, bgResized.Width, bgResized.Height));
            gBackground.DrawImage(bgResized, xOff, yOff, bgResized.Width, bgResized.Height);

            if(glow) {
                gBitmap = new Bitmap(bgBitmap.Width, bgBitmap.Height);
                Graphics gGlow = Graphics.FromImage(gBitmap);
                gGlow.DrawImage(gResized, xOff, yOff, gResized.Width, gResized.Height);
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
                if (!glow) {
                    //size of black borders around image
                    int widthDifference = bgBitmap.Width - bgResized.Width;
                    int heightDifference = bgBitmap.Height - bgResized.Height;
                    Util.SaveTextFile(
                        new Generator().GeneratePaintableSign(bgBitmap, (double)PixelContrastUDC.Value, optimize, optimizationThreshold, cutout,
                            heightDifference - yOff, yOff,
                            widthDifference - xOff, xOff
                        ),
                        path
                    );
                } else {
                    Util.SaveTextFile(new Generator().GeneratePaintableIndicator(bgBitmap, gBitmap, (double)PixelContrastUDC.Value, Darken.Checked, backgroundSelected), path);
                }

                if(File.Exists(Settings.thumbnailPath)) {
                    string thumbnailPath = path.Substring(0, path.Length - 4) + ".png";
                    File.Copy(Settings.thumbnailPath, thumbnailPath, true);
                }
                
                bgResized = null;
                gResized = null;
                bgBitmap = null;
                gBitmap = null;
                //hopefully cleans up (up to) hundreds of megabytes of data
                GC.Collect();
            }
        }

        private void RulerClick(object sender, EventArgs e) {
            newWidth = 0;
            newHeight = 0;
            
            new ScaleBySegment(this, backgroundImage).ShowDialog();
            
            if(ModeSelect.SelectedIndex == 2) {
                if (newWidth == 0) {
                    return;
                }
                WidthUDC.Value = newWidth;
            } else if(ModeSelect.SelectedIndex == 3) {
                if(newHeight == 0) {
                    return;
                }
                HeightUDC.Value = newHeight;
            }
            
            CalculateWidthHeight();
        }

        private void CalculateWidthHeight() {
            if (backgroundSelected || glowSelected) {
                if(ModeSelect.SelectedIndex==0) {
                    WidthUDC.Enabled = false;
                    HeightUDC.Enabled = false;
                } else if(ModeSelect.SelectedIndex==1) {
                    WidthUDC.Enabled = true;
                    HeightUDC.Enabled = true;
                } else if(ModeSelect.SelectedIndex==2) {
                    WidthUDC.Enabled = true;
                    HeightUDC.Enabled = false;
                    double newHeight = ((double)backgroundImage.Height / (double)backgroundImage.Width) * (double)WidthUDC.Value * 9;
                    HeightUDC.Value = Math.Max((int)Math.Ceiling(Math.Floor(newHeight) / 9), 1);
                } else if(ModeSelect.SelectedIndex==3) {
                    WidthUDC.Enabled = false;
                    HeightUDC.Enabled = true;
                    double newWidth = ((double)backgroundImage.Width / (double)backgroundImage.Height) * (double)HeightUDC.Value * 9;
                    WidthUDC.Value = Math.Max((int)Math.Ceiling(Math.Floor(newWidth) / 9), 1);
                } else if(ModeSelect.SelectedIndex==4) {
                    WidthUDC.Enabled = false;
                    HeightUDC.Enabled = false;
                    WidthUDC.Value = (int)Math.Ceiling((double)backgroundImage.Width / 9);
                    HeightUDC.Value = (int)Math.Ceiling((double)backgroundImage.Height / 9);
                }
            } else {
                WidthUDC.Enabled = false;
                HeightUDC.Enabled = false;
                WidthUDC.Value = 1;
                HeightUDC.Value = 1;
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

        private void XOffsetKeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) e.SuppressKeyPress = true;
        }

        private void YOffsetKeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) e.SuppressKeyPress = true;
        }

        private void ContrastKeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) e.SuppressKeyPress = true;
        }

        private Keys[] keys = new Keys[10];
        private void Key_Down(object sender, KeyEventArgs e) {
            keys[0] = keys[1];
            keys[1] = keys[2];
            keys[2] = keys[3];
            keys[3] = keys[4];
            keys[4] = keys[5];
            keys[5] = keys[6];
            keys[6] = keys[7];
            keys[7] = keys[8];
            keys[8] = keys[9];
            keys[9] = e.KeyCode;
            if (
                keys[0] == Keys.Up &&
                keys[1] == Keys.Up &&
                keys[2] == Keys.Down &&
                keys[3] == Keys.Down &&
                keys[4] == Keys.Left &&
                keys[5] == Keys.Right &&
                keys[6] == Keys.Left &&
                keys[7] == Keys.Right &&
                keys[8] == Keys.A &&
                keys[9] == Keys.B
                ) ASCIIArtEasterEgg();
        }

        private static bool firstTime = true;

        private static void ASCIIArtEasterEgg() {
            if (firstTime) {
                firstTime = false;
                AllocConsole();
                ShowWindow(GetConsoleWindow(), 3);
                Console.Title = "...";
                string text = Unzip(new byte[] { 0x1f, 0x8b, 0x8, 0x0, 0x0, 0x0, 0x0, 0x0, 0x4, 0x0, 0xcd, 0x59, 0x4d, 0x6f, 0xdb, 0x38, 0x10, 0xbd, 0x1b, 0xf0, 0x7f, 0x98, 0x40, 0x5a, 0x6c, 0x6b, 0xc8, 0x52, 0xf7, 0x2a, 0xd5, 0x40, 0x8a, 0x3d, 0xe5, 0xb2, 0x97, 0x5e, 0x5, 0x68, 0x9d, 0x56, 0x1b, 0xb7, 0x48, 0xac, 0x85, 0x3f, 0x1a, 0x4, 0x10, 0xfc, 0xdb, 0x97, 0x8f, 0x23, 0x8a, 0xa4, 0x4c, 0xc9, 0xfa, 0x70, 0xeb, 0x65, 0x62, 0x87, 0xe4, 0xcc, 0xbc, 0xf7, 0x86, 0x33, 0xa2, 0xd1, 0x9a, 0xb2, 0x2c, 0x23, 0xca, 0x30, 0xf8, 0x5d, 0xac, 0xaa, 0x49, 0x6d, 0x30, 0x67, 0xa4, 0x47, 0x87, 0x57, 0x5, 0x52, 0xfb, 0x91, 0xb5, 0xdd, 0xf0, 0xb5, 0xc0, 0xd, 0x0, 0x4d, 0x63, 0x4c, 0x4c, 0xa3, 0x93, 0x96, 0x68, 0x3e, 0x2b, 0x53, 0xa2, 0x14, 0x6f, 0x94, 0x51, 0x8a, 0xb7, 0x7a, 0x99, 0xe9, 0x59, 0x96, 0xc9, 0x19, 0xa2, 0xd2, 0x9a, 0xcc, 0xb6, 0xd9, 0xfe, 0xd5, 0x54, 0x3a, 0x47, 0xe2, 0x57, 0x47, 0x9b, 0xbe, 0x75, 0x1a, 0xa9, 0xc3, 0x6c, 0xd2, 0x68, 0x9c, 0xc8, 0x30, 0x4a, 0x0, 0x93, 0xb6, 0x32, 0xcc, 0x67, 0x29, 0xc9, 0x20, 0xbc, 0xa5, 0xc2, 0xc9, 0x58, 0x96, 0xf5, 0x4c, 0x44, 0x96, 0x72, 0x26, 0x2, 0xa3, 0x52, 0xb1, 0x35, 0x6c, 0x86, 0xbf, 0x1, 0xc2, 0x5a, 0xa8, 0xac, 0xa3, 0xd, 0xdf, 0xb2, 0x3a, 0xa4, 0x34, 0x4b, 0x5d, 0x66, 0x8b, 0xa6, 0xc2, 0x89, 0xd2, 0x28, 0xd3, 0x46, 0x89, 0x80, 0x7d, 0x3b, 0x6a, 0x3e, 0x23, 0x33, 0x2b, 0xe1, 0x54, 0xd6, 0xcb, 0x2a, 0x75, 0xf6, 0x54, 0xe9, 0x66, 0x65, 0x54, 0x57, 0x5f, 0xd9, 0xf4, 0xa1, 0x38, 0xb2, 0x62, 0x2d, 0xa4, 0xa3, 0x2b, 0x6c, 0xf1, 0x9b, 0x6a, 0x8c, 0x36, 0xb3, 0x6, 0xaf, 0x72, 0x2a, 0x23, 0x83, 0x56, 0xd5, 0x4f, 0x65, 0xc5, 0x2, 0x90, 0x14, 0x59, 0xe7, 0x6a, 0x2f, 0x5d, 0x55, 0xab, 0x6a, 0x66, 0xd5, 0x2a, 0x6b, 0xcf, 0x8a, 0x73, 0xb2, 0xa3, 0xd9, 0x43, 0xa0, 0x9e, 0x67, 0xd5, 0x34, 0xd7, 0x34, 0xa, 0x47, 0x87, 0xc8, 0xce, 0xe3, 0x9c, 0x6c, 0x1, 0x48, 0xa, 0x4e, 0x72, 0x37, 0xcb, 0x54, 0x56, 0xf5, 0x52, 0xbd, 0x65, 0x95, 0xf2, 0x7a, 0x56, 0xc3, 0x3b, 0x6c, 0xa9, 0x7c, 0x48, 0x54, 0xa8, 0xd2, 0x72, 0xee, 0x61, 0xd3, 0xb5, 0x98, 0x6b, 0x97, 0x3a, 0x27, 0x15, 0x82, 0x57, 0x24, 0xf7, 0x6b, 0x42, 0x8e, 0x97, 0x49, 0xc9, 0xde, 0x2c, 0xb9, 0xf7, 0xec, 0x65, 0xfd, 0x26, 0x87, 0x31, 0xab, 0xe1, 0x1d, 0xb6, 0xaa, 0x89, 0xeb, 0xf8, 0xc8, 0xe9, 0xab, 0xf9, 0xaa, 0x49, 0x9b, 0xb9, 0x76, 0xa9, 0x73, 0xe2, 0xb5, 0xdc, 0x93, 0xfb, 0x9a, 0x90, 0xe3, 0x65, 0x52, 0xcd, 0x3b, 0xd4, 0x3d, 0xb4, 0x87, 0x75, 0x5f, 0x53, 0x46, 0xd5, 0x1d, 0xc6, 0x86, 0xa, 0xef, 0x3c, 0xca, 0x82, 0x2, 0x2f, 0x2e, 0x32, 0xd9, 0x3a, 0xfa, 0x4e, 0x6, 0x98, 0x42, 0xa8, 0x36, 0xa5, 0x87, 0xa4, 0x7b, 0x97, 0xbd, 0x47, 0xc, 0xbd, 0x97, 0x6b, 0xb9, 0xe2, 0xfa, 0x95, 0xe2, 0x47, 0xdc, 0x5f, 0x11, 0x37, 0xa8, 0x84, 0x51, 0xb8, 0xd6, 0xa7, 0x95, 0xd8, 0x14, 0xbc, 0xe8, 0x4, 0xa6, 0xce, 0xd4, 0x84, 0x7e, 0xc7, 0xbd, 0x8b, 0xab, 0xd5, 0xda, 0xcc, 0xa2, 0x77, 0xd9, 0xf2, 0x23, 0x4, 0x61, 0x1b, 0xe8, 0x51, 0x45, 0xc2, 0x7d, 0x2f, 0x78, 0x23, 0x32, 0x82, 0xe8, 0x6f, 0x8e, 0x5c, 0xa, 0x69, 0x72, 0x1b, 0x13, 0xb1, 0x91, 0x9, 0x5a, 0xc8, 0x41, 0x47, 0xe1, 0x85, 0x1d, 0xa1, 0x8f, 0x7f, 0xb2, 0xc0, 0xde, 0x4c, 0x65, 0xfb, 0xa9, 0xf4, 0xa4, 0x2d, 0x60, 0x57, 0x39, 0x11, 0x62, 0x4a, 0x5c, 0xa8, 0xd2, 0x5f, 0x6d, 0x56, 0xb8, 0x51, 0xa9, 0x48, 0x84, 0x9f, 0x0, 0x98, 0xcf, 0xe6, 0xb3, 0xbf, 0x8a, 0x3, 0xed, 0x8f, 0xbb, 0x9c, 0x5e, 0x37, 0x6f, 0xf4, 0x56, 0x1c, 0xe9, 0xb0, 0x29, 0x8e, 0x4f, 0x9b, 0x83, 0xf8, 0xbb, 0x3e, 0xd0, 0x9a, 0xfe, 0xdd, 0x15, 0x4f, 0xbb, 0xf5, 0x8b, 0x58, 0x7e, 0xdb, 0xd3, 0xfe, 0x65, 0xfd, 0xfc, 0x4c, 0xaf, 0xc5, 0xf1, 0xf9, 0x2b, 0x6d, 0xd6, 0x3f, 0x72, 0x5a, 0x6f, 0x29, 0x5f, 0xef, 0xf, 0xf9, 0x8e, 0xf2, 0xa7, 0xa7, 0x80, 0x1e, 0x8f, 0x7, 0xda, 0xe4, 0x0, 0x13, 0xa6, 0x5d, 0x1e, 0xce, 0x67, 0x9f, 0x9e, 0x19, 0x4f, 0xc4, 0x63, 0x5f, 0x60, 0x8, 0xc0, 0xc7, 0xf5, 0xe3, 0xf3, 0x9b, 0xc0, 0xfe, 0xe3, 0xc3, 0x87, 0xdf, 0xe8, 0xcb, 0x66, 0xbd, 0xfd, 0x92, 0x33, 0xdd, 0x3, 0x1d, 0xa, 0x1, 0xbd, 0x2f, 0x5e, 0xf2, 0x62, 0x5b, 0xed, 0x89, 0x40, 0xda, 0xe6, 0xaf, 0xf4, 0x23, 0xdf, 0xed, 0xbf, 0x15, 0x5b, 0x2a, 0xfe, 0x61, 0x2d, 0x9b, 0xf5, 0x57, 0x83, 0x7b, 0x2f, 0xb8, 0x3e, 0x17, 0x74, 0xdc, 0x84, 0x61, 0x48, 0xf9, 0xf6, 0x7b, 0xf1, 0x26, 0x3, 0x3f, 0x7d, 0xfe, 0xf3, 0xe1, 0x41, 0x48, 0x1, 0xf4, 0xd3, 0x31, 0xdf, 0xef, 0x29, 0x96, 0x59, 0x5f, 0xea, 0x65, 0xf7, 0xb8, 0x77, 0x8c, 0x71, 0x48, 0xc3, 0x14, 0xb8, 0x78, 0xa7, 0x6a, 0xe9, 0xaf, 0xa0, 0x2f, 0xfb, 0x50, 0x15, 0x7d, 0x14, 0xc, 0xe5, 0x1e, 0xa6, 0xe3, 0xb2, 0x82, 0x29, 0xfc, 0x7d, 0x34, 0x74, 0x2b, 0x98, 0xca, 0xde, 0x47, 0xc5, 0x7c, 0xd6, 0x6e, 0xbf, 0x16, 0x7f, 0x97, 0x86, 0xfb, 0x7b, 0x28, 0x70, 0xf9, 0x5c, 0x93, 0xbd, 0x9b, 0x41, 0x2b, 0x30, 0xbd, 0x7e, 0x6, 0xbf, 0x1b, 0x9d, 0xfb, 0xe0, 0x67, 0xf1, 0xf5, 0x19, 0xaa, 0x13, 0x6f, 0xc7, 0xaf, 0x9f, 0x85, 0x5b, 0xf1, 0x9b, 0x4f, 0xe3, 0x6d, 0xf8, 0xed, 0xfb, 0xe0, 0x16, 0xfc, 0xcd, 0x1b, 0x69, 0x1c, 0xc6, 0xb4, 0xd8, 0x29, 0x67, 0x30, 0xe5, 0xfe, 0x70, 0x29, 0x18, 0x8a, 0x31, 0x4d, 0x81, 0xab, 0xf, 0x6e, 0xad, 0x60, 0x1a, 0xff, 0x14, 0x84, 0x29, 0xf7, 0xc1, 0x34, 0x7e, 0x5b, 0xc1, 0x98, 0xf8, 0xe9, 0xa, 0xcc, 0x3b, 0x71, 0x5c, 0xbc, 0xd6, 0x30, 0x25, 0xbe, 0xfd, 0x93, 0xa9, 0x1f, 0x6e, 0x5f, 0xfe, 0x76, 0x96, 0x29, 0x9f, 0x8d, 0xe4, 0x9c, 0xe, 0xc4, 0xf8, 0x9f, 0x7c, 0x3a, 0xb7, 0x5b, 0xfb, 0x20, 0xf4, 0xf3, 0x1a, 0xa3, 0xa0, 0x67, 0x8f, 0xf5, 0x72, 0xe9, 0x56, 0xd0, 0x15, 0x37, 0xa5, 0xcf, 0xfb, 0xe2, 0x5c, 0x56, 0x70, 0x3e, 0xba, 0xf9, 0xda, 0x22, 0x7f, 0xa5, 0x2, 0x77, 0xfc, 0xf5, 0x14, 0x74, 0xf1, 0xdb, 0x6d, 0x71, 0x4d, 0x5, 0xe6, 0x4e, 0xb7, 0x82, 0x76, 0xd, 0xd7, 0x52, 0x70, 0x89, 0xdf, 0xf6, 0xfb, 0x15, 0xa, 0x2e, 0xed, 0x5d, 0x5b, 0x81, 0xdb, 0x7a, 0xce, 0xe1, 0xd2, 0x30, 0x4e, 0x41, 0x9b, 0xed, 0x92, 0x2, 0x97, 0xe7, 0x18, 0x5, 0xed, 0xfc, 0x43, 0x47, 0xb7, 0x82, 0x69, 0x9f, 0xce, 0x7d, 0x15, 0xc, 0xe7, 0xbf, 0x26, 0xfb, 0x38, 0xfe, 0x66, 0xf7, 0x35, 0xff, 0xcd, 0xdd, 0xde, 0x1, 0xe7, 0x5d, 0x38, 0x86, 0xbf, 0xd9, 0x3f, 0xae, 0xbe, 0x6b, 0x1f, 0x7d, 0xd8, 0x99, 0xbf, 0xeb, 0x4, 0xc6, 0xf2, 0xf7, 0xcf, 0xfe, 0x72, 0xfe, 0xc3, 0xd8, 0xdd, 0xd1, 0xb7, 0x61, 0x6f, 0xc7, 0x56, 0xec, 0x43, 0xef, 0x99, 0x6b, 0x66, 0xee, 0xf6, 0x58, 0x2c, 0x56, 0xab, 0xa1, 0xec, 0x4d, 0xc5, 0x98, 0xaf, 0x56, 0xbe, 0xdf, 0xc5, 0x7d, 0xce, 0x7e, 0x77, 0x47, 0x14, 0x86, 0xe3, 0x72, 0x37, 0x31, 0x7d, 0x1f, 0x38, 0x8b, 0x45, 0x5f, 0xe6, 0x24, 0x21, 0xa, 0x2, 0xdf, 0xbf, 0xbb, 0x1b, 0xcf, 0xad, 0xce, 0xce, 0xf3, 0xc2, 0x90, 0x88, 0x4f, 0xd0, 0xf7, 0x83, 0x80, 0x31, 0xdd, 0xff, 0x23, 0x71, 0x3a, 0x11, 0x2d, 0x97, 0xc8, 0x1b, 0xef, 0x63, 0xb8, 0x35, 0x56, 0x18, 0xfa, 0x3e, 0xf8, 0x88, 0xe2, 0xf8, 0xfe, 0x1e, 0x7f, 0x91, 0xff, 0x39, 0xaf, 0xf6, 0xf1, 0xfd, 0x30, 0xc, 0x2, 0xf3, 0x1c, 0x86, 0xb3, 0x22, 0x86, 0xb5, 0x23, 0x17, 0x64, 0x7e, 0x9e, 0x2f, 0xfb, 0xc7, 0xb1, 0x3a, 0x95, 0xe5, 0x12, 0xa7, 0xdd, 0xac, 0xcd, 0x10, 0x4e, 0xae, 0xb0, 0xc2, 0x43, 0xf5, 0x92, 0xe4, 0x9c, 0x95, 0x63, 0xb8, 0xb, 0x56, 0x2b, 0xac, 0x35, 0xab, 0xae, 0x75, 0x1b, 0xb7, 0xb6, 0xea, 0x3e, 0x62, 0x56, 0x22, 0xee, 0x6b, 0xcf, 0x73, 0x71, 0xea, 0x73, 0x6, 0xdf, 0xe9, 0xc4, 0xfd, 0x8c, 0xa, 0xab, 0xb3, 0x71, 0x3f, 0x15, 0x9a, 0xed, 0x74, 0xc2, 0x69, 0x86, 0x61, 0x1c, 0x83, 0x1, 0x38, 0x38, 0x3b, 0xdd, 0x21, 0xed, 0x9c, 0xe0, 0xf0, 0x3c, 0x54, 0x2, 0x2f, 0x8c, 0xd3, 0xc9, 0xf7, 0xd5, 0x9c, 0xcf, 0xc8, 0x54, 0xe0, 0x79, 0x49, 0x82, 0x79, 0x10, 0xf0, 0xc9, 0xa0, 0x7e, 0xa8, 0x22, 0xd7, 0xa, 0xe7, 0xca, 0x27, 0xec, 0x62, 0x54, 0x4f, 0x1a, 0x72, 0x62, 0x6, 0xb0, 0xa3, 0xb, 0xd1, 0x5f, 0xa7, 0x53, 0x92, 0x60, 0x8d, 0x5c, 0x78, 0x87, 0xb1, 0x16, 0xb, 0xf8, 0x78, 0x9e, 0x52, 0x83, 0x35, 0x47, 0xb2, 0xbd, 0x8b, 0x4f, 0xd7, 0x64, 0xb5, 0x52, 0xac, 0xb8, 0xab, 0xb4, 0xca, 0x38, 0x46, 0xad, 0x80, 0xc6, 0xec, 0x38, 0x3f, 0x66, 0xe0, 0xa7, 0x81, 0x7d, 0x50, 0x7, 0xdf, 0x4f, 0x12, 0x5d, 0xd7, 0xf6, 0xef, 0x50, 0x94, 0x7, 0x6e, 0x26, 0x3c, 0x33, 0xe0, 0x64, 0xdd, 0xc0, 0xc0, 0x2c, 0x49, 0xc0, 0x8e, 0x2a, 0x71, 0x46, 0xc8, 0x4d, 0xb3, 0xf1, 0x93, 0x68, 0x56, 0xbc, 0xeb, 0xfb, 0x1a, 0x95, 0x1d, 0x57, 0x3, 0x4a, 0xf1, 0x97, 0x4f, 0x17, 0xf7, 0x93, 0xd6, 0xa2, 0xba, 0x97, 0x2b, 0xa6, 0xd8, 0x70, 0xf7, 0xa8, 0x79, 0xff, 0xef, 0x85, 0x54, 0x6e, 0xac, 0x5d, 0xdd, 0xbf, 0x98, 0xab, 0xe7, 0x85, 0x7b, 0x10, 0x5c, 0xcd, 0x3b, 0x75, 0xc8, 0xf7, 0x5f, 0xc8, 0x23, 0x8, 0xe2, 0x18, 0x27, 0xc2, 0xfa, 0xa1, 0x17, 0x1d, 0xa3, 0x75, 0x78, 0x1e, 0x6e, 0x25, 0xbd, 0xd7, 0x87, 0xc1, 0x64, 0xc1, 0xd9, 0x81, 0x45, 0xe3, 0x63, 0xb5, 0x5c, 0x72, 0x87, 0xa8, 0xea, 0xd, 0xd1, 0xef, 0x3a, 0x31, 0x3e, 0x93, 0x20, 0x8, 0xab, 0xa1, 0x7a, 0xb9, 0x6f, 0x9d, 0xfb, 0xe0, 0xbb, 0xc6, 0x35, 0xbf, 0x75, 0xfe, 0xf, 0xf8, 0xf, 0x5e, 0xa4, 0x68, 0x27, 0x0, 0x0 });
                for (int i = 0; i < text.Length; i++) {
                    Console.Write(text[i]);
                    if(i%15==0) Thread.Sleep(1);
                }
                void CopyTo(Stream src, Stream dest) {
                    byte[] bytes = new byte[4096];
                    int cnt;
                    while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0) {
                        dest.Write(bytes, 0, cnt);
                    }
                }
                string Unzip(byte[] bytes) {
                    using (var msi = new MemoryStream(bytes))
                    using (var mso = new MemoryStream()) {
                        using (var gs = new System.IO.Compression.GZipStream(msi, System.IO.Compression.CompressionMode.Decompress)) {
                            CopyTo(gs, mso);
                        }
                        return System.Text.Encoding.UTF8.GetString(mso.ToArray());
                    }
                }
            } else {
                ShowWindow(GetConsoleWindow(), 3);
            }
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(39, 14);
            Console.Title = "(X out or press ENTER key to close)";
            Console.ReadLine();
            ShowWindow(GetConsoleWindow(), 0);
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern bool AllocConsole();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}
