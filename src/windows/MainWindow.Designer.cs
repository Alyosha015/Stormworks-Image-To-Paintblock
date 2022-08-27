﻿
namespace ImageConverter {
    partial class MainWindow {
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
            this.ModeSelect = new System.Windows.Forms.ComboBox();
            this.SettingsPanelBackground = new System.Windows.Forms.Panel();
            this.Darken = new System.Windows.Forms.CheckBox();
            this.DarkenLabel = new System.Windows.Forms.Label();
            this.Threshold = new System.Windows.Forms.NumericUpDown();
            this.ThresholdPanel = new System.Windows.Forms.Label();
            this.Glow = new System.Windows.Forms.CheckBox();
            this.Optimize = new System.Windows.Forms.CheckBox();
            this.GlowLabel = new System.Windows.Forms.Label();
            this.OptimizeLabel = new System.Windows.Forms.Label();
            this.Height = new System.Windows.Forms.NumericUpDown();
            this.HeightLabel = new System.Windows.Forms.Label();
            this.Width = new System.Windows.Forms.NumericUpDown();
            this.WidthLabel = new System.Windows.Forms.Label();
            this.ControlPanelBackground = new System.Windows.Forms.Panel();
            this.SelectBackground = new System.Windows.Forms.Button();
            this.SelectGlow = new System.Windows.Forms.Button();
            this.OpenSettings = new System.Windows.Forms.Button();
            this.GenerateXML = new System.Windows.Forms.Button();
            this.SelectFile = new System.Windows.Forms.Button();
            this.SettingsPanelBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Threshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Height)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Width)).BeginInit();
            this.ControlPanelBackground.SuspendLayout();
            this.SuspendLayout();
            // 
            // ModeSelect
            // 
            this.ModeSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.ModeSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ModeSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ModeSelect.Font = new System.Drawing.Font("Arial", 10.5F);
            this.ModeSelect.ForeColor = System.Drawing.Color.Gainsboro;
            this.ModeSelect.FormattingEnabled = true;
            this.ModeSelect.ItemHeight = 19;
            this.ModeSelect.Items.AddRange(new object[] {
            "Select Mode",
            "Custom Dimensions",
            "Custom Width",
            "Custom Height",
            "Don\'t Resize"});
            this.ModeSelect.Location = new System.Drawing.Point(5, 5);
            this.ModeSelect.Name = "ModeSelect";
            this.ModeSelect.Size = new System.Drawing.Size(185, 27);
            this.ModeSelect.TabIndex = 1;
            this.ModeSelect.SelectedIndexChanged += new System.EventHandler(this.ModeChanged);
            // 
            // SettingsPanelBackground
            // 
            this.SettingsPanelBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.SettingsPanelBackground.Controls.Add(this.Darken);
            this.SettingsPanelBackground.Controls.Add(this.DarkenLabel);
            this.SettingsPanelBackground.Controls.Add(this.Threshold);
            this.SettingsPanelBackground.Controls.Add(this.ThresholdPanel);
            this.SettingsPanelBackground.Controls.Add(this.Glow);
            this.SettingsPanelBackground.Controls.Add(this.Optimize);
            this.SettingsPanelBackground.Controls.Add(this.GlowLabel);
            this.SettingsPanelBackground.Controls.Add(this.OptimizeLabel);
            this.SettingsPanelBackground.Controls.Add(this.Height);
            this.SettingsPanelBackground.Controls.Add(this.HeightLabel);
            this.SettingsPanelBackground.Controls.Add(this.Width);
            this.SettingsPanelBackground.Controls.Add(this.WidthLabel);
            this.SettingsPanelBackground.ForeColor = System.Drawing.Color.Black;
            this.SettingsPanelBackground.Location = new System.Drawing.Point(5, 39);
            this.SettingsPanelBackground.Name = "SettingsPanelBackground";
            this.SettingsPanelBackground.Size = new System.Drawing.Size(185, 129);
            this.SettingsPanelBackground.TabIndex = 2;
            this.SettingsPanelBackground.TabStop = true;
            // 
            // Darken
            // 
            this.Darken.AutoSize = true;
            this.Darken.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.Darken.Checked = true;
            this.Darken.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Darken.Enabled = false;
            this.Darken.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Darken.ForeColor = System.Drawing.Color.Gainsboro;
            this.Darken.Location = new System.Drawing.Point(78, 76);
            this.Darken.Name = "Darken";
            this.Darken.Size = new System.Drawing.Size(14, 13);
            this.Darken.TabIndex = 9;
            this.Darken.UseVisualStyleBackColor = false;
            this.Darken.Visible = false;
            // 
            // DarkenLabel
            // 
            this.DarkenLabel.AutoSize = true;
            this.DarkenLabel.Font = new System.Drawing.Font("Arial", 11F);
            this.DarkenLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.DarkenLabel.Location = new System.Drawing.Point(-1, 71);
            this.DarkenLabel.Name = "DarkenLabel";
            this.DarkenLabel.Size = new System.Drawing.Size(81, 22);
            this.DarkenLabel.TabIndex = 10;
            this.DarkenLabel.Text = "Darken?";
            this.DarkenLabel.Visible = false;
            // 
            // Threshold
            // 
            this.Threshold.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.Threshold.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Threshold.Enabled = false;
            this.Threshold.Font = new System.Drawing.Font("Arial", 10F);
            this.Threshold.ForeColor = System.Drawing.Color.Gainsboro;
            this.Threshold.Location = new System.Drawing.Point(90, 100);
            this.Threshold.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.Threshold.Name = "Threshold";
            this.Threshold.Size = new System.Drawing.Size(90, 23);
            this.Threshold.TabIndex = 5;
            this.Threshold.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ThresholdKeyDown);
            // 
            // ThresholdPanel
            // 
            this.ThresholdPanel.AutoSize = true;
            this.ThresholdPanel.Font = new System.Drawing.Font("Arial", 10.5F);
            this.ThresholdPanel.ForeColor = System.Drawing.Color.Gainsboro;
            this.ThresholdPanel.Location = new System.Drawing.Point(-1, 101);
            this.ThresholdPanel.Name = "ThresholdPanel";
            this.ThresholdPanel.Size = new System.Drawing.Size(91, 21);
            this.ThresholdPanel.TabIndex = 8;
            this.ThresholdPanel.Text = "Threshold";
            // 
            // Glow
            // 
            this.Glow.AutoSize = true;
            this.Glow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.Glow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Glow.ForeColor = System.Drawing.Color.Gainsboro;
            this.Glow.Location = new System.Drawing.Point(165, 76);
            this.Glow.Name = "Glow";
            this.Glow.Size = new System.Drawing.Size(14, 13);
            this.Glow.TabIndex = 6;
            this.Glow.UseVisualStyleBackColor = false;
            this.Glow.CheckedChanged += new System.EventHandler(this.GlowCheckChanged);
            // 
            // Optimize
            // 
            this.Optimize.AutoSize = true;
            this.Optimize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.Optimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Optimize.ForeColor = System.Drawing.Color.Gainsboro;
            this.Optimize.Location = new System.Drawing.Point(81, 76);
            this.Optimize.Name = "Optimize";
            this.Optimize.Size = new System.Drawing.Size(14, 13);
            this.Optimize.TabIndex = 4;
            this.Optimize.UseVisualStyleBackColor = false;
            this.Optimize.CheckedChanged += new System.EventHandler(this.OptimizeCheckChange);
            // 
            // GlowLabel
            // 
            this.GlowLabel.AutoSize = true;
            this.GlowLabel.Font = new System.Drawing.Font("Arial", 11F);
            this.GlowLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.GlowLabel.Location = new System.Drawing.Point(104, 71);
            this.GlowLabel.Name = "GlowLabel";
            this.GlowLabel.Size = new System.Drawing.Size(64, 22);
            this.GlowLabel.TabIndex = 5;
            this.GlowLabel.Text = "Glow?";
            // 
            // OptimizeLabel
            // 
            this.OptimizeLabel.AutoSize = true;
            this.OptimizeLabel.Font = new System.Drawing.Font("Arial", 10F);
            this.OptimizeLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.OptimizeLabel.Location = new System.Drawing.Point(-1, 73);
            this.OptimizeLabel.Name = "OptimizeLabel";
            this.OptimizeLabel.Size = new System.Drawing.Size(81, 19);
            this.OptimizeLabel.TabIndex = 4;
            this.OptimizeLabel.Text = "Optimize?";
            // 
            // Height
            // 
            this.Height.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.Height.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Height.Enabled = false;
            this.Height.Font = new System.Drawing.Font("Arial", 10F);
            this.Height.ForeColor = System.Drawing.Color.Gainsboro;
            this.Height.Location = new System.Drawing.Point(60, 40);
            this.Height.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.Height.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Height.Name = "Height";
            this.Height.Size = new System.Drawing.Size(120, 23);
            this.Height.TabIndex = 3;
            this.Height.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Height.ValueChanged += new System.EventHandler(this.Height_ValueChanged);
            this.Height.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HeightKeyDown);
            // 
            // HeightLabel
            // 
            this.HeightLabel.AutoSize = true;
            this.HeightLabel.Font = new System.Drawing.Font("Arial", 10.5F);
            this.HeightLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.HeightLabel.Location = new System.Drawing.Point(-1, 40);
            this.HeightLabel.Name = "HeightLabel";
            this.HeightLabel.Size = new System.Drawing.Size(62, 21);
            this.HeightLabel.TabIndex = 2;
            this.HeightLabel.Text = "Height";
            // 
            // Width
            // 
            this.Width.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.Width.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Width.Enabled = false;
            this.Width.Font = new System.Drawing.Font("Arial", 10F);
            this.Width.ForeColor = System.Drawing.Color.Gainsboro;
            this.Width.Location = new System.Drawing.Point(60, 7);
            this.Width.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.Width.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Width.Name = "Width";
            this.Width.Size = new System.Drawing.Size(120, 23);
            this.Width.TabIndex = 2;
            this.Width.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Width.ValueChanged += new System.EventHandler(this.Width_ValueChanged);
            this.Width.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WidthKeyDown);
            // 
            // WidthLabel
            // 
            this.WidthLabel.AutoSize = true;
            this.WidthLabel.Font = new System.Drawing.Font("Arial", 10.5F);
            this.WidthLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.WidthLabel.Location = new System.Drawing.Point(-1, 8);
            this.WidthLabel.Name = "WidthLabel";
            this.WidthLabel.Size = new System.Drawing.Size(56, 21);
            this.WidthLabel.TabIndex = 0;
            this.WidthLabel.Text = "Width";
            // 
            // ControlPanelBackground
            // 
            this.ControlPanelBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ControlPanelBackground.Controls.Add(this.SelectBackground);
            this.ControlPanelBackground.Controls.Add(this.SelectGlow);
            this.ControlPanelBackground.Controls.Add(this.OpenSettings);
            this.ControlPanelBackground.Controls.Add(this.GenerateXML);
            this.ControlPanelBackground.Controls.Add(this.SelectFile);
            this.ControlPanelBackground.Location = new System.Drawing.Point(195, 5);
            this.ControlPanelBackground.Name = "ControlPanelBackground";
            this.ControlPanelBackground.Size = new System.Drawing.Size(185, 163);
            this.ControlPanelBackground.TabIndex = 3;
            // 
            // SelectBackground
            // 
            this.SelectBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.SelectBackground.FlatAppearance.BorderSize = 0;
            this.SelectBackground.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectBackground.Font = new System.Drawing.Font("Arial", 9F);
            this.SelectBackground.ForeColor = System.Drawing.Color.Gainsboro;
            this.SelectBackground.Location = new System.Drawing.Point(5, 4);
            this.SelectBackground.Name = "SelectBackground";
            this.SelectBackground.Size = new System.Drawing.Size(105, 47);
            this.SelectBackground.TabIndex = 10;
            this.SelectBackground.Text = "Select Background";
            this.SelectBackground.UseVisualStyleBackColor = false;
            this.SelectBackground.Visible = false;
            this.SelectBackground.Click += new System.EventHandler(this.SelectBackgroundClick);
            // 
            // SelectGlow
            // 
            this.SelectGlow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.SelectGlow.FlatAppearance.BorderSize = 0;
            this.SelectGlow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectGlow.Font = new System.Drawing.Font("Arial", 9F);
            this.SelectGlow.ForeColor = System.Drawing.Color.Gainsboro;
            this.SelectGlow.Location = new System.Drawing.Point(115, 4);
            this.SelectGlow.Name = "SelectGlow";
            this.SelectGlow.Size = new System.Drawing.Size(65, 47);
            this.SelectGlow.TabIndex = 9;
            this.SelectGlow.Text = "Select Glow";
            this.SelectGlow.UseVisualStyleBackColor = false;
            this.SelectGlow.Visible = false;
            this.SelectGlow.Click += new System.EventHandler(this.SelectGlowClick);
            // 
            // OpenSettings
            // 
            this.OpenSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.OpenSettings.FlatAppearance.BorderSize = 0;
            this.OpenSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenSettings.Font = new System.Drawing.Font("Arial", 13F);
            this.OpenSettings.ForeColor = System.Drawing.Color.Gainsboro;
            this.OpenSettings.Location = new System.Drawing.Point(5, 110);
            this.OpenSettings.Name = "OpenSettings";
            this.OpenSettings.Size = new System.Drawing.Size(175, 47);
            this.OpenSettings.TabIndex = 8;
            this.OpenSettings.Text = "Open Settings";
            this.OpenSettings.UseVisualStyleBackColor = false;
            this.OpenSettings.Click += new System.EventHandler(this.OpenSettingsClick);
            // 
            // GenerateXML
            // 
            this.GenerateXML.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.GenerateXML.Enabled = false;
            this.GenerateXML.FlatAppearance.BorderSize = 0;
            this.GenerateXML.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GenerateXML.Font = new System.Drawing.Font("Arial", 13F);
            this.GenerateXML.ForeColor = System.Drawing.Color.Gainsboro;
            this.GenerateXML.Location = new System.Drawing.Point(5, 57);
            this.GenerateXML.Name = "GenerateXML";
            this.GenerateXML.Size = new System.Drawing.Size(175, 47);
            this.GenerateXML.TabIndex = 7;
            this.GenerateXML.Text = "Generate XML";
            this.GenerateXML.UseVisualStyleBackColor = false;
            this.GenerateXML.Click += new System.EventHandler(this.GenerateClick);
            // 
            // SelectFile
            // 
            this.SelectFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.SelectFile.FlatAppearance.BorderSize = 0;
            this.SelectFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectFile.Font = new System.Drawing.Font("Arial", 13F);
            this.SelectFile.ForeColor = System.Drawing.Color.Gainsboro;
            this.SelectFile.Location = new System.Drawing.Point(5, 4);
            this.SelectFile.Name = "SelectFile";
            this.SelectFile.Size = new System.Drawing.Size(175, 47);
            this.SelectFile.TabIndex = 0;
            this.SelectFile.Text = "Select File";
            this.SelectFile.UseVisualStyleBackColor = false;
            this.SelectFile.Click += new System.EventHandler(this.SelectFileClick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(385, 173);
            this.Controls.Add(this.ControlPanelBackground);
            this.Controls.Add(this.SettingsPanelBackground);
            this.Controls.Add(this.ModeSelect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "Image Converter";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnClose);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.SettingsPanelBackground.ResumeLayout(false);
            this.SettingsPanelBackground.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Threshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Height)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Width)).EndInit();
            this.ControlPanelBackground.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox ModeSelect;
        private System.Windows.Forms.Panel SettingsPanelBackground;
        private System.Windows.Forms.Label WidthLabel;
        private System.Windows.Forms.NumericUpDown Width;
        private System.Windows.Forms.NumericUpDown Height;
        private System.Windows.Forms.Label HeightLabel;
        private System.Windows.Forms.Label OptimizeLabel;
        private System.Windows.Forms.Label GlowLabel;
        private System.Windows.Forms.CheckBox Optimize;
        private System.Windows.Forms.CheckBox Glow;
        private System.Windows.Forms.Label ThresholdPanel;
        private System.Windows.Forms.NumericUpDown Threshold;
        private System.Windows.Forms.Panel ControlPanelBackground;
        private System.Windows.Forms.Button SelectFile;
        private System.Windows.Forms.Button GenerateXML;
        private System.Windows.Forms.Button OpenSettings;
        private System.Windows.Forms.Button SelectGlow;
        private System.Windows.Forms.CheckBox Darken;
        private System.Windows.Forms.Label DarkenLabel;
        private System.Windows.Forms.Button SelectBackground;
    }
}

