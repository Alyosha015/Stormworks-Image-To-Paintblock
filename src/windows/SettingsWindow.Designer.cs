
namespace ImageConverter {
    partial class SettingsWindow {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.VehicleFolderPath = new System.Windows.Forms.TextBox();
            this.VehicleOutputName = new System.Windows.Forms.TextBox();
            this.UseImageNameAsVehicleName = new System.Windows.Forms.CheckBox();
            this.UseImageNameLabel = new System.Windows.Forms.Label();
            this.BackupLabel = new System.Windows.Forms.Label();
            this.Backup = new System.Windows.Forms.CheckBox();
            this.BackupCountLabel = new System.Windows.Forms.Label();
            this.CenterWindowPosLabel = new System.Windows.Forms.Label();
            this.MaxBackups = new System.Windows.Forms.NumericUpDown();
            this.OpenBackups = new System.Windows.Forms.Button();
            this.Close = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.Reset = new System.Windows.Forms.Button();
            this.SaveWindowPos = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.MaxBackups)).BeginInit();
            this.SuspendLayout();
            // 
            // VehicleFolderPath
            // 
            this.VehicleFolderPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.VehicleFolderPath.Font = new System.Drawing.Font("Arial", 12F);
            this.VehicleFolderPath.ForeColor = System.Drawing.Color.Gainsboro;
            this.VehicleFolderPath.Location = new System.Drawing.Point(5, 5);
            this.VehicleFolderPath.Name = "VehicleFolderPath";
            this.VehicleFolderPath.Size = new System.Drawing.Size(375, 30);
            this.VehicleFolderPath.TabIndex = 0;
            this.VehicleFolderPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.VehicleFolderPathKeyDown);
            // 
            // VehicleOutputName
            // 
            this.VehicleOutputName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.VehicleOutputName.Font = new System.Drawing.Font("Arial", 12F);
            this.VehicleOutputName.ForeColor = System.Drawing.Color.Gainsboro;
            this.VehicleOutputName.Location = new System.Drawing.Point(5, 40);
            this.VehicleOutputName.Name = "VehicleOutputName";
            this.VehicleOutputName.Size = new System.Drawing.Size(135, 30);
            this.VehicleOutputName.TabIndex = 1;
            this.VehicleOutputName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.VehicleOutputNameKeyDown);
            // 
            // UseImageNameAsVehicleName
            // 
            this.UseImageNameAsVehicleName.AutoSize = true;
            this.UseImageNameAsVehicleName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.UseImageNameAsVehicleName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UseImageNameAsVehicleName.ForeColor = System.Drawing.Color.Gainsboro;
            this.UseImageNameAsVehicleName.Location = new System.Drawing.Point(365, 49);
            this.UseImageNameAsVehicleName.Name = "UseImageNameAsVehicleName";
            this.UseImageNameAsVehicleName.Size = new System.Drawing.Size(14, 13);
            this.UseImageNameAsVehicleName.TabIndex = 11;
            this.UseImageNameAsVehicleName.UseVisualStyleBackColor = false;
            this.UseImageNameAsVehicleName.CheckedChanged += new System.EventHandler(this.UseImageNameAsVehicleName_CheckedChanged);
            // 
            // UseImageNameLabel
            // 
            this.UseImageNameLabel.AutoSize = true;
            this.UseImageNameLabel.Font = new System.Drawing.Font("Arial", 9.5F);
            this.UseImageNameLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.UseImageNameLabel.Location = new System.Drawing.Point(140, 46);
            this.UseImageNameLabel.Name = "UseImageNameLabel";
            this.UseImageNameLabel.Size = new System.Drawing.Size(215, 18);
            this.UseImageNameLabel.TabIndex = 12;
            this.UseImageNameLabel.Text = "Use Image Filename Instead?";
            // 
            // BackupLabel
            // 
            this.BackupLabel.AutoSize = true;
            this.BackupLabel.Font = new System.Drawing.Font("Arial", 10F);
            this.BackupLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.BackupLabel.Location = new System.Drawing.Point(1, 75);
            this.BackupLabel.Name = "BackupLabel";
            this.BackupLabel.Size = new System.Drawing.Size(139, 19);
            this.BackupLabel.TabIndex = 13;
            this.BackupLabel.Text = "Backup Vehicles?";
            // 
            // Backup
            // 
            this.Backup.AutoSize = true;
            this.Backup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.Backup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Backup.ForeColor = System.Drawing.Color.Gainsboro;
            this.Backup.Location = new System.Drawing.Point(148, 78);
            this.Backup.Name = "Backup";
            this.Backup.Size = new System.Drawing.Size(14, 13);
            this.Backup.TabIndex = 14;
            this.Backup.UseVisualStyleBackColor = false;
            // 
            // BackupCountLabel
            // 
            this.BackupCountLabel.AutoSize = true;
            this.BackupCountLabel.Font = new System.Drawing.Font("Arial", 10F);
            this.BackupCountLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.BackupCountLabel.Location = new System.Drawing.Point(1, 103);
            this.BackupCountLabel.Name = "BackupCountLabel";
            this.BackupCountLabel.Size = new System.Drawing.Size(106, 19);
            this.BackupCountLabel.TabIndex = 15;
            this.BackupCountLabel.Text = "Max Backups";
            // 
            // CenterWindowPosLabel
            // 
            this.CenterWindowPosLabel.AutoSize = true;
            this.CenterWindowPosLabel.Font = new System.Drawing.Font("Arial", 7.5F);
            this.CenterWindowPosLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.CenterWindowPosLabel.Location = new System.Drawing.Point(1, 132);
            this.CenterWindowPosLabel.Name = "CenterWindowPosLabel";
            this.CenterWindowPosLabel.Size = new System.Drawing.Size(148, 16);
            this.CenterWindowPosLabel.TabIndex = 16;
            this.CenterWindowPosLabel.Text = "Load Window Centered?";
            // 
            // MaxBackups
            // 
            this.MaxBackups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.MaxBackups.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MaxBackups.Font = new System.Drawing.Font("Arial", 10F);
            this.MaxBackups.ForeColor = System.Drawing.Color.Gainsboro;
            this.MaxBackups.Location = new System.Drawing.Point(113, 100);
            this.MaxBackups.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.MaxBackups.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MaxBackups.Name = "MaxBackups";
            this.MaxBackups.Size = new System.Drawing.Size(50, 23);
            this.MaxBackups.TabIndex = 17;
            this.MaxBackups.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MaxBackups.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MaxBackupsKeyDown);
            // 
            // OpenBackups
            // 
            this.OpenBackups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.OpenBackups.FlatAppearance.BorderSize = 0;
            this.OpenBackups.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenBackups.Font = new System.Drawing.Font("Arial", 11F);
            this.OpenBackups.ForeColor = System.Drawing.Color.Gainsboro;
            this.OpenBackups.Location = new System.Drawing.Point(169, 73);
            this.OpenBackups.Name = "OpenBackups";
            this.OpenBackups.Size = new System.Drawing.Size(211, 47);
            this.OpenBackups.TabIndex = 19;
            this.OpenBackups.Text = "Open Backups Folder";
            this.OpenBackups.UseVisualStyleBackColor = false;
            this.OpenBackups.Click += new System.EventHandler(this.OpenBackupsClick);
            // 
            // Close
            // 
            this.Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.Close.FlatAppearance.BorderSize = 0;
            this.Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Close.Font = new System.Drawing.Font("Arial", 10F);
            this.Close.ForeColor = System.Drawing.Color.Gainsboro;
            this.Close.Location = new System.Drawing.Point(169, 125);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(67, 28);
            this.Close.TabIndex = 20;
            this.Close.Text = "Close";
            this.Close.UseVisualStyleBackColor = false;
            this.Close.Click += new System.EventHandler(this.CloseWindow);
            // 
            // Save
            // 
            this.Save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.Save.FlatAppearance.BorderSize = 0;
            this.Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Save.Font = new System.Drawing.Font("Arial", 10F);
            this.Save.ForeColor = System.Drawing.Color.Gainsboro;
            this.Save.Location = new System.Drawing.Point(241, 125);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(67, 28);
            this.Save.TabIndex = 21;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = false;
            this.Save.Click += new System.EventHandler(this.SaveSettings);
            // 
            // Reset
            // 
            this.Reset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.Reset.FlatAppearance.BorderSize = 0;
            this.Reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Reset.Font = new System.Drawing.Font("Arial", 10F);
            this.Reset.ForeColor = System.Drawing.Color.Gainsboro;
            this.Reset.Location = new System.Drawing.Point(313, 125);
            this.Reset.Name = "Reset";
            this.Reset.Size = new System.Drawing.Size(67, 28);
            this.Reset.TabIndex = 22;
            this.Reset.Text = "Reset";
            this.Reset.UseVisualStyleBackColor = false;
            this.Reset.Click += new System.EventHandler(this.ResetSettings);
            // 
            // SaveWindowPos
            // 
            this.SaveWindowPos.AutoSize = true;
            this.SaveWindowPos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.SaveWindowPos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveWindowPos.ForeColor = System.Drawing.Color.Gainsboro;
            this.SaveWindowPos.Location = new System.Drawing.Point(148, 133);
            this.SaveWindowPos.Name = "SaveWindowPos";
            this.SaveWindowPos.Size = new System.Drawing.Size(14, 13);
            this.SaveWindowPos.TabIndex = 23;
            this.SaveWindowPos.UseVisualStyleBackColor = false;
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(385, 159);
            this.Controls.Add(this.SaveWindowPos);
            this.Controls.Add(this.Reset);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.OpenBackups);
            this.Controls.Add(this.MaxBackups);
            this.Controls.Add(this.CenterWindowPosLabel);
            this.Controls.Add(this.BackupCountLabel);
            this.Controls.Add(this.Backup);
            this.Controls.Add(this.BackupLabel);
            this.Controls.Add(this.UseImageNameAsVehicleName);
            this.Controls.Add(this.UseImageNameLabel);
            this.Controls.Add(this.VehicleOutputName);
            this.Controls.Add(this.VehicleFolderPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SettingsWindow";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MaxBackups)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox VehicleFolderPath;
        private System.Windows.Forms.TextBox VehicleOutputName;
        private System.Windows.Forms.CheckBox UseImageNameAsVehicleName;
        private System.Windows.Forms.Label UseImageNameLabel;
        private System.Windows.Forms.Label BackupLabel;
        private System.Windows.Forms.CheckBox Backup;
        private System.Windows.Forms.Label BackupCountLabel;
        private System.Windows.Forms.Label CenterWindowPosLabel;
        private System.Windows.Forms.NumericUpDown MaxBackups;
        private System.Windows.Forms.Button OpenBackups;
        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button Reset;
        private System.Windows.Forms.CheckBox SaveWindowPos;
    }
}