namespace ImageConverter.src.windows {
    partial class UpdateCheck {
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
            this.StatusLabel = new System.Windows.Forms.Label();
            this.OpenLatestRelease = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.StatusLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.StatusLabel.Location = new System.Drawing.Point(0, 3);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(95, 18);
            this.StatusLabel.TabIndex = 0;
            this.StatusLabel.Text = "Please wait...";
            // 
            // OpenLatestRelease
            // 
            this.OpenLatestRelease.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.OpenLatestRelease.FlatAppearance.BorderSize = 0;
            this.OpenLatestRelease.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenLatestRelease.Font = new System.Drawing.Font("Arial", 11F);
            this.OpenLatestRelease.ForeColor = System.Drawing.Color.Gainsboro;
            this.OpenLatestRelease.Location = new System.Drawing.Point(4, 26);
            this.OpenLatestRelease.Name = "OpenLatestRelease";
            this.OpenLatestRelease.Size = new System.Drawing.Size(265, 38);
            this.OpenLatestRelease.TabIndex = 20;
            this.OpenLatestRelease.Text = "Open Latest Release";
            this.OpenLatestRelease.UseVisualStyleBackColor = false;
            this.OpenLatestRelease.Click += new System.EventHandler(this.OpenLatestRelease_Click);
            // 
            // UpdateCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(273, 69);
            this.Controls.Add(this.OpenLatestRelease);
            this.Controls.Add(this.StatusLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateCheck";
            this.Text = "Updates";
            this.Load += new System.EventHandler(this.UpdateCheck_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Button OpenLatestRelease;
    }
}