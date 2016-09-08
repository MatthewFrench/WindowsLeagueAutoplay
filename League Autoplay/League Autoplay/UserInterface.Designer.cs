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
            this.label4 = new System.Windows.Forms.Label();
            this.aiFpsBox = new System.Windows.Forms.ComboBox();
            this.updateDisplayCheckBox = new System.Windows.Forms.CheckBox();
            this.debugBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.screenCaptureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.debugBox)).BeginInit();
            this.SuspendLayout();
            // 
            // screenCaptureBox
            // 
            this.screenCaptureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.screenCaptureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.screenCaptureBox.Location = new System.Drawing.Point(297, 86);
            this.screenCaptureBox.Name = "screenCaptureBox";
            this.screenCaptureBox.Size = new System.Drawing.Size(209, 203);
            this.screenCaptureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.screenCaptureBox.TabIndex = 0;
            this.screenCaptureBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(376, 34);
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(175, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Set Intelligence Update Speed (fps)";
            // 
            // aiFpsBox
            // 
            this.aiFpsBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.aiFpsBox.FormattingEnabled = true;
            this.aiFpsBox.Items.AddRange(new object[] {
            "2",
            "5",
            "10",
            "15",
            "20",
            "30",
            "60",
            "120"});
            this.aiFpsBox.Location = new System.Drawing.Point(193, 64);
            this.aiFpsBox.Name = "aiFpsBox";
            this.aiFpsBox.Size = new System.Drawing.Size(58, 21);
            this.aiFpsBox.TabIndex = 7;
            this.aiFpsBox.SelectedIndexChanged += new System.EventHandler(this.aiFpsBox_SelectedIndexChanged);
            // 
            // updateDisplayCheckBox
            // 
            this.updateDisplayCheckBox.AutoSize = true;
            this.updateDisplayCheckBox.Location = new System.Drawing.Point(342, 63);
            this.updateDisplayCheckBox.Name = "updateDisplayCheckBox";
            this.updateDisplayCheckBox.Size = new System.Drawing.Size(130, 17);
            this.updateDisplayCheckBox.TabIndex = 8;
            this.updateDisplayCheckBox.Text = "Update Display Image";
            this.updateDisplayCheckBox.UseVisualStyleBackColor = true;
            this.updateDisplayCheckBox.CheckedChanged += new System.EventHandler(this.updateDisplayCheckBox_CheckedChanged);
            // 
            // debugBox
            // 
            this.debugBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.debugBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.debugBox.Location = new System.Drawing.Point(82, 91);
            this.debugBox.Name = "debugBox";
            this.debugBox.Size = new System.Drawing.Size(209, 203);
            this.debugBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.debugBox.TabIndex = 9;
            this.debugBox.TabStop = false;
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(518, 301);
            this.Controls.Add(this.debugBox);
            this.Controls.Add(this.updateDisplayCheckBox);
            this.Controls.Add(this.aiFpsBox);
            this.Controls.Add(this.label4);
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
            ((System.ComponentModel.ISupportInitialize)(this.debugBox)).EndInit();
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox aiFpsBox;
        private System.Windows.Forms.CheckBox updateDisplayCheckBox;
        private System.Windows.Forms.PictureBox debugBox;
    }
}

