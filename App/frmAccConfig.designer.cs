namespace App
{
    partial class frmAccConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAccConfig));
            this.gbUc = new System.Windows.Forms.GroupBox();
            this.cbHostUrl = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblVndStake = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRate = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWait = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUnderOdd = new System.Windows.Forms.TextBox();
            this.txtStake = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbUnder = new System.Windows.Forms.ComboBox();
            this.cbOver = new System.Windows.Forms.ComboBox();
            this.txtOverOdd = new System.Windows.Forms.TextBox();
            this.rdOver = new System.Windows.Forms.RadioButton();
            this.rdUnder = new System.Windows.Forms.RadioButton();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIpB = new System.Windows.Forms.TextBox();
            this.txtPwdB = new System.Windows.Forms.TextBox();
            this.txtUsnB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.gbUc.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbUc
            // 
            this.gbUc.BackColor = System.Drawing.SystemColors.ControlLight;
            this.gbUc.Controls.Add(this.cbHostUrl);
            this.gbUc.Controls.Add(this.groupBox2);
            this.gbUc.Controls.Add(this.label1);
            this.gbUc.Controls.Add(this.label4);
            this.gbUc.Controls.Add(this.txtIpB);
            this.gbUc.Controls.Add(this.txtPwdB);
            this.gbUc.Controls.Add(this.txtUsnB);
            this.gbUc.Controls.Add(this.label6);
            this.gbUc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbUc.Location = new System.Drawing.Point(0, 0);
            this.gbUc.Name = "gbUc";
            this.gbUc.Size = new System.Drawing.Size(365, 230);
            this.gbUc.TabIndex = 77;
            this.gbUc.TabStop = false;
            this.gbUc.Text = "Bet Account";
            // 
            // cbHostUrl
            // 
            this.cbHostUrl.FormattingEnabled = true;
            this.cbHostUrl.Items.AddRange(new object[] {
            "http://www.com3456.com",
            "http://www.bookie88.net",
            "http://www.currybread.com"});
            this.cbHostUrl.Location = new System.Drawing.Point(10, 23);
            this.cbHostUrl.Name = "cbHostUrl";
            this.cbHostUrl.Size = new System.Drawing.Size(291, 21);
            this.cbHostUrl.TabIndex = 144;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblVndStake);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtRate);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtWait);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtUnderOdd);
            this.groupBox2.Controls.Add(this.txtStake);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Location = new System.Drawing.Point(10, 108);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(351, 117);
            this.groupBox2.TabIndex = 90;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Configuration";
            // 
            // lblVndStake
            // 
            this.lblVndStake.AutoSize = true;
            this.lblVndStake.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVndStake.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblVndStake.Location = new System.Drawing.Point(224, 58);
            this.lblVndStake.Name = "lblVndStake";
            this.lblVndStake.Size = new System.Drawing.Size(21, 15);
            this.lblVndStake.TabIndex = 137;
            this.lblVndStake.Text = "0đ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(211, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 15);
            this.label7.TabIndex = 136;
            this.label7.Text = "=";
            // 
            // txtRate
            // 
            this.txtRate.Location = new System.Drawing.Point(163, 56);
            this.txtRate.Name = "txtRate";
            this.txtRate.Size = new System.Drawing.Size(42, 20);
            this.txtRate.TabIndex = 135;
            this.txtRate.Click += new System.EventHandler(this.txtStake_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(148, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 15);
            this.label8.TabIndex = 134;
            this.label8.Text = "x";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(91, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 15);
            this.label3.TabIndex = 97;
            this.label3.Text = "s";
            // 
            // txtWait
            // 
            this.txtWait.Location = new System.Drawing.Point(58, 87);
            this.txtWait.Name = "txtWait";
            this.txtWait.Size = new System.Drawing.Size(28, 20);
            this.txtWait.TabIndex = 96;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(22, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 95;
            this.label2.Text = "Wait";
            // 
            // txtUnderOdd
            // 
            this.txtUnderOdd.Location = new System.Drawing.Point(293, 24);
            this.txtUnderOdd.Name = "txtUnderOdd";
            this.txtUnderOdd.Size = new System.Drawing.Size(46, 20);
            this.txtUnderOdd.TabIndex = 89;
            // 
            // txtStake
            // 
            this.txtStake.Location = new System.Drawing.Point(58, 56);
            this.txtStake.Name = "txtStake";
            this.txtStake.Size = new System.Drawing.Size(87, 20);
            this.txtStake.TabIndex = 93;
            this.txtStake.Click += new System.EventHandler(this.txtStake_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(4, 58);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(38, 15);
            this.label17.TabIndex = 92;
            this.label17.Text = "Stake";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbUnder);
            this.panel1.Controls.Add(this.cbOver);
            this.panel1.Controls.Add(this.txtOverOdd);
            this.panel1.Controls.Add(this.rdOver);
            this.panel1.Controls.Add(this.rdUnder);
            this.panel1.Location = new System.Drawing.Point(3, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(339, 30);
            this.panel1.TabIndex = 91;
            // 
            // cbUnder
            // 
            this.cbUnder.FormattingEnabled = true;
            this.cbUnder.Items.AddRange(new object[] {
            "=",
            ">=",
            "<="});
            this.cbUnder.Location = new System.Drawing.Point(251, 3);
            this.cbUnder.Name = "cbUnder";
            this.cbUnder.Size = new System.Drawing.Size(37, 21);
            this.cbUnder.TabIndex = 80;
            // 
            // cbOver
            // 
            this.cbOver.FormattingEnabled = true;
            this.cbOver.Items.AddRange(new object[] {
            "=",
            ">=",
            "<="});
            this.cbOver.Location = new System.Drawing.Point(77, 3);
            this.cbOver.Name = "cbOver";
            this.cbOver.Size = new System.Drawing.Size(37, 21);
            this.cbOver.TabIndex = 79;
            // 
            // txtOverOdd
            // 
            this.txtOverOdd.Location = new System.Drawing.Point(117, 4);
            this.txtOverOdd.Name = "txtOverOdd";
            this.txtOverOdd.Size = new System.Drawing.Size(44, 20);
            this.txtOverOdd.TabIndex = 2;
            // 
            // rdOver
            // 
            this.rdOver.AutoSize = true;
            this.rdOver.Location = new System.Drawing.Point(5, 5);
            this.rdOver.Name = "rdOver";
            this.rdOver.Size = new System.Drawing.Size(48, 17);
            this.rdOver.TabIndex = 0;
            this.rdOver.TabStop = true;
            this.rdOver.Text = "Over";
            this.rdOver.UseVisualStyleBackColor = true;
            // 
            // rdUnder
            // 
            this.rdUnder.AutoSize = true;
            this.rdUnder.Location = new System.Drawing.Point(190, 5);
            this.rdUnder.Name = "rdUnder";
            this.rdUnder.Size = new System.Drawing.Size(54, 17);
            this.rdUnder.TabIndex = 76;
            this.rdUnder.TabStop = true;
            this.rdUnder.Text = "Under";
            this.rdUnder.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(303, 85);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(40, 24);
            this.btnSave.TabIndex = 90;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(119, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 15);
            this.label1.TabIndex = 89;
            this.label1.Text = "Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(219, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 15);
            this.label4.TabIndex = 84;
            this.label4.Text = "Ip";
            // 
            // txtIpB
            // 
            this.txtIpB.Location = new System.Drawing.Point(216, 77);
            this.txtIpB.Name = "txtIpB";
            this.txtIpB.Size = new System.Drawing.Size(87, 20);
            this.txtIpB.TabIndex = 83;
            // 
            // txtPwdB
            // 
            this.txtPwdB.Location = new System.Drawing.Point(117, 77);
            this.txtPwdB.Name = "txtPwdB";
            this.txtPwdB.Size = new System.Drawing.Size(95, 20);
            this.txtPwdB.TabIndex = 81;
            this.txtPwdB.UseSystemPasswordChar = true;
            // 
            // txtUsnB
            // 
            this.txtUsnB.Location = new System.Drawing.Point(10, 77);
            this.txtUsnB.Name = "txtUsnB";
            this.txtUsnB.Size = new System.Drawing.Size(104, 20);
            this.txtUsnB.TabIndex = 80;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(12, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 15);
            this.label6.TabIndex = 79;
            this.label6.Text = "Username";
            // 
            // frmAccConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 230);
            this.Controls.Add(this.gbUc);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAccConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bet Account Config";
            this.gbUc.ResumeLayout(false);
            this.gbUc.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbUc;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWait;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUnderOdd;
        private System.Windows.Forms.TextBox txtStake;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbUnder;
        private System.Windows.Forms.ComboBox cbOver;
        private System.Windows.Forms.TextBox txtOverOdd;
        private System.Windows.Forms.RadioButton rdOver;
        private System.Windows.Forms.RadioButton rdUnder;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtIpB;
        private System.Windows.Forms.TextBox txtPwdB;
        private System.Windows.Forms.TextBox txtUsnB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblVndStake;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbHostUrl;

    }
}