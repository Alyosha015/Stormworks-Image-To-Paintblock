using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageToPaintBlockConverter {

    public partial class Window : Form {
        public Window() {
            InitializeComponent();
        }

        private void Window_Load(object sender, EventArgs e) {
            this.Icon = Icon.FromHandle(ImageToPaintBlockConverter.Properties.Resources.LogoInverted.GetHicon());

            this.Location = new Point((int)System.Windows.SystemParameters.PrimaryScreenWidth/2,(int)System.Windows.SystemParameters.PrimaryScreenHeight/2);

            this.Text = Settings.windowTitle;
            this.Width = Settings.windowWidth;
            this.Height = Settings.windowHeight;
            this.BackColor = Color.FromArgb(37,37,38);

            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            String pathToImage = "";
            Bitmap selectedImage = new Bitmap(1, 1);
            bool fileSelected = false;

            //Mode selector
            ComboBox modes = new ComboBox();
            modes.Name = "ModeCombobox";
            modes.Font = new Font("", 10);
            modes.Location = new Point(5, 5);
            modes.Size = new Size(185, 25);
            modes.DropDownStyle = ComboBoxStyle.DropDownList;

            modes.Items.Add("Select Mode");
            modes.Items.Add("Custom Dimensions");
            modes.Items.Add("Custom Width");
            modes.Items.Add("Don't Resize");
            modes.SelectedIndex = 0;

            modes.ForeColor = Color.FromArgb(241, 241, 241);
            modes.BackColor = Color.FromArgb(70, 70, 80);

            modes.FlatStyle = FlatStyle.Flat;

            this.Controls.Add(modes);

            //output settings
            Panel settings = new Panel();
            settings.BackColor = Color.FromArgb(30, 30, 30);
            settings.Location = new Point(5, 40);
            settings.Size = new Size(185, 125);
            this.Controls.Add(settings);

            //width
            Label widthLabel = new Label();
            widthLabel.Font = new Font("", 10);
            widthLabel.ForeColor = Color.FromArgb(241, 241, 241);
            widthLabel.BackColor = Color.FromArgb(30, 30, 30);
            widthLabel.Location = new Point(5, 5);
            widthLabel.Size = new Size(60, 20);
            widthLabel.Text = "Width";
            settings.Controls.Add(widthLabel);

            NumericUpDown width = new NumericUpDown();
            width.Font = new Font("", 10);
            width.ForeColor = Color.FromArgb(241, 241, 241);
            width.BackColor = Color.FromArgb(70, 70, 80);
            width.Location = new Point(65, 5);
            width.Size = new Size(125, 20);
            width.Minimum = 1;
            width.Maximum = Int32.MaxValue;
            width.Controls[0].Hide();
            width.Enabled = false;
            width.BorderStyle = BorderStyle.None;
            settings.Controls.Add(width);

            //height
            Label heightLabel = new Label();
            heightLabel.Font = new Font("", 10);
            heightLabel.ForeColor = Color.FromArgb(241, 241, 241);
            heightLabel.BackColor = Color.FromArgb(30, 30, 30);
            heightLabel.Location = new Point(5, 40);
            heightLabel.Size = new Size(60, 20);
            heightLabel.Text = "Height";
            settings.Controls.Add(heightLabel);

            NumericUpDown height = new NumericUpDown();
            height.Font = new Font("", 10);
            height.ForeColor = Color.FromArgb(241, 241, 241);
            height.BackColor = Color.FromArgb(70, 70, 80);
            height.Location = new Point(65, 40);
            height.Size = new Size(125, 20);
            height.Minimum = 1;
            height.Maximum = Int32.MaxValue;
            height.Controls[0].Hide();
            height.Enabled = false;
            height.BorderStyle = BorderStyle.None;
            settings.Controls.Add(height);

            //use threshold checkbox
            Label useThresholdLabel = new Label();
            useThresholdLabel.Font = new Font("", 8);
            useThresholdLabel.ForeColor = Color.FromArgb(241, 241, 241);
            useThresholdLabel.BackColor = Color.FromArgb(30, 30, 30);
            useThresholdLabel.Location = new Point(5, 70);
            useThresholdLabel.Size = new Size(150, 15);
            useThresholdLabel.Text = "Optimize Paintblocks?";
            settings.Controls.Add(useThresholdLabel);

            CheckBox useThreshold = new CheckBox();
            useThreshold.Font = new Font("", 9);
            useThreshold.ForeColor = Color.FromArgb(241, 241, 241);
            useThreshold.BackColor = Color.FromArgb(70, 70, 80);
            useThreshold.Location = new Point(155, 70);
            useThreshold.Size = new Size(14, 15);
            useThreshold.FlatStyle = FlatStyle.Flat;
            useThreshold.FlatAppearance.BorderSize = 0;
            settings.Controls.Add(useThreshold);

            //threshold
            Label thresholdLabel = new Label();
            thresholdLabel.Font = new Font("", 10);
            thresholdLabel.ForeColor = Color.FromArgb(241, 241, 241);
            thresholdLabel.BackColor = Color.FromArgb(30, 30, 30);
            thresholdLabel.Location = new Point(5, 95);
            thresholdLabel.Size = new Size(83, 20);
            thresholdLabel.Text = "Threshold";
            settings.Controls.Add(thresholdLabel);

            NumericUpDown threshold = new NumericUpDown();
            threshold.Font = new Font("", 10);
            threshold.ForeColor = Color.FromArgb(241, 241, 241);
            threshold.BackColor = Color.FromArgb(70, 70, 80);
            threshold.Location = new Point(90, 95);
            threshold.Size = new Size(100, 20);
            threshold.Minimum = 1;
            threshold.Maximum = 255;
            threshold.Controls[0].Hide();
            threshold.Enabled = false;
            threshold.BorderStyle = BorderStyle.None;
            settings.Controls.Add(threshold);

            //controls
            Panel controls = new Panel();
            controls.BackColor = Color.FromArgb(30, 30, 30);
            controls.Location = new Point(195, 5);
            controls.Size = new Size(195, 160);
            this.Controls.Add(controls);

            //filepath
            Button getFile = new Button();
            getFile.Font = new Font("", 12);
            getFile.ForeColor = Color.FromArgb(241, 241, 241);
            getFile.BackColor = Color.FromArgb(70, 70, 80);
            getFile.Location = new Point(5, 5);
            getFile.Size = new Size(175,45);
            getFile.Text = "Select File";
            getFile.FlatStyle = FlatStyle.Flat;
            getFile.FlatAppearance.BorderSize = 0;

            controls.Controls.Add(getFile);

            //generate
            Button generate = new Button();
            generate.Font = new Font("", 12);
            generate.ForeColor = Color.FromArgb(241, 241, 241);
            generate.BackColor = Color.FromArgb(70, 70, 80);
            generate.Location = new Point(5, 55);
            generate.Size = new Size(175, 45);
            generate.Text = "Generate XML";
            generate.FlatStyle = FlatStyle.Flat;
            generate.FlatAppearance.BorderSize = 0;
            generate.Enabled = false;
            controls.Controls.Add(generate);

            //settings
            Button openSettings = new Button();
            openSettings.Font = new Font("", 12);
            openSettings.ForeColor = Color.FromArgb(241, 241, 241);
            openSettings.BackColor = Color.FromArgb(70, 70, 80);
            openSettings.Location = new Point(5, 105);
            openSettings.Size = new Size(175, 45);
            openSettings.Text = "Settings";
            openSettings.FlatStyle = FlatStyle.Flat;
            openSettings.FlatAppearance.BorderSize = 0;
            controls.Controls.Add(openSettings);

            modes.SelectedIndexChanged += new EventHandler((object o, EventArgs a) => {
                WidthHeightButtonLogic();
            });

            width.ValueChanged += new EventHandler((object o, EventArgs a) => {
                WidthHeightButtonLogic();
            });

            useThreshold.CheckedChanged += new EventHandler((object o, EventArgs a) => {
                if(useThreshold.Checked) {
                    threshold.Enabled = true;
                } else {
                    threshold.Enabled = false;
                }
            });

            getFile.Click += new EventHandler((object o, EventArgs a) => {
                pathToImage = Util.FileChooser("Image Chooser","", "All Files (*.*)|*.*|Supported Files (*.PNG;*.JPG;*.BMP;*.GIF;*.TIFF;*.EXIF)|*.PNG;*.JPG;*.BMP;*.GIF;*.TIFF;*.EXIF");
                if(pathToImage != null) { 
                    try {
                        selectedImage = Util.ReadImage(pathToImage);
                        fileSelected = true;
                    } catch {

                    }
                } else fileSelected = false;
                UpdateStuff();
            });

            generate.Click += new EventHandler((object o, EventArgs a) => {
                selectedImage = Util.ReadImage(pathToImage);
                WidthHeightButtonLogic();
                String path = Settings.vehicleFolderPath + Settings.vehicleOutputName;
                bool optimize = useThreshold.Checked;
                int optimizationThreshold = (int)threshold.Value;

                double aspectRatio = (double)selectedImage.Height / (double)selectedImage.Width;
                Bitmap image = new Bitmap(1,1);
                //Custom dimensions
                if(modes.SelectedIndex == 1) {
                    image = new Bitmap(selectedImage,new Size((int)width.Value*9,(int)height.Value*9));
                    GenerateAndSaveXML(image);
                }
                //Custom Width
                if(modes.SelectedIndex == 2) {
                    int newY=(int)(aspectRatio*(double)width.Value*9);
                    image = new Bitmap(selectedImage, new Size((int)width.Value*9,newY));
                    //height needs border
                    if (image.Height % 9 != 0) {
                        Bitmap correctedHeightImage = new Bitmap(image.Width,newY+9);
                        Graphics g = Graphics.FromImage(correctedHeightImage);
                        g.DrawImage(image,0,newY%9,image.Width,image.Height);
                        GenerateAndSaveXML(correctedHeightImage);
                    } else {
                        GenerateAndSaveXML(image);
                    }
                }
                //no resize
                if(modes.SelectedIndex == 3) {
                    int newY = (int)(aspectRatio * (double)width.Value * 9);
                    int newX = (int)(((double)selectedImage.Width / (double)selectedImage.Height) * (double)height.Value * 9);
                    Bitmap correctedSizeImage = new Bitmap((int)(Math.Ceiling((double)selectedImage.Width/9)*9), (int)(Math.Ceiling((double)selectedImage.Height / 9) * 9));
                    if (selectedImage.Width % 9 == 0 && selectedImage.Height % 9 == 0) GenerateAndSaveXML(selectedImage);
                    else {
                        Graphics g = Graphics.FromImage(correctedSizeImage);
                        g.DrawImage(selectedImage, 0, 0, selectedImage.Width, selectedImage.Height);
                        GenerateAndSaveXML(correctedSizeImage);
                    }
                }
                void GenerateAndSaveXML(Bitmap bitmap) {
                    Util.SaveFile(GenerateVehicle.GenerateXML(bitmap, optimize, optimizationThreshold), path);
                }
                System.GC.Collect();
            });

            openSettings.Click += new EventHandler((object o, EventArgs a) => {
                SettingsWindow settingsWindow = new SettingsWindow();
                settingsWindow.ShowDialog();
            });

            void UpdateStuff() {
                WidthHeightButtonLogic();
                if(fileSelected) {
                    generate.Enabled = true;
                } else {
                    generate.Enabled = false;
                }
            }

            void WidthHeightButtonLogic() {
                if(fileSelected) {
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
                        double newHeight = ((double)selectedImage.Height / (double)selectedImage.Width) * (double)width.Value*9;
                        height.Value = (int)Math.Ceiling(Math.Floor(newHeight)/9);
                    }
                    //no resize
                    if(modes.SelectedIndex == 3) {
                        width.Enabled = false;
                        height.Enabled = false;
                        width.Value = (int)Math.Ceiling((double)selectedImage.Width / 9);
                        height.Value = (int)Math.Ceiling((double)selectedImage.Height / 9);
                    }
                } else {
                    width.Enabled = false;
                    height.Enabled = false;
                    width.Value = 1;
                    height.Value = 1;
                }
            }
        }
    }
}