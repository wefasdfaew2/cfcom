namespace App
{
    partial class UCAccount
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
            this.btnLoginB = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtIpB = new System.Windows.Forms.TextBox();
            this.txtPwdB = new System.Windows.Forms.TextBox();
            this.txtUsnB = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.cbHostUrl = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnLoginB
            // 
            this.btnLoginB.Location = new System.Drawing.Point(450, 0);
            this.btnLoginB.Name = "btnLoginB";
            this.btnLoginB.Size = new System.Drawing.Size(48, 22);
            this.btnLoginB.TabIndex = 116;
            this.btnLoginB.Text = "Login";
            this.btnLoginB.UseVisualStyleBackColor = true;
            this.btnLoginB.Click += new System.EventHandler(this.btnLoginB_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(552, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(22, 22);
            this.btnDelete.TabIndex = 136;
            this.btnDelete.Text = "X";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(507, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(40, 22);
            this.btnSave.TabIndex = 138;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtIpB
            // 
            this.txtIpB.Location = new System.Drawing.Point(356, 1);
            this.txtIpB.Name = "txtIpB";
            this.txtIpB.Size = new System.Drawing.Size(87, 20);
            this.txtIpB.TabIndex = 141;
            // 
            // txtPwdB
            // 
            this.txtPwdB.Location = new System.Drawing.Point(260, 1);
            this.txtPwdB.Name = "txtPwdB";
            this.txtPwdB.Size = new System.Drawing.Size(92, 20);
            this.txtPwdB.TabIndex = 140;
            this.txtPwdB.UseSystemPasswordChar = true;
            // 
            // txtUsnB
            // 
            this.txtUsnB.Location = new System.Drawing.Point(171, 1);
            this.txtUsnB.Name = "txtUsnB";
            this.txtUsnB.Size = new System.Drawing.Size(86, 20);
            this.txtUsnB.TabIndex = 139;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(578, 4);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(10, 13);
            this.lblMessage.TabIndex = 142;
            this.lblMessage.Text = ".";
            // 
            // cbHostUrl
            // 
            this.cbHostUrl.FormattingEnabled = true;
            this.cbHostUrl.Items.AddRange(new object[] {
            "http://www.com3456.com",
            "http://www.bookie88.net",
            "http://www.currybread.com"});
            this.cbHostUrl.Location = new System.Drawing.Point(0, 0);
            this.cbHostUrl.Name = "cbHostUrl";
            this.cbHostUrl.Size = new System.Drawing.Size(165, 21);
            this.cbHostUrl.TabIndex = 143;
            // 
            // UCAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.Controls.Add(this.cbHostUrl);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.txtIpB);
            this.Controls.Add(this.txtPwdB);
            this.Controls.Add(this.txtUsnB);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnLoginB);
            this.Name = "UCAccount";
            this.Size = new System.Drawing.Size(661, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoginB;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtIpB;
        private System.Windows.Forms.TextBox txtPwdB;
        private System.Windows.Forms.TextBox txtUsnB;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.ComboBox cbHostUrl;


    }
}
