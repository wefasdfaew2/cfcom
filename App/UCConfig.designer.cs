namespace App
{
    partial class UCConfig
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblStakeVnd = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStake = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.lbl_BetSuccess = new System.Windows.Forms.Label();
            this.lblAccount = new System.Windows.Forms.LinkLabel();
            this.btnLoginB = new System.Windows.Forms.Button();
            this.cbDigit = new System.Windows.Forms.ComboBox();
            this.txtOdd = new System.Windows.Forms.TextBox();
            this.btnTrans = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblBetCredit = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblStakeVnd
            // 
            this.lblStakeVnd.AutoSize = true;
            this.lblStakeVnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStakeVnd.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblStakeVnd.Location = new System.Drawing.Point(411, 2);
            this.lblStakeVnd.Name = "lblStakeVnd";
            this.lblStakeVnd.Size = new System.Drawing.Size(21, 15);
            this.lblStakeVnd.TabIndex = 133;
            this.lblStakeVnd.Text = "0đ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(397, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 15);
            this.label4.TabIndex = 132;
            this.label4.Text = "=";
            // 
            // txtRate
            // 
            this.txtRate.Location = new System.Drawing.Point(353, 0);
            this.txtRate.Name = "txtRate";
            this.txtRate.Size = new System.Drawing.Size(42, 20);
            this.txtRate.TabIndex = 131;
            this.txtRate.Click += new System.EventHandler(this.txtStake_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(337, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 15);
            this.label1.TabIndex = 130;
            this.label1.Text = "x";
            // 
            // txtStake
            // 
            this.txtStake.Location = new System.Drawing.Point(264, 0);
            this.txtStake.Name = "txtStake";
            this.txtStake.Size = new System.Drawing.Size(70, 20);
            this.txtStake.TabIndex = 126;
            this.txtStake.Click += new System.EventHandler(this.txtStake_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(226, 2);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(38, 15);
            this.label17.TabIndex = 125;
            this.label17.Text = "Stake";
            // 
            // lbl_BetSuccess
            // 
            this.lbl_BetSuccess.AutoSize = true;
            this.lbl_BetSuccess.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_BetSuccess.ForeColor = System.Drawing.Color.Red;
            this.lbl_BetSuccess.Location = new System.Drawing.Point(500, 2);
            this.lbl_BetSuccess.Name = "lbl_BetSuccess";
            this.lbl_BetSuccess.Size = new System.Drawing.Size(15, 15);
            this.lbl_BetSuccess.TabIndex = 120;
            this.lbl_BetSuccess.Text = "0";
            // 
            // lblAccount
            // 
            this.lblAccount.AutoSize = true;
            this.lblAccount.Location = new System.Drawing.Point(2, 3);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(47, 13);
            this.lblAccount.TabIndex = 117;
            this.lblAccount.TabStop = true;
            this.lblAccount.Text = "Account";
            this.lblAccount.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblAccount_LinkClicked);
            // 
            // btnLoginB
            // 
            this.btnLoginB.Location = new System.Drawing.Point(82, 0);
            this.btnLoginB.Name = "btnLoginB";
            this.btnLoginB.Size = new System.Drawing.Size(48, 22);
            this.btnLoginB.TabIndex = 116;
            this.btnLoginB.Text = "Login";
            this.btnLoginB.UseVisualStyleBackColor = true;
            this.btnLoginB.Click += new System.EventHandler(this.btnLoginB_Click);
            // 
            // cbDigit
            // 
            this.cbDigit.FormattingEnabled = true;
            this.cbDigit.Items.AddRange(new object[] {
            "=",
            ">=",
            "<="});
            this.cbDigit.Location = new System.Drawing.Point(134, 0);
            this.cbDigit.Name = "cbDigit";
            this.cbDigit.Size = new System.Drawing.Size(37, 21);
            this.cbDigit.TabIndex = 135;
            // 
            // txtOdd
            // 
            this.txtOdd.Location = new System.Drawing.Point(173, 0);
            this.txtOdd.Name = "txtOdd";
            this.txtOdd.Size = new System.Drawing.Size(44, 20);
            this.txtOdd.TabIndex = 134;
            // 
            // btnTrans
            // 
            this.btnTrans.Enabled = false;
            this.btnTrans.Location = new System.Drawing.Point(704, 0);
            this.btnTrans.Name = "btnTrans";
            this.btnTrans.Size = new System.Drawing.Size(22, 22);
            this.btnTrans.TabIndex = 137;
            this.btnTrans.Text = "T";
            this.btnTrans.UseVisualStyleBackColor = true;
            this.btnTrans.Click += new System.EventHandler(this.btnTrans_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(734, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(22, 22);
            this.btnDelete.TabIndex = 136;
            this.btnDelete.Text = "X";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(661, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(40, 22);
            this.btnSave.TabIndex = 138;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblBetCredit
            // 
            this.lblBetCredit.AutoSize = true;
            this.lblBetCredit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBetCredit.ForeColor = System.Drawing.Color.Green;
            this.lblBetCredit.Location = new System.Drawing.Point(582, 2);
            this.lblBetCredit.Name = "lblBetCredit";
            this.lblBetCredit.Size = new System.Drawing.Size(14, 15);
            this.lblBetCredit.TabIndex = 139;
            this.lblBetCredit.Text = "0";
            // 
            // UCConfig
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.Controls.Add(this.lblBetCredit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnTrans);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.cbDigit);
            this.Controls.Add(this.txtOdd);
            this.Controls.Add(this.lblStakeVnd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtRate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtStake);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.lbl_BetSuccess);
            this.Controls.Add(this.lblAccount);
            this.Controls.Add(this.btnLoginB);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "UCConfig";
            this.Size = new System.Drawing.Size(755, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStakeVnd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStake;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lbl_BetSuccess;
        private System.Windows.Forms.LinkLabel lblAccount;
        private System.Windows.Forms.Button btnLoginB;
        private System.Windows.Forms.ComboBox cbDigit;
        private System.Windows.Forms.TextBox txtOdd;
        private System.Windows.Forms.Button btnTrans;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblBetCredit;


    }
}
