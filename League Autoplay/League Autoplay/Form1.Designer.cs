namespace League_Autoplay
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.screenCaptureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.screenCaptureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // screenCaptureBox
            // 
            this.screenCaptureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.screenCaptureBox.Location = new System.Drawing.Point(365, 12);
            this.screenCaptureBox.Name = "screenCaptureBox";
            this.screenCaptureBox.Size = new System.Drawing.Size(141, 139);
            this.screenCaptureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.screenCaptureBox.TabIndex = 0;
            this.screenCaptureBox.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(518, 301);
            this.Controls.Add(this.screenCaptureBox);
            this.Name = "Form1";
            this.Text = "League Autoplay";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.screenCaptureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox screenCaptureBox;
    }
}

