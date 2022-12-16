using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageConverter {
    public partial class SettingsWindow : Form {
        public SettingsWindow() {
            InitializeComponent();
        }

        private void SettingsWindow_Load(object sender, EventArgs e) {
            float dpi = this.CreateGraphics().DpiX;
            this.Icon = Icon.FromHandle(ImageConverter.Properties.Resources.IconInverted.GetHicon());
            this.Location = new Point(Settings.xPos,Settings.yPos);
            LoadSettings();
            if(dpi==96) {
                CenterWindowPosLabel.Text = "Load Window Centered";
            }
        }

        private void UseImageNameAsVehicleName_CheckedChanged(object sender, EventArgs e) {
            VehicleOutputName.Enabled = !UseImageNameAsVehicleName.Checked;
        }

        private void OpenBackupsClick(object sender, EventArgs e) {
            Backups.OpenBackups();
        }

        private void CloseWindow(object sender, EventArgs e) {
            this.Close();
        }

        private void SaveSettings(object sender, EventArgs e) {
            UpdateSettings();
            Settings.SaveSettings();
        }

        private void ResetSettings(object sender, EventArgs e) {
            Settings.GenerateNewSettings();
            Settings.LoadSettings();
            LoadSettings();
        }

        private void LoadSettings() {
            VehicleFolderPath.Text = Settings.vehicleFolderPath;
            VehicleOutputName.Text = Settings.vehicleOutputName;
            UseImageNameAsVehicleName.Checked = Settings.useImageNameAsVehicleName;
            if (Settings.useImageNameAsVehicleName) VehicleOutputName.Enabled = false;
            else VehicleOutputName.Enabled = true;
            Backup.Checked = Settings.doBackups;
            MaxBackups.Value = Settings.backupCount;
            SaveWindowPos.Checked = !Settings.saveAndLoadPos;
        }

        private void UpdateSettings() {
            Settings.vehicleFolderPath = VehicleFolderPath.Text;
            Settings.vehicleOutputName = VehicleOutputName.Text;
            Settings.useImageNameAsVehicleName = UseImageNameAsVehicleName.Checked;
            Settings.doBackups = Backup.Checked;
            Settings.backupCount = (int)MaxBackups.Value;
            Settings.saveAndLoadPos = !SaveWindowPos.Checked;
        }

        private void VehicleFolderPathKeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) e.SuppressKeyPress = true;
        }

        private void VehicleOutputNameKeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) e.SuppressKeyPress = true;
        }

        private void MaxBackupsKeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) e.SuppressKeyPress = true;
        }
    }
}