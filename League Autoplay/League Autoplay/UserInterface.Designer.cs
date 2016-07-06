namespace League_Autoplay
{
    partial class UserInterface
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.aiPerformanceLabel = new System.Windows.Forms.Label();
            this.screenPerformanceLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.screenCaptureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // screenCaptureBox
            // 
            this.screenCaptureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.screenCaptureBox.Location = new System.Drawing.Point(365, 34);
            this.screenCaptureBox.Name = "screenCaptureBox";
            this.screenCaptureBox.Size = new System.Drawing.Size(141, 139);
            this.screenCaptureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.screenCaptureBox.TabIndex = 0;
            this.screenCaptureBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(417, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Display";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(195, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Artificial Intelligence Timer Performance:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Screen Analysis Performance:";
            // 
            // aiPerformanceLabel
            // 
            this.aiPerformanceLabel.AutoSize = true;
            this.aiPerformanceLabel.Location = new System.Drawing.Point(213, 18);
            this.aiPerformanceLabel.Name = "aiPerformanceLabel";
            this.aiPerformanceLabel.Size = new System.Drawing.Size(85, 13);
            this.aiPerformanceLabel.TabIndex = 4;
            this.aiPerformanceLabel.Text = "000 fps (000 ms)";
            // 
            // screenPerformanceLabel
            // 
            this.screenPerformanceLabel.AutoSize = true;
            this.screenPerformanceLabel.Location = new System.Drawing.Point(166, 34);
            this.screenPerformanceLabel.Name = "screenPerformanceLabel";
            this.screenPerformanceLabel.Size = new System.Drawing.Size(85, 13);
            this.screenPerformanceLabel.TabIndex = 5;
            this.screenPerformanceLabel.Text = "000 fps (000 ms)";
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(518, 301);
            this.Controls.Add(this.screenPerformanceLabel);
            this.Controls.Add(this.aiPerformanceLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.screenCaptureBox);
            this.Name = "UserInterface";
            this.Text = "League Autoplay";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.screenCaptureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox screenCaptureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label aiPerformanceLabel;
        private System.Windows.Forms.Label screenPerformanceLabel;
    }
}

