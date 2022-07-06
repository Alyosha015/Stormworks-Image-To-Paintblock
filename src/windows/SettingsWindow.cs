using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageToPaintBlockConverter {
    public partial class SettingsWindow : Form {
        public SettingsWindow() {
            InitializeComponent();
        }

        private void SettingsWindow_Load(object sender, EventArgs e) {
            float Scale = Settings.scale;
            float dpi = this.CreateGraphics().DpiX;
            float fontCorrection = (120 / dpi) * Scale;

            this.Icon = Icon.FromHandle(ImageToPaintBlockConverter.Properties.Resources.LogoInverted.GetHicon());

            this.Location = new Point((int)System.Windows.SystemParameters.PrimaryScreenWidth / 2, (int)System.Windows.SystemParameters.PrimaryScreenHeight / 2);

            this.Text = "Settings";
            this.Width = 1;
            this.Height = 1;
            this.BackColor = Color.FromArgb(37, 37, 38);

            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            ToolTip tooltip = new ToolTip();

            TextBox stormworksVehicleFolderPath = new TextBox();
            stormworksVehicleFolderPath.Font = new Font("", 10 * fontCorrection);
            stormworksVehicleFolderPath.Text = Settings.vehicleFolderPath;
            stormworksVehicleFolderPath.ForeColor = Color.FromArgb(220, 220, 220);
            stormworksVehicleFolderPath.BackColor = Color.FromArgb(70, 70, 80);
            stormworksVehicleFolderPath.Location = new Point(S(5), S(5));
            stormworksVehicleFolderPath.Size = new Size(S(375), S(30));
            this.Controls.Add(stormworksVehicleFolderPath);

            stormworksVehicleFolderPath.MouseHover += new EventHandler((object o, EventArgs a) => {
                tooltip.SetToolTip(stormworksVehicleFolderPath, "The filepath to the stormworks vehicle folder.");
            });

            stormworksVehicleFolderPath.KeyDown += (object o, KeyEventArgs a) => {
                if (a.KeyCode == Keys.Enter) a.SuppressKeyPress = true;
            };

            TextBox stormworksVehicleName = new TextBox();
            stormworksVehicleName.Font = new Font("", 10 * fontCorrection);
            stormworksVehicleName.Text = Settings.vehicleOutputName;
            stormworksVehicleName.ForeColor = Color.FromArgb(220, 220, 220);
            stormworksVehicleName.BackColor = Color.FromArgb(70, 70, 80);
            stormworksVehicleName.Location = new Point(S(5), S(35));
            stormworksVehicleName.Size = new Size(S(375), S(30));
            this.Controls.Add(stormworksVehicleName);

            stormworksVehicleName.MouseHover += new EventHandler((object o, EventArgs a) => {
                tooltip.SetToolTip(stormworksVehicleName, "The name of the generated vehicle.");
            });

            stormworksVehicleName.KeyDown += (object o, KeyEventArgs a) => {
                if (a.KeyCode == Keys.Enter) a.SuppressKeyPress = true;
            };

            //scale
            Label scaleLabel = new Label();
            scaleLabel.Font = new Font("", 10 * fontCorrection);
            scaleLabel.ForeColor = Color.FromArgb(220, 220, 220);
            scaleLabel.Location = new Point(S(5), S(65));
            scaleLabel.Size = new Size(S(120), S(20));
            scaleLabel.Text = "Window Scale";
            this.Controls.Add(scaleLabel);

            NumericUpDown scale = new NumericUpDown();
            scale.Font = new Font("", 10 * fontCorrection);
            scale.Value = (decimal)Settings.scale;
            scale.ForeColor = Color.FromArgb(220, 220, 220);
            scale.BackColor = Color.FromArgb(70, 70, 80);
            scale.Location = new Point(S(125), S(65));
            scale.Size = new Size(S(50), S(20));
            scale.Increment = (decimal)0.25;
            scale.DecimalPlaces = 2;
            scale.Minimum = 1;
            scale.Maximum = 5;
            scale.BorderStyle = BorderStyle.None;
            this.Controls.Add(scale);
            scale.KeyDown += (object o, KeyEventArgs a) => {
                if (a.KeyCode == Keys.Enter) a.SuppressKeyPress = true;
            };

            Button close = new Button();
            close.Font = new Font("", 10 * fontCorrection);
            close.Text = "Cancel";
            close.ForeColor = Color.FromArgb(220, 220, 220);
            close.BackColor = Color.FromArgb(70, 70, 80);
            close.Location = new Point(S(170), S(95));
            close.Size = new Size(S(70), S(25));
            close.FlatStyle = FlatStyle.Flat;
            close.FlatAppearance.BorderSize = 0;
            this.Controls.Add(close);

            close.Click += new EventHandler((object o, EventArgs a) => {
                this.Close();
            });

            Button saveSettings = new Button();
            saveSettings.Font = new Font("", 10 * fontCorrection);
            saveSettings.Text = "Save";
            saveSettings.ForeColor = Color.FromArgb(220, 220, 220);
            saveSettings.BackColor = Color.FromArgb(70, 70, 80);
            saveSettings.Location = new Point(S(245), S(95));
            saveSettings.Size = new Size(S(65), S(25));
            saveSettings.FlatStyle = FlatStyle.Flat;
            saveSettings.FlatAppearance.BorderSize = 0;
            this.Controls.Add(saveSettings);

            saveSettings.Click += new EventHandler((object o, EventArgs a) => {
                Settings.UpdateSettings(1, stormworksVehicleFolderPath.Text);
                Settings.UpdateSettings(2, stormworksVehicleName.Text);
                Settings.UpdateSettings(3, scale.Value.ToString());
                Settings.LoadSettings();
            });

            Button resetSetting = new Button();
            resetSetting.Font = new Font("", 10 * fontCorrection);
            resetSetting.Text = "Reset";
            resetSetting.ForeColor = Color.FromArgb(220, 220, 220);
            resetSetting.BackColor = Color.FromArgb(70, 70, 80);
            resetSetting.Location = new Point(S(315), S(95));
            resetSetting.Size = new Size(S(65), S(25));
            resetSetting.FlatStyle = FlatStyle.Flat;
            resetSetting.FlatAppearance.BorderSize = 0;
            this.Controls.Add(resetSetting);

            resetSetting.Click += new EventHandler((object o, EventArgs a) => {
                Settings.GenerateNewSettings();
                Settings.LoadSettings();
                stormworksVehicleFolderPath.Text = Settings.vehicleFolderPath;
                stormworksVehicleName.Text = Settings.vehicleOutputName;
                scale.Value = (decimal)Settings.scale;
            });

            int S(int value) {
                return (int)(value * Scale);
            }
        }
    }
}