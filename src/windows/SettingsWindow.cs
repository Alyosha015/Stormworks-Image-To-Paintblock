﻿using ImageConverter.src.windows;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageConverter {
    public partial class SettingsWindow : Form {
        public SettingsWindow() {
            InitializeComponent();
        }

        private void SettingsWindow_Load(object sender, EventArgs e) {
            Icon = Icon.FromHandle(Properties.Resources.IconInverted.GetHicon());
            Location = new Point(Settings.xPos, Settings.yPos);
            LoadSettings();
        }

        private void UseImageNameAsVehicleName_CheckedChanged(object sender, EventArgs e) {
            VehicleOutputName.Enabled = !UseImageNameAsVehicleName.Checked;
        }

        private void OpenBackupsClick(object sender, EventArgs e) {
            Backups.OpenBackups();
        }

        private void CloseWindow(object sender, EventArgs e) {
            Close();
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

        private void CheckForUpdates_Click(object sender, EventArgs e) {
            UpdateCheck updateCheckWindow = new UpdateCheck();
            updateCheckWindow.ShowDialog();
        }

        private void LoadSettings() {
            VehicleFolderPath.Text = Settings.vehicleFolderPath;
            VehicleOutputName.Text = Settings.vehicleOutputName;
            UseImageNameAsVehicleName.Checked = Settings.useImageNameAsVehicleName;
            VehicleOutputName.Enabled = !Settings.useImageNameAsVehicleName;
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
