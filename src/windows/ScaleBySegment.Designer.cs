namespace ImageConverter {
    partial class ScaleBySegment {
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
            this.ImageBox = new System.Windows.Forms.PictureBox();
            this.Apply = new System.Windows.Forms.Button();
            this.LengthUDC = new System.Windows.Forms.NumericUpDown();
            this.LengthLabel = new System.Windows.Forms.Label();
            this.ResetLine = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LengthUDC)).BeginInit();
            this.SuspendLayout();
            // 
            // ImageBox
            // 
            this.ImageBox.Location = new System.Drawing.Point(0, 0);
            this.ImageBox.Name = "ImageBox";
            this.ImageBox.Size = new System.Drawing.Size(100, 50);
            this.ImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ImageBox.TabIndex = 0;
            this.ImageBox.TabStop = false;
            this.ImageBox.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            this.ImageBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseCursorDown);
            this.ImageBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseCursorMove);
            this.ImageBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MouseCursorUp);
            // 
            // Apply
            // 
            this.Apply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.Apply.Enabled = false;
            this.Apply.FlatAppearance.BorderSize = 0;
            this.Apply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Apply.Font = new System.Drawing.Font("Arial", 15F);
            this.Apply.ForeColor = System.Drawing.Color.Gainsboro;
            this.Apply.Location = new System.Drawing.Point(12, 41);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(175, 46);
            this.Apply.TabIndex = 8;
            this.Apply.Text = "Apply";
            this.Apply.UseVisualStyleBackColor = false;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // LengthUDC
            // 
            this.LengthUDC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.LengthUDC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LengthUDC.Enabled = false;
            this.LengthUDC.Font = new System.Drawing.Font("Arial", 10F);
            this.LengthUDC.ForeColor = System.Drawing.Color.Gainsboro;
            this.LengthUDC.Location = new System.Drawing.Point(72, 12);
            this.LengthUDC.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.LengthUDC.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.LengthUDC.Name = "LengthUDC";
            this.LengthUDC.Size = new System.Drawing.Size(115, 23);
            this.LengthUDC.TabIndex = 10;
            this.LengthUDC.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.LengthUDC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LengthKeyDown);
            // 
            // LengthLabel
            // 
            this.LengthLabel.AutoSize = true;
            this.LengthLabel.Font = new System.Drawing.Font("Arial", 10.5F);
            this.LengthLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.LengthLabel.Location = new System.Drawing.Point(6, 13);
            this.LengthLabel.Name = "LengthLabel";
            this.LengthLabel.Size = new System.Drawing.Size(65, 21);
            this.LengthLabel.TabIndex = 9;
            this.LengthLabel.Text = "Length";
            // 
            // ResetLine
            // 
            this.ResetLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.ResetLine.Enabled = false;
            this.ResetLine.FlatAppearance.BorderSize = 0;
            this.ResetLine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ResetLine.Font = new System.Drawing.Font("Arial", 15F);
            this.ResetLine.ForeColor = System.Drawing.Color.Gainsboro;
            this.ResetLine.Location = new System.Drawing.Point(12, 93);
            this.ResetLine.Name = "ResetLine";
            this.ResetLine.Size = new System.Drawing.Size(175, 46);
            this.ResetLine.TabIndex = 11;
            this.ResetLine.Text = "Reset";
            this.ResetLine.UseVisualStyleBackColor = false;
            this.ResetLine.Click += new System.EventHandler(this.ResetLine_Click);
            // 
            // ScaleBySegment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ResetLine);
            this.Controls.Add(this.LengthUDC);
            this.Controls.Add(this.LengthLabel);
            this.Controls.Add(this.Apply);
            this.Controls.Add(this.ImageBox);
            this.Name = "ScaleBySegment";
            this.Text = "Scale By Segment";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ScaleBySegment_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ScaleBySegment_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ScaleBySegment_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.ImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LengthUDC)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox ImageBox;
        private System.Windows.Forms.Button Apply;
        private System.Windows.Forms.NumericUpDown LengthUDC;
        private System.Windows.Forms.Label LengthLabel;
        private System.Windows.Forms.Button ResetLine;
    }
}