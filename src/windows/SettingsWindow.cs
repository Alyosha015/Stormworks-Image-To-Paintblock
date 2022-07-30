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

            //XML backups
            Label doBackupsLabel = new Label();
            doBackupsLabel.Font = new Font("", 10 * fontCorrection);
            doBackupsLabel.ForeColor = Color.FromArgb(220, 220, 220);
            doBackupsLabel.Location = new Point(S(0), S(67));
            doBackupsLabel.Size = new Size(S(147), S(20));
            doBackupsLabel.Text = "Backup Vehicles?";
            this.Controls.Add(doBackupsLabel);

            doBackupsLabel.MouseHover += new EventHandler((object o, EventArgs a) => {
                tooltip.SetToolTip(doBackupsLabel, "Stores backups of vehicles overwritten by the image converter.");
            });

            CheckBox doBackups = new CheckBox();
            doBackups.Checked = Settings.doBackups;
            doBackups.ForeColor = Color.FromArgb(220, 220, 220);
            doBackups.BackColor = Color.FromArgb(70, 70, 80);
            doBackups.Location = new Point(S(147), S(70));
            doBackups.Size = new Size(S(14), S(15));
            doBackups.FlatStyle = FlatStyle.Flat;
            doBackups.FlatAppearance.BorderSize = 0;
            this.Controls.Add(doBackups);

            doBackups.MouseHover += new EventHandler((object o, EventArgs a) => {
                tooltip.SetToolTip(doBackups, "Stores backups of vehicles overwritten by the image converter.");
            });

            //how many files to back up at a time
            Label backupCountLabel = new Label();
            backupCountLabel.Font = new Font("", 10 * fontCorrection);
            backupCountLabel.ForeColor = Color.FromArgb(220, 220, 220);
            backupCountLabel.Location = new Point(S(165), S(67));
            backupCountLabel.Size = new Size(S(105), S(20));
            backupCountLabel.Text = "Backup Max";
            this.Controls.Add(backupCountLabel);

            backupCountLabel.MouseHover += new EventHandler((object o, EventArgs a) => {
                tooltip.SetToolTip(backupCountLabel, "Amount of backups to store until the oldest backup is deleted.");
            });

            NumericUpDown backupCount = new NumericUpDown();
            backupCount.Font = new Font("", 10 * fontCorrection);
            backupCount.Value = Settings.backupCount;
            backupCount.ForeColor = Color.FromArgb(220, 220, 220);
            backupCount.BackColor = Color.FromArgb(70, 70, 80);
            backupCount.Location = new Point(S(270), S(67));
            backupCount.Size = new Size(S(40), S(20));
            backupCount.Minimum = 1;
            backupCount.Maximum = 99;
            backupCount.BorderStyle = BorderStyle.None;
            this.Controls.Add(backupCount);
            backupCount.KeyDown += (object o, KeyEventArgs a) => {
                if (a.KeyCode == Keys.Enter) a.SuppressKeyPress = true;
            };

            //open backups in file explorer
            Button openBackups = new Button();
            openBackups.Font = new Font("", 7 * fontCorrection);
            openBackups.Text = "Backups";
            openBackups.ForeColor = Color.FromArgb(220, 220, 220);
            openBackups.BackColor = Color.FromArgb(70, 70, 80);
            openBackups.Location = new Point(S(315), S(67));
            openBackups.Size = new Size(S(65), S(25));
            openBackups.FlatStyle = FlatStyle.Flat;
            openBackups.FlatAppearance.BorderSize = 0;
            this.Controls.Add(openBackups);
            openBackups.MouseHover += new EventHandler((object o, EventArgs a) => {
                tooltip.SetToolTip(openBackups, "Opens backup folder in file explorer. The newest backup is backup0.xml, second newest is backup1.xml, and so on.");
            });

            openBackups.Click += new EventHandler((object o, EventArgs a) => {
                XMLBackup.OpenBackups();
            });

            //scale
            Label scaleLabel = new Label();
            scaleLabel.Font = new Font("", 9 * fontCorrection);
            scaleLabel.ForeColor = Color.FromArgb(220, 220, 220);
            scaleLabel.Location = new Point(S(0), S(95));
            scaleLabel.Size = new Size(S(107), S(20));
            scaleLabel.Text = "Window Scale";
            this.Controls.Add(scaleLabel);
            scaleLabel.MouseHover += new EventHandler((object o, EventArgs a) => {
                tooltip.SetToolTip(scaleLabel, "A bit experimental, program has to be restarted for scale to change.");
            });

            NumericUpDown scale = new NumericUpDown();
            scale.Font = new Font("", 10 * fontCorrection);
            scale.Value = (decimal)Settings.scale;
            scale.ForeColor = Color.FromArgb(220, 220, 220);
            scale.BackColor = Color.FromArgb(70, 70, 80);
            scale.Location = new Point(S(110), S(95));
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
                Settings.vehicleFolderPath = stormworksVehicleFolderPath.Text;
                Settings.vehicleOutputName = stormworksVehicleName.Text;
                Settings.doBackups = doBackups.Checked;
                Settings.backupCount = (int)backupCount.Value;
                Settings.scale = (float)scale.Value;
                Settings.SaveSettings();
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
                Settings.GenerateNewSettingsFile();
                Settings.LoadSettings();
                stormworksVehicleFolderPath.Text = Settings.vehicleFolderPath;
                stormworksVehicleName.Text = Settings.vehicleOutputName;
                doBackups.Checked = Settings.doBackups;
                backupCount.Value = Settings.backupCount;
                scale.Value = (decimal)Settings.scale;
            });

            int S(int value) {
                return (int)(value * Scale);
            }
        }
    }
}