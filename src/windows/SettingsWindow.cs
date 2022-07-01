using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageToPaintBlockConverter {
    public partial class SettingsWindow : Form {
        public SettingsWindow() {
            InitializeComponent();
        }

        private void SettingsWindow_Load(object sender, EventArgs e) {
            this.Icon = Icon.FromHandle(ImageToPaintBlockConverter.Properties.Resources.LogoInverted.GetHicon());

            this.Location = new Point((int)System.Windows.SystemParameters.PrimaryScreenWidth / 2, (int)System.Windows.SystemParameters.PrimaryScreenHeight / 2);

            this.Text = "Settings";
            this.Width = 400;
            this.Height = 175;
            this.BackColor = Color.FromArgb(37, 37, 38);

            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            ToolTip tooltip = new ToolTip();

            TextBox stormworksVehicleFolderPath = new TextBox();
            stormworksVehicleFolderPath.Font = new Font("", 10);
            stormworksVehicleFolderPath.Text = Settings.vehicleFolderPath;
            stormworksVehicleFolderPath.ForeColor = Color.FromArgb(241, 241, 241);
            stormworksVehicleFolderPath.BackColor = Color.FromArgb(70, 70, 80);
            stormworksVehicleFolderPath.Location = new Point(5, 5);
            stormworksVehicleFolderPath.Size = new Size(375, 30);
            this.Controls.Add(stormworksVehicleFolderPath);

            stormworksVehicleFolderPath.MouseHover += new EventHandler((object o, EventArgs a) => {
                tooltip.SetToolTip(stormworksVehicleFolderPath, "The filepath to the stormworks vehicle folder.");
            });

            TextBox stormworksVehicleName = new TextBox();
            stormworksVehicleName.Font = new Font("", 10);
            stormworksVehicleName.Text = Settings.vehicleOutputName;
            stormworksVehicleName.ForeColor = Color.FromArgb(241, 241, 241);
            stormworksVehicleName.BackColor = Color.FromArgb(70, 70, 80);
            stormworksVehicleName.Location = new Point(5, 35);
            stormworksVehicleName.Size = new Size(375, 30);
            this.Controls.Add(stormworksVehicleName);

            stormworksVehicleName.MouseHover += new EventHandler((object o, EventArgs a) => {
                tooltip.SetToolTip(stormworksVehicleName, "The name of the generated vehicle.");
            });

            Button close = new Button();
            close.Font = new Font("", 10);
            close.Text = "Cancel";
            close.ForeColor = Color.FromArgb(241, 241, 241);
            close.BackColor = Color.FromArgb(70, 70, 80);
            close.Location = new Point(170, 95);
            close.Size = new Size(70, 25);
            close.FlatStyle = FlatStyle.Flat;
            close.FlatAppearance.BorderSize = 0;
            this.Controls.Add(close);

            close.Click += new EventHandler((object o, EventArgs a) => {
                this.Close();
            });

            Button saveSettings = new Button();
            saveSettings.Font = new Font("", 10);
            saveSettings.Text = "Save";
            saveSettings.ForeColor = Color.FromArgb(241, 241, 241);
            saveSettings.BackColor = Color.FromArgb(70, 70, 80);
            saveSettings.Location = new Point(245, 95);
            saveSettings.Size = new Size(65, 25);
            saveSettings.FlatStyle = FlatStyle.Flat;
            saveSettings.FlatAppearance.BorderSize = 0;
            this.Controls.Add(saveSettings);

            saveSettings.Click += new EventHandler((object o, EventArgs a) => {
                Settings.UpdateSettings(0, stormworksVehicleFolderPath.Text);
                Settings.UpdateSettings(1, stormworksVehicleName.Text);
                Settings.LoadSettings();
            });

            Button resetSetting = new Button();
            resetSetting.Font = new Font("", 10);
            resetSetting.Text = "Reset";
            resetSetting.ForeColor = Color.FromArgb(241, 241, 241);
            resetSetting.BackColor = Color.FromArgb(70, 70, 80);
            resetSetting.Location = new Point(315, 95);
            resetSetting.Size = new Size(65, 25);
            resetSetting.FlatStyle = FlatStyle.Flat;
            resetSetting.FlatAppearance.BorderSize = 0;
            this.Controls.Add(resetSetting);

            resetSetting.Click += new EventHandler((object o, EventArgs a) => {
                Settings.GenerateNewSettings();
                Settings.LoadSettings();
                stormworksVehicleFolderPath.Text = Settings.vehicleFolderPath;
                stormworksVehicleName.Text = Settings.vehicleOutputName;
            });
        }
    }
}