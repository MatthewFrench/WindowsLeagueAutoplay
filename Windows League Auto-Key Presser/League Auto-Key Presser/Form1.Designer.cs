namespace League_Auto_Key_Presser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.autoKeyOn = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.active1On = new System.Windows.Forms.CheckBox();
            this.active2On = new System.Windows.Forms.CheckBox();
            this.active5On = new System.Windows.Forms.CheckBox();
            this.active3On = new System.Windows.Forms.CheckBox();
            this.active6On = new System.Windows.Forms.CheckBox();
            this.active7On = new System.Windows.Forms.CheckBox();
            this.wardCheckbox = new System.Windows.Forms.CheckBox();
            this.qValueText = new System.Windows.Forms.TextBox();
            this.wValueText = new System.Windows.Forms.TextBox();
            this.eValueText = new System.Windows.Forms.TextBox();
            this.rValueText = new System.Windows.Forms.TextBox();
            this.activeValueText = new System.Windows.Forms.TextBox();
            this.activeKeyComboBox = new System.Windows.Forms.ComboBox();
            this.wardHopCheckBox = new System.Windows.Forms.CheckBox();
            this.wardHopKeyComboBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.qActivateWCheckBox = new System.Windows.Forms.CheckBox();
            this.qActivateECheckBox = new System.Windows.Forms.CheckBox();
            this.qActivateRCheckBox = new System.Windows.Forms.CheckBox();
            this.wActivateRCheckBox = new System.Windows.Forms.CheckBox();
            this.wActivateECheckBox = new System.Windows.Forms.CheckBox();
            this.wActivateQCheckBox = new System.Windows.Forms.CheckBox();
            this.eActivateRCheckBox = new System.Windows.Forms.CheckBox();
            this.eActivateWCheckBox = new System.Windows.Forms.CheckBox();
            this.eActivateQCheckBox = new System.Windows.Forms.CheckBox();
            this.rActivateECheckBox = new System.Windows.Forms.CheckBox();
            this.rActivateWCheckBox = new System.Windows.Forms.CheckBox();
            this.rActivateQCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(105, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Designed for League of Legends by Matt French";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(261, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Actives still work even when League Auto Press is Off";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Q -                            millisecond delay";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(184, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "W -                           millisecond delay";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(183, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "E -                            millisecond delay";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 143);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(184, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "R -                            millisecond delay";
            // 
            // autoKeyOn
            // 
            this.autoKeyOn.AutoSize = true;
            this.autoKeyOn.Checked = true;
            this.autoKeyOn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoKeyOn.Location = new System.Drawing.Point(18, 44);
            this.autoKeyOn.Name = "autoKeyOn";
            this.autoKeyOn.Size = new System.Drawing.Size(292, 17);
            this.autoKeyOn.TabIndex = 6;
            this.autoKeyOn.Text = "League Auto Press Keys On - It mashes the keys quickly";
            this.autoKeyOn.UseVisualStyleBackColor = true;
            this.autoKeyOn.CheckedChanged += new System.EventHandler(this.autoKeyOn_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 212);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(282, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Actives -                            millisecond delay, bound to key:";
            // 
            // active1On
            // 
            this.active1On.AutoSize = true;
            this.active1On.Location = new System.Drawing.Point(17, 236);
            this.active1On.Name = "active1On";
            this.active1On.Size = new System.Drawing.Size(89, 17);
            this.active1On.TabIndex = 8;
            this.active1On.Text = "Do #1 Active";
            this.active1On.UseVisualStyleBackColor = true;
            this.active1On.CheckedChanged += new System.EventHandler(this.active1On_CheckedChanged);
            // 
            // active2On
            // 
            this.active2On.AutoSize = true;
            this.active2On.Location = new System.Drawing.Point(18, 260);
            this.active2On.Name = "active2On";
            this.active2On.Size = new System.Drawing.Size(89, 17);
            this.active2On.TabIndex = 9;
            this.active2On.Text = "Do #2 Active";
            this.active2On.UseVisualStyleBackColor = true;
            this.active2On.CheckedChanged += new System.EventHandler(this.active2On_CheckedChanged);
            // 
            // active5On
            // 
            this.active5On.AutoSize = true;
            this.active5On.Location = new System.Drawing.Point(134, 257);
            this.active5On.Name = "active5On";
            this.active5On.Size = new System.Drawing.Size(89, 17);
            this.active5On.TabIndex = 11;
            this.active5On.Text = "Do #5 Active";
            this.active5On.UseVisualStyleBackColor = true;
            this.active5On.CheckedChanged += new System.EventHandler(this.active5On_CheckedChanged);
            // 
            // active3On
            // 
            this.active3On.AutoSize = true;
            this.active3On.Location = new System.Drawing.Point(134, 236);
            this.active3On.Name = "active3On";
            this.active3On.Size = new System.Drawing.Size(89, 17);
            this.active3On.TabIndex = 10;
            this.active3On.Text = "Do #3 Active";
            this.active3On.UseVisualStyleBackColor = true;
            this.active3On.CheckedChanged += new System.EventHandler(this.active3On_CheckedChanged);
            // 
            // active6On
            // 
            this.active6On.AutoSize = true;
            this.active6On.Location = new System.Drawing.Point(229, 236);
            this.active6On.Name = "active6On";
            this.active6On.Size = new System.Drawing.Size(89, 17);
            this.active6On.TabIndex = 12;
            this.active6On.Text = "Do #6 Active";
            this.active6On.UseVisualStyleBackColor = true;
            this.active6On.CheckedChanged += new System.EventHandler(this.active6On_CheckedChanged);
            // 
            // active7On
            // 
            this.active7On.AutoSize = true;
            this.active7On.Location = new System.Drawing.Point(229, 257);
            this.active7On.Name = "active7On";
            this.active7On.Size = new System.Drawing.Size(89, 17);
            this.active7On.TabIndex = 13;
            this.active7On.Text = "Do #7 Active";
            this.active7On.UseVisualStyleBackColor = true;
            this.active7On.CheckedChanged += new System.EventHandler(this.active7On_CheckedChanged);
            // 
            // wardCheckbox
            // 
            this.wardCheckbox.AutoSize = true;
            this.wardCheckbox.Location = new System.Drawing.Point(17, 285);
            this.wardCheckbox.Name = "wardCheckbox";
            this.wardCheckbox.Size = new System.Drawing.Size(160, 17);
            this.wardCheckbox.TabIndex = 14;
            this.wardCheckbox.Text = "Place ward every 6 seconds";
            this.wardCheckbox.UseVisualStyleBackColor = true;
            this.wardCheckbox.CheckedChanged += new System.EventHandler(this.wardCheckbox_CheckedChanged);
            // 
            // qValueText
            // 
            this.qValueText.Location = new System.Drawing.Point(34, 67);
            this.qValueText.Name = "qValueText";
            this.qValueText.Size = new System.Drawing.Size(73, 20);
            this.qValueText.TabIndex = 15;
            this.qValueText.Text = "10";
            this.qValueText.TextChanged += new System.EventHandler(this.qValueText_TextChanged);
            // 
            // wValueText
            // 
            this.wValueText.Location = new System.Drawing.Point(34, 92);
            this.wValueText.Name = "wValueText";
            this.wValueText.Size = new System.Drawing.Size(73, 20);
            this.wValueText.TabIndex = 16;
            this.wValueText.Text = "10";
            this.wValueText.TextChanged += new System.EventHandler(this.wValueText_TextChanged);
            // 
            // eValueText
            // 
            this.eValueText.Location = new System.Drawing.Point(34, 117);
            this.eValueText.Name = "eValueText";
            this.eValueText.Size = new System.Drawing.Size(73, 20);
            this.eValueText.TabIndex = 17;
            this.eValueText.Text = "10";
            this.eValueText.TextChanged += new System.EventHandler(this.eValueText_TextChanged);
            // 
            // rValueText
            // 
            this.rValueText.Location = new System.Drawing.Point(34, 140);
            this.rValueText.Name = "rValueText";
            this.rValueText.Size = new System.Drawing.Size(73, 20);
            this.rValueText.TabIndex = 18;
            this.rValueText.Text = "100";
            this.rValueText.TextChanged += new System.EventHandler(this.rValueText_TextChanged);
            // 
            // activeValueText
            // 
            this.activeValueText.Location = new System.Drawing.Point(61, 209);
            this.activeValueText.Name = "activeValueText";
            this.activeValueText.Size = new System.Drawing.Size(73, 20);
            this.activeValueText.TabIndex = 19;
            this.activeValueText.Text = "500";
            this.activeValueText.TextChanged += new System.EventHandler(this.activeValueText_TextChanged);
            // 
            // activeKeyComboBox
            // 
            this.activeKeyComboBox.DisplayMember = "E";
            this.activeKeyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.activeKeyComboBox.FormattingEnabled = true;
            this.activeKeyComboBox.Items.AddRange(new object[] {
            "Q",
            "W",
            "E",
            "R"});
            this.activeKeyComboBox.Location = new System.Drawing.Point(297, 209);
            this.activeKeyComboBox.MaxDropDownItems = 4;
            this.activeKeyComboBox.Name = "activeKeyComboBox";
            this.activeKeyComboBox.Size = new System.Drawing.Size(121, 21);
            this.activeKeyComboBox.TabIndex = 20;
            this.activeKeyComboBox.SelectedIndexChanged += new System.EventHandler(this.activeKeyComboBox_SelectedIndexChanged);
            // 
            // wardHopCheckBox
            // 
            this.wardHopCheckBox.AutoSize = true;
            this.wardHopCheckBox.Location = new System.Drawing.Point(17, 308);
            this.wardHopCheckBox.Name = "wardHopCheckBox";
            this.wardHopCheckBox.Size = new System.Drawing.Size(170, 17);
            this.wardHopCheckBox.TabIndex = 21;
            this.wardHopCheckBox.Text = "Ward hop (T Key) using ability:";
            this.wardHopCheckBox.UseVisualStyleBackColor = true;
            this.wardHopCheckBox.CheckedChanged += new System.EventHandler(this.wardHopCheckBox_CheckedChanged);
            // 
            // wardHopKeyComboBox
            // 
            this.wardHopKeyComboBox.DisplayMember = "E";
            this.wardHopKeyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.wardHopKeyComboBox.FormattingEnabled = true;
            this.wardHopKeyComboBox.Items.AddRange(new object[] {
            "Q",
            "W",
            "E",
            "R"});
            this.wardHopKeyComboBox.Location = new System.Drawing.Point(193, 306);
            this.wardHopKeyComboBox.MaxDropDownItems = 4;
            this.wardHopKeyComboBox.Name = "wardHopKeyComboBox";
            this.wardHopKeyComboBox.Size = new System.Drawing.Size(121, 21);
            this.wardHopKeyComboBox.TabIndex = 22;
            this.wardHopKeyComboBox.SelectedIndexChanged += new System.EventHandler(this.wardHopKeyComboBox_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(212, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Pre-activate:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(212, 95);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "Pre-activate:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(212, 120);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "Pre-activate:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(212, 143);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "Pre-activate:";
            // 
            // qActivateWCheckBox
            // 
            this.qActivateWCheckBox.AutoSize = true;
            this.qActivateWCheckBox.Location = new System.Drawing.Point(285, 69);
            this.qActivateWCheckBox.Name = "qActivateWCheckBox";
            this.qActivateWCheckBox.Size = new System.Drawing.Size(37, 17);
            this.qActivateWCheckBox.TabIndex = 27;
            this.qActivateWCheckBox.Text = "W";
            this.qActivateWCheckBox.UseVisualStyleBackColor = true;
            this.qActivateWCheckBox.CheckedChanged += new System.EventHandler(this.qActivateWCheckBox_CheckedChanged);
            // 
            // qActivateECheckBox
            // 
            this.qActivateECheckBox.AutoSize = true;
            this.qActivateECheckBox.Location = new System.Drawing.Point(325, 69);
            this.qActivateECheckBox.Name = "qActivateECheckBox";
            this.qActivateECheckBox.Size = new System.Drawing.Size(33, 17);
            this.qActivateECheckBox.TabIndex = 28;
            this.qActivateECheckBox.Text = "E";
            this.qActivateECheckBox.UseVisualStyleBackColor = true;
            this.qActivateECheckBox.CheckedChanged += new System.EventHandler(this.qActivateECheckBox_CheckedChanged);
            // 
            // qActivateRCheckBox
            // 
            this.qActivateRCheckBox.AutoSize = true;
            this.qActivateRCheckBox.Location = new System.Drawing.Point(368, 69);
            this.qActivateRCheckBox.Name = "qActivateRCheckBox";
            this.qActivateRCheckBox.Size = new System.Drawing.Size(34, 17);
            this.qActivateRCheckBox.TabIndex = 29;
            this.qActivateRCheckBox.Text = "R";
            this.qActivateRCheckBox.UseVisualStyleBackColor = true;
            this.qActivateRCheckBox.CheckedChanged += new System.EventHandler(this.qActivateRCheckBox_CheckedChanged);
            // 
            // wActivateRCheckBox
            // 
            this.wActivateRCheckBox.AutoSize = true;
            this.wActivateRCheckBox.Location = new System.Drawing.Point(368, 94);
            this.wActivateRCheckBox.Name = "wActivateRCheckBox";
            this.wActivateRCheckBox.Size = new System.Drawing.Size(34, 17);
            this.wActivateRCheckBox.TabIndex = 32;
            this.wActivateRCheckBox.Text = "R";
            this.wActivateRCheckBox.UseVisualStyleBackColor = true;
            this.wActivateRCheckBox.CheckedChanged += new System.EventHandler(this.wActivateRCheckBox_CheckedChanged);
            // 
            // wActivateECheckBox
            // 
            this.wActivateECheckBox.AutoSize = true;
            this.wActivateECheckBox.Location = new System.Drawing.Point(325, 94);
            this.wActivateECheckBox.Name = "wActivateECheckBox";
            this.wActivateECheckBox.Size = new System.Drawing.Size(33, 17);
            this.wActivateECheckBox.TabIndex = 31;
            this.wActivateECheckBox.Text = "E";
            this.wActivateECheckBox.UseVisualStyleBackColor = true;
            this.wActivateECheckBox.CheckedChanged += new System.EventHandler(this.wActivateECheckBox_CheckedChanged);
            // 
            // wActivateQCheckBox
            // 
            this.wActivateQCheckBox.AutoSize = true;
            this.wActivateQCheckBox.Location = new System.Drawing.Point(285, 94);
            this.wActivateQCheckBox.Name = "wActivateQCheckBox";
            this.wActivateQCheckBox.Size = new System.Drawing.Size(34, 17);
            this.wActivateQCheckBox.TabIndex = 30;
            this.wActivateQCheckBox.Text = "Q";
            this.wActivateQCheckBox.UseVisualStyleBackColor = true;
            this.wActivateQCheckBox.CheckedChanged += new System.EventHandler(this.wActivateQCheckBox_CheckedChanged);
            // 
            // eActivateRCheckBox
            // 
            this.eActivateRCheckBox.AutoSize = true;
            this.eActivateRCheckBox.Location = new System.Drawing.Point(368, 119);
            this.eActivateRCheckBox.Name = "eActivateRCheckBox";
            this.eActivateRCheckBox.Size = new System.Drawing.Size(34, 17);
            this.eActivateRCheckBox.TabIndex = 35;
            this.eActivateRCheckBox.Text = "R";
            this.eActivateRCheckBox.UseVisualStyleBackColor = true;
            this.eActivateRCheckBox.CheckedChanged += new System.EventHandler(this.eActivateRCheckBox_CheckedChanged);
            // 
            // eActivateWCheckBox
            // 
            this.eActivateWCheckBox.AutoSize = true;
            this.eActivateWCheckBox.Location = new System.Drawing.Point(325, 119);
            this.eActivateWCheckBox.Name = "eActivateWCheckBox";
            this.eActivateWCheckBox.Size = new System.Drawing.Size(37, 17);
            this.eActivateWCheckBox.TabIndex = 34;
            this.eActivateWCheckBox.Text = "W";
            this.eActivateWCheckBox.UseVisualStyleBackColor = true;
            this.eActivateWCheckBox.CheckedChanged += new System.EventHandler(this.eActivateWCheckBox_CheckedChanged);
            // 
            // eActivateQCheckBox
            // 
            this.eActivateQCheckBox.AutoSize = true;
            this.eActivateQCheckBox.Location = new System.Drawing.Point(285, 119);
            this.eActivateQCheckBox.Name = "eActivateQCheckBox";
            this.eActivateQCheckBox.Size = new System.Drawing.Size(34, 17);
            this.eActivateQCheckBox.TabIndex = 33;
            this.eActivateQCheckBox.Text = "Q";
            this.eActivateQCheckBox.UseVisualStyleBackColor = true;
            this.eActivateQCheckBox.CheckedChanged += new System.EventHandler(this.eActivateQCheckBox_CheckedChanged);
            // 
            // rActivateECheckBox
            // 
            this.rActivateECheckBox.AutoSize = true;
            this.rActivateECheckBox.Location = new System.Drawing.Point(368, 142);
            this.rActivateECheckBox.Name = "rActivateECheckBox";
            this.rActivateECheckBox.Size = new System.Drawing.Size(33, 17);
            this.rActivateECheckBox.TabIndex = 38;
            this.rActivateECheckBox.Text = "E";
            this.rActivateECheckBox.UseVisualStyleBackColor = true;
            this.rActivateECheckBox.CheckedChanged += new System.EventHandler(this.rActivateECheckBox_CheckedChanged);
            // 
            // rActivateWCheckBox
            // 
            this.rActivateWCheckBox.AutoSize = true;
            this.rActivateWCheckBox.Location = new System.Drawing.Point(325, 142);
            this.rActivateWCheckBox.Name = "rActivateWCheckBox";
            this.rActivateWCheckBox.Size = new System.Drawing.Size(37, 17);
            this.rActivateWCheckBox.TabIndex = 37;
            this.rActivateWCheckBox.Text = "W";
            this.rActivateWCheckBox.UseVisualStyleBackColor = true;
            this.rActivateWCheckBox.CheckedChanged += new System.EventHandler(this.rActivateWCheckBox_CheckedChanged);
            // 
            // rActivateQCheckBox
            // 
            this.rActivateQCheckBox.AutoSize = true;
            this.rActivateQCheckBox.Location = new System.Drawing.Point(285, 142);
            this.rActivateQCheckBox.Name = "rActivateQCheckBox";
            this.rActivateQCheckBox.Size = new System.Drawing.Size(34, 17);
            this.rActivateQCheckBox.TabIndex = 36;
            this.rActivateQCheckBox.Text = "Q";
            this.rActivateQCheckBox.UseVisualStyleBackColor = true;
            this.rActivateQCheckBox.CheckedChanged += new System.EventHandler(this.rActivateQCheckBox_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 336);
            this.Controls.Add(this.rActivateECheckBox);
            this.Controls.Add(this.rActivateWCheckBox);
            this.Controls.Add(this.rActivateQCheckBox);
            this.Controls.Add(this.eActivateRCheckBox);
            this.Controls.Add(this.eActivateWCheckBox);
            this.Controls.Add(this.eActivateQCheckBox);
            this.Controls.Add(this.wActivateRCheckBox);
            this.Controls.Add(this.wActivateECheckBox);
            this.Controls.Add(this.wActivateQCheckBox);
            this.Controls.Add(this.qActivateRCheckBox);
            this.Controls.Add(this.qActivateECheckBox);
            this.Controls.Add(this.qActivateWCheckBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.wardHopKeyComboBox);
            this.Controls.Add(this.wardHopCheckBox);
            this.Controls.Add(this.activeKeyComboBox);
            this.Controls.Add(this.activeValueText);
            this.Controls.Add(this.rValueText);
            this.Controls.Add(this.eValueText);
            this.Controls.Add(this.wValueText);
            this.Controls.Add(this.qValueText);
            this.Controls.Add(this.wardCheckbox);
            this.Controls.Add(this.active7On);
            this.Controls.Add(this.active6On);
            this.Controls.Add(this.active5On);
            this.Controls.Add(this.active3On);
            this.Controls.Add(this.active2On);
            this.Controls.Add(this.active1On);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.autoKeyOn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "League Ultimate Caster";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_Closing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox autoKeyOn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox active1On;
        private System.Windows.Forms.CheckBox active2On;
        private System.Windows.Forms.CheckBox active5On;
        private System.Windows.Forms.CheckBox active3On;
        private System.Windows.Forms.CheckBox active6On;
        private System.Windows.Forms.CheckBox active7On;
        private System.Windows.Forms.CheckBox wardCheckbox;
        private System.Windows.Forms.TextBox qValueText;
        private System.Windows.Forms.TextBox wValueText;
        private System.Windows.Forms.TextBox eValueText;
        private System.Windows.Forms.TextBox rValueText;
        private System.Windows.Forms.TextBox activeValueText;
        private System.Windows.Forms.ComboBox activeKeyComboBox;
        private System.Windows.Forms.CheckBox wardHopCheckBox;
        private System.Windows.Forms.ComboBox wardHopKeyComboBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox qActivateWCheckBox;
        private System.Windows.Forms.CheckBox qActivateECheckBox;
        private System.Windows.Forms.CheckBox qActivateRCheckBox;
        private System.Windows.Forms.CheckBox wActivateRCheckBox;
        private System.Windows.Forms.CheckBox wActivateECheckBox;
        private System.Windows.Forms.CheckBox wActivateQCheckBox;
        private System.Windows.Forms.CheckBox eActivateRCheckBox;
        private System.Windows.Forms.CheckBox eActivateWCheckBox;
        private System.Windows.Forms.CheckBox eActivateQCheckBox;
        private System.Windows.Forms.CheckBox rActivateECheckBox;
        private System.Windows.Forms.CheckBox rActivateWCheckBox;
        private System.Windows.Forms.CheckBox rActivateQCheckBox;
    }
}

