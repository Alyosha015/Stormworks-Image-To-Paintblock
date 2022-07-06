using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace ImageToPaintBlockConverter {
    public partial class Window : Form {
        public static Window window;
        public Button generate;
        public Window() {
            InitializeComponent();
        }

        private void Window_Load(object sender, EventArgs e) {
            window = this;
            float Scale = Settings.scale;
            float dpi = this.CreateGraphics().DpiX;
            float fontCorrection = (120 / dpi) * Scale;

            this.Icon = Icon.FromHandle(ImageToPaintBlockConverter.Properties.Resources.LogoInverted.GetHicon());

            this.Location = new Point((int)System.Windows.SystemParameters.PrimaryScreenWidth / 2, (int)System.Windows.SystemParameters.PrimaryScreenHeight / 2);

            this.Text = Settings.windowTitle;
            this.Width = 1;
            this.Height = 1;
            this.BackColor = Color.FromArgb(37, 37, 38);

            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            String pathToBackground = "";
            Bitmap backgroundImage = new Bitmap(1, 1);
            bool backgroundSelected = false;

            String pathToGlow = "";
            Bitmap glowImage = new Bitmap(1, 1);
            bool glowSelected = false;

            ToolTip tooltip = new ToolTip();

            //Mode selector
            ComboBox modes = new ComboBox();
            modes.Name = "ModeCombobox";
            modes.Font = new Font("", 10 * fontCorrection);
            modes.Location = new Point(S(5), S(5));
            modes.Size = new Size(S(185), S(25));
            modes.DropDownStyle = ComboBoxStyle.DropDownList;

            modes.Items.Add("Select Mode");
            modes.Items.Add("Custom Dimensions");
            modes.Items.Add("Custom Width");
            modes.Items.Add("Custom Height");
            modes.Items.Add("Don't Resize");
            modes.SelectedIndex = 0;

            modes.ForeColor = Color.FromArgb(220, 220, 220);
            modes.BackColor = Color.FromArgb(70, 70, 80);

            modes.FlatStyle = FlatStyle.Flat;

            this.Controls.Add(modes);

            modes.MouseHover += new EventHandler((object o, EventArgs a) => {
                tooltip.SetToolTip(modes, "Modes for how to convert the image to paintblocks.\n 1: Custom Dimensions allows stretching the image\n 2: Custom Width keeps the aspect ratio of the image\n 3: Don't Resize copies the image pixel for pixel");
            });

            //output settings
            Panel settings = new Panel();
            settings.BackColor = Color.FromArgb(30, 30, 30);
            settings.Location = new Point(S(5), S(40));
            settings.Size = new Size(S(185), S(125));
            this.Controls.Add(settings);

            //width
            Label widthLabel = new Label();
            widthLabel.Font = new Font("", 10 * fontCorrection);
            widthLabel.ForeColor = Color.FromArgb(220, 220, 220);
            widthLabel.Location = new Point(S(5), S(5));
            widthLabel.Size = new Size(S(60), S(20));
            widthLabel.Text = "Width";
            settings.Controls.Add(widthLabel);

            NumericUpDown width = new NumericUpDown();
            width.Font = new Font("", 10 * fontCorrection);
            width.ForeColor = Color.FromArgb(220, 220, 220);
            width.BackColor = Color.FromArgb(70, 70, 80);
            width.Location = new Point(S(65), S(5));
            width.Size = new Size(S(125), S(20));
            width.Minimum = 1;
            width.Maximum = Int32.MaxValue;
            width.Controls[0].Hide();
            width.Enabled = false;
            width.BorderStyle = BorderStyle.None;
            settings.Controls.Add(width);
            width.KeyDown += (object o, KeyEventArgs a) => {
                if(a.KeyCode==Keys.Enter) a.SuppressKeyPress = true;
            };

            //height
            Label heightLabel = new Label();
            heightLabel.Font = new Font("", 10 * fontCorrection);
            heightLabel.ForeColor = Color.FromArgb(220, 220, 220);
            heightLabel.Location = new Point(S(5), S(40));
            heightLabel.Size = new Size(S(60), S(20));
            heightLabel.Text = "Height";
            settings.Controls.Add(heightLabel);

            NumericUpDown height = new NumericUpDown();
            height.Font = new Font("", 10 * fontCorrection);
            height.ForeColor = Color.FromArgb(220, 220, 220);
            height.BackColor = Color.FromArgb(70, 70, 80);
            height.Location = new Point(S(65), S(40));
            height.Size = new Size(S(125), S(20));
            height.Minimum = 1;
            height.Maximum = Int32.MaxValue;
            height.Controls[0].Hide();
            height.Enabled = false;
            height.BorderStyle = BorderStyle.None;
            settings.Controls.Add(height);
            height.KeyDown += (object o, KeyEventArgs a) => {
                if (a.KeyCode == Keys.Enter) a.SuppressKeyPress = true;
            };

            //use threshold checkbox
            Label useThresholdLabel = new Label();
            useThresholdLabel.Font = new Font("", 8 * fontCorrection);
            useThresholdLabel.ForeColor = Color.FromArgb(220, 220, 220);
            useThresholdLabel.Location = new Point(S(5), S(70));
            useThresholdLabel.Size = new Size(S(75), S(15));
            useThresholdLabel.Text = "Optimize?";
            settings.Controls.Add(useThresholdLabel);

            useThresholdLabel.MouseHover += new EventHandler((object o, EventArgs a) => {
                tooltip.SetToolTip(useThresholdLabel, "Optimizes the image by replacing paintblocks with regular blocks.\nA paintblock is replaced when the min/max values of the colors are within the set threshold. The block's color is an average of the pixels.");
            });

            CheckBox useThreshold = new CheckBox();
            useThreshold.Font = new Font("", 9 * fontCorrection);
            useThreshold.ForeColor = Color.FromArgb(220, 220, 220);
            useThreshold.BackColor = Color.FromArgb(70, 70, 80);
            useThreshold.Location = new Point(S(80), S(70));
            useThreshold.Size = new Size(S(14), S(15));
            useThreshold.FlatStyle = FlatStyle.Flat;
            useThreshold.FlatAppearance.BorderSize = 0;
            settings.Controls.Add(useThreshold);

            useThreshold.MouseHover += new EventHandler((object o, EventArgs a) => {
                tooltip.SetToolTip(useThreshold, "Optimizes the image by replacing paintblocks with regular blocks.\nA paintblock is replaced when the min/max values of the colors are within the set threshold. The block's color is an average of the pixels.");
            });

            //glow
            Label useGlowLabel = new Label();
            useGlowLabel.Font = new Font("", 8 * fontCorrection);
            useGlowLabel.ForeColor = Color.FromArgb(220, 220, 220);
            useGlowLabel.Location = new Point(S(110), S(70));
            useGlowLabel.Size = new Size(S(50), S(15));
            useGlowLabel.Text = "Glow?";
            settings.Controls.Add(useGlowLabel);

            useGlowLabel.MouseHover += new EventHandler((object o, EventArgs a) => {
                tooltip.SetToolTip(useGlowLabel, "Allows generating paintable indicators instead of paintable signs. If a background image is selected, the glow image will be scaled/stretched to the size of the background image.");
            });

            CheckBox useGlow = new CheckBox();
            useGlow.Font = new Font("", 9 * fontCorrection);
            useGlow.ForeColor = Color.FromArgb(220, 220, 220);
            useThreshold.BackColor = Color.FromArgb(70, 70, 80);
            useGlow.Location = new Point(S(160), S(70));
            useGlow.Size = new Size(S(14), S(15));
            useGlow.FlatStyle = FlatStyle.Flat;
            useGlow.FlatAppearance.BorderSize = 0;
            settings.Controls.Add(useGlow);

            useGlow.MouseHover += new EventHandler((object o, EventArgs a) => {
                tooltip.SetToolTip(useGlow, "Allows generating paintable indicators instead of paintable signs. If a background image is selected, the glow image will be scaled/stretched to the size of the background image.");
            });

            //threshold
            Label thresholdLabel = new Label();
            thresholdLabel.Font = new Font("", 10 * fontCorrection);
            thresholdLabel.ForeColor = Color.FromArgb(220, 220, 220);
            thresholdLabel.Location = new Point(S(5), S(95));
            thresholdLabel.Size = new Size(S(85), S(20));
            thresholdLabel.Text = "Threshold";
            settings.Controls.Add(thresholdLabel);

            NumericUpDown threshold = new NumericUpDown();
            threshold.Font = new Font("", 10 * fontCorrection);
            threshold.ForeColor = Color.FromArgb(220, 220, 220);
            threshold.BackColor = Color.FromArgb(70, 70, 80);
            threshold.Location = new Point(S(90), S(95));
            threshold.Size = new Size(S(100), S(20));
            threshold.Minimum = 1;
            threshold.Maximum = 255;
            threshold.Controls[0].Hide();
            threshold.Enabled = false;
            threshold.BorderStyle = BorderStyle.None;
            settings.Controls.Add(threshold);
            threshold.KeyDown += (object o, KeyEventArgs a) => {
                if (a.KeyCode == Keys.Enter) a.SuppressKeyPress = true;
            };

            //controls
            Panel controls = new Panel();
            controls.BackColor = Color.FromArgb(30, 30, 30);
            controls.Location = new Point(S(195), S(5));
            controls.Size = new Size(S(185), S(160));
            this.Controls.Add(controls);

            //filepath0
            Button selectBackgroundFile = new Button();
            selectBackgroundFile.Font = new Font("", 12 * fontCorrection);
            selectBackgroundFile.ForeColor = Color.FromArgb(220, 220, 220);
            selectBackgroundFile.BackColor = Color.FromArgb(70, 70, 80);
            selectBackgroundFile.Location = new Point(S(5), S(5));
            selectBackgroundFile.Size = new Size(S(175), S(45));
            selectBackgroundFile.Text = "Select File";
            selectBackgroundFile.FlatStyle = FlatStyle.Flat;
            selectBackgroundFile.FlatAppearance.BorderSize = 0;

            controls.Controls.Add(selectBackgroundFile);

            //filepath1
            Button selectGlowFile = new Button();
            selectGlowFile.Font = new Font("", 9 * fontCorrection);
            selectGlowFile.ForeColor = Color.FromArgb(220, 220, 220);
            selectGlowFile.BackColor = Color.FromArgb(70, 70, 80);
            selectGlowFile.Location = new Point(S(110), S(5));
            selectGlowFile.Size = new Size(S(70), S(45));
            selectGlowFile.Text = "Select Glow";
            selectGlowFile.FlatStyle = FlatStyle.Flat;
            selectGlowFile.FlatAppearance.BorderSize = 0;
            selectGlowFile.Enabled = false;
            selectGlowFile.Visible = false;

            controls.Controls.Add(selectGlowFile);

            //generate
            generate = new Button();
            generate.Font = new Font("", 12 * fontCorrection);
            generate.ForeColor = Color.FromArgb(220, 220, 220);
            generate.BackColor = Color.FromArgb(70, 70, 80);
            generate.Location = new Point(S(5), S(55));
            generate.Size = new Size(S(175), S(45));
            generate.Text = "Generate XML";
            generate.FlatStyle = FlatStyle.Flat;
            generate.FlatAppearance.BorderSize = 0;
            generate.Enabled = false;
            controls.Controls.Add(generate);

            //settings
            Button openSettings = new Button();
            openSettings.Font = new Font("", 12 * fontCorrection);
            openSettings.ForeColor = Color.FromArgb(220, 220, 220);
            openSettings.BackColor = Color.FromArgb(70, 70, 80);
            openSettings.Location = new Point(S(5), S(105));
            openSettings.Size = new Size(S(175), S(45));
            openSettings.Text = "Settings";
            openSettings.FlatStyle = FlatStyle.Flat;
            openSettings.FlatAppearance.BorderSize = 0;
            controls.Controls.Add(openSettings);

            modes.SelectedIndexChanged += new EventHandler((object o, EventArgs a) => {
                if (modes.SelectedIndex != 0 && (backgroundSelected || glowSelected)) generate.Enabled = true;
                else generate.Enabled = false;
                WidthHeightButtonLogic();
            });

            width.ValueChanged += new EventHandler((object o, EventArgs a) => {
                WidthHeightButtonLogic();
            });

            height.ValueChanged += new EventHandler((object o, EventArgs a) => {
                WidthHeightButtonLogic();
            });

            useGlow.CheckedChanged += new EventHandler((object o, EventArgs a) => {
                if(useGlow.Checked) {
                    selectGlowFile.Enabled = true;
                    selectGlowFile.Visible = true;

                    selectBackgroundFile.Size = new Size(S(100),S(45));
                    selectBackgroundFile.Font = new Font("", 9 * fontCorrection);
                    selectBackgroundFile.Text = "Select Background";

                    useThreshold.Enabled = false;
                } else {
                    selectGlowFile.Enabled = false;
                    selectGlowFile.Visible = false;

                    selectBackgroundFile.Size = new Size(S(175), S(45));
                    selectBackgroundFile.Font = new Font("", 12 * fontCorrection);
                    selectBackgroundFile.Text = "Select File";

                    pathToGlow = "";
                    glowSelected = false;
                    glowImage = new Bitmap(1, 1);

                    useThreshold.Enabled = true;
                }
            });

            useThreshold.CheckedChanged += new EventHandler((object o, EventArgs a) => {
                if(useThreshold.Checked) {
                    threshold.Enabled = true;
                } else {
                    threshold.Enabled = false;
                }
            });

            selectBackgroundFile.Click += new EventHandler((object o, EventArgs a) => {
                pathToBackground = Util.FileChooser("Image Chooser","", "All Files (*.*)|*.*|Supported Files (*.PNG;*.JPG;*.BMP;*.GIF;*.TIFF;*.EXIF)|*.PNG;*.JPG;*.BMP;*.GIF;*.TIFF;*.EXIF");
                if (pathToBackground != null) {
                    try {
                        backgroundImage = Util.ReadImage(pathToBackground);
                        backgroundSelected = true;
                    } catch {

                    }
                }
                else { backgroundSelected = false; backgroundImage = new Bitmap(1, 1); if (glowSelected) backgroundImage = new Bitmap(glowImage); }
                WidthHeightButtonLogic();
                if ((backgroundSelected || (glowSelected && useGlow.Checked)) && modes.SelectedIndex != 0) generate.Enabled = true;
                else generate.Enabled = false;

                if (backgroundSelected) selectBackgroundFile.ForeColor = Color.White;
                else selectBackgroundFile.ForeColor = Color.FromArgb(220, 220, 220);
            });

            selectGlowFile.Click += new EventHandler((object o, EventArgs a) => {
                pathToGlow = Util.FileChooser("Image Chooser", "", "All Files (*.*)|*.*|Supported Files (*.PNG;*.JPG;*.BMP;*.GIF;*.TIFF;*.EXIF)|*.PNG;*.JPG;*.BMP;*.GIF;*.TIFF;*.EXIF");
                if (pathToGlow != null) {
                    try {
                        glowImage = Util.ReadImage(pathToGlow);
                        glowSelected = true;
                    } catch {

                    }
                }
                else { glowSelected = false; glowImage = new Bitmap(1, 1); }
                if (glowSelected && !backgroundSelected) backgroundImage = new Bitmap(glowImage);
                WidthHeightButtonLogic();
                if (backgroundSelected || (glowSelected && useGlow.Checked)) generate.Enabled = true;
                else generate.Enabled = false;

                if (glowSelected) selectGlowFile.ForeColor = Color.White;
                else selectGlowFile.ForeColor = Color.FromArgb(220, 220, 220);
            });

            //(v.1.5.0) this turned into spagehetti code, im sorry to anyone trying to understand it in advance.
            //goodluck!
            //(v1.5.2) good news, I kinda fixed it.
            generate.Click += new EventHandler((object o, EventArgs a) => {
                generate.Text = "Generating...";
                generate.Enabled = false;
                //incase the image was edited since it was selected, reopen the image and recalulate the width/height in blocks incase the size changed.
                if (File.Exists(pathToBackground) && backgroundSelected) backgroundImage = Util.ReadImage(pathToBackground);
                if (File.Exists(pathToGlow) && glowSelected) glowImage = Util.ReadImage(pathToGlow);
                if (!backgroundSelected) backgroundImage = new Bitmap(glowImage); //if only the glow image is selected, set it to the background so that it will be resized.
                WidthHeightButtonLogic();

                String path = Settings.vehicleFolderPath + Settings.vehicleOutputName;
                bool optimize = useThreshold.Checked;
                int optimizationThreshold = (int)threshold.Value;
                bool glow = useGlow.Checked;

                double aspectRatio = (double)backgroundImage.Height / (double)backgroundImage.Width;
                int newY = (int)(((double)backgroundImage.Height / (double)backgroundImage.Width) * (double)width.Value * 9);
                int newX = (int)(((double)backgroundImage.Width / (double)backgroundImage.Height) * (double)height.Value * 9);

                //bitmap storing image after processing (resizing/adding white background)
                Bitmap backgroundResized = new Bitmap(1, 1);
                Bitmap glowResized = new Bitmap(1, 1);

                //bitmap fed into generator
                Bitmap backgroundBitmap = new Bitmap(1, 1);
                Bitmap glowBitmap = new Bitmap(1, 1);

                //resizes images based on mode
                if(modes.SelectedIndex==1) { //custom dimensions mode
                    backgroundResized = new Bitmap(backgroundImage, (int)width.Value * 9, (int)height.Value * 9);
                    if (glow) glowResized = new Bitmap(glowImage, backgroundResized.Width, backgroundResized.Height);
                } else if(modes.SelectedIndex==2) { //custom width mode
                    backgroundResized = new Bitmap(backgroundImage, (int)width.Value * 9, newY);
                    if (glow) glowResized = new Bitmap(glowImage, backgroundResized.Width, backgroundResized.Height);
                } else if (modes.SelectedIndex == 3) { //custom height mode
                    backgroundResized = new Bitmap(backgroundImage, newX, (int)height.Value * 9);
                    if (glow) glowResized = new Bitmap(glowImage, backgroundResized.Width, backgroundResized.Height);
                } else if (modes.SelectedIndex == 4) { //don't resize mode
                    backgroundResized = new Bitmap(backgroundImage);
                    if(glow) {
                        if (glow) glowResized = new Bitmap(glowImage, backgroundImage.Width, backgroundImage.Height);
                    }
                }
                //draw image onto background, add which where image is incase the image is transparent, and a border if the width or height is not divisible by 9.
                backgroundBitmap = new Bitmap((int)(Math.Ceiling((double)backgroundResized.Width / 9) * 9), (int)(Math.Ceiling((double)backgroundResized.Height / 9) * 9));
                Graphics gBackground = Graphics.FromImage(backgroundBitmap);
                if (backgroundSelected) gBackground.FillRectangle(new SolidBrush(Color.White), new RectangleF(0, 0, backgroundResized.Width, backgroundResized.Height));
                gBackground.DrawImage(backgroundResized,0,0,backgroundResized.Width,backgroundResized.Height);

                if(glow) {
                    glowBitmap = new Bitmap(backgroundBitmap.Width, backgroundBitmap.Height);
                    Graphics gGlow = Graphics.FromImage(glowBitmap);
                    gGlow.DrawImage(glowResized,0,0,glowResized.Width,glowResized.Height);
                }

                if(!backgroundSelected) { glowBitmap = new Bitmap(backgroundBitmap); backgroundBitmap = new Bitmap(1,1); }
                //generate the vehicle on a separate thread from the window so that the window doesn't freeze.
                ThreadStart starter = GenerateXML;
                starter += () => { //runs when thread is finished
                    Invoke(new Action(() => { generate.Text = "Generate XML"; generate.Enabled = true; }));
                };

                Thread thread = new Thread(starter);
                thread.IsBackground = true; //makes thread stop when main window is closed
                thread.Start();

                void GenerateXML() {
                    if(!glow) Util.SaveFile(GenerateVehicle.GenerateXML(backgroundBitmap, optimize, optimizationThreshold),path);
                    else Util.SaveFile(GenerateVehicle.GenerateXML(backgroundBitmap, glowBitmap, backgroundSelected), path);
                }
                System.GC.Collect();
            });

            openSettings.Click += new EventHandler((object o, EventArgs a) => {
                Thread thread = new Thread(SettingsWindow);
                thread.IsBackground = true;  //makes thread stop when main window is closed
                thread.Start();
                void SettingsWindow() {
                    new SettingsWindow().ShowDialog();
                }
            });

            void WidthHeightButtonLogic() {
                if(backgroundSelected || (!backgroundSelected && glowSelected)) {
                    //none selected
                    if(modes.SelectedIndex == 0) {
                        width.Enabled = false;
                        height.Enabled = false;
                    }
                    //Custom dimensions
                    if(modes.SelectedIndex == 1) {
                        width.Enabled = true;
                        height.Enabled = true;
                    }
                    //Custom Width
                    if(modes.SelectedIndex == 2) {
                        width.Enabled = true;
                        height.Enabled = false;
                        double newHeight = ((double)backgroundImage.Height / (double)backgroundImage.Width) * (double)width.Value*9;
                        height.Value = (int)Math.Ceiling(Math.Floor(newHeight)/9);
                    }
                    //Custom Height
                    if(modes.SelectedIndex == 3) {
                        width.Enabled = false;
                        height.Enabled = true;
                        double newWidth = ((double)backgroundImage.Width / (double)backgroundImage.Height) * (double)height.Value * 9;
                        width.Value = (int)Math.Ceiling(Math.Floor(newWidth) / 9);
                    }
                    //no resize
                    if (modes.SelectedIndex == 4) {
                        width.Enabled = false;
                        height.Enabled = false;
                        width.Value = (int)Math.Ceiling((double)backgroundImage.Width / 9);
                        height.Value = (int)Math.Ceiling((double)backgroundImage.Height / 9);
                    }
                } else {
                    width.Enabled = false;
                    height.Enabled = false;
                    width.Value = 1;
                    height.Value = 1;
                }
            }

            int S(int value) {
                return (int)(value * Scale);
            }
        }
    }
}