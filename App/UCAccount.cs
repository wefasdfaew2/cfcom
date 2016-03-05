using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.Model;
using ModelObjects;
using SbobetLib;
using System.Threading;

namespace App
{
    public partial class UCAccount : UserControl
    {
        Account _acccount;
        public UCAccount()
        {
            InitializeComponent();
        }

        private async void CheckLogin()
        {
            while (1 == 1)
            {
                var acc = Program.AccountGets.SingleOrDefault(x => x.Key == _acccount.Key);
                if (acc != null && (String.IsNullOrEmpty(acc.UserName) || acc.Cookie.Count == 0) && btnLoginB.Text == "Logout")
                {
                    lblMessage.Invoke(new Action(() => lblMessage.Text = "Loss session"));
                    btnLoginB.Invoke(new Action(() => btnLoginB.Text = "Login"));
                    btnLoginB.Invoke(new Action(() => btnLoginB.Enabled = true));
                    btnDelete.Invoke(new Action(() => btnDelete.Enabled = true));
                }
                await Task.Delay(3000);
            }
        }

        public UCAccount(Account account)
        {
            InitializeComponent();
            this.Name = account.Key;
            if (account.MatchOdds == null) account.MatchOdds = new List<MatchOdd>();
            if (account.CurrentMatch == null) account.CurrentMatch = new MatchOdd();
            _acccount = account;
            if (!Program.AccountGets.Any(x => x.Key == account.Key))
            {
                Program.AccountGets.Add(account);
            }
            ReFreshAccountView(account);
        }

        async void ReFreshAccountView(Account account)
        {
            txtUsnB.Text = account.UserName.ToString();
            txtPwdB.Text = account.Password.ToString();
            txtIpB.Text = account.FakeIp.ToString();
            cbHostUrl.SelectedItem = account.HostUrl;
        }

        async void btnSave_Click(object sender, EventArgs e)
        {
            if (txtUsnB.Text.Trim() == "" || txtPwdB.Text.Trim() == "")
            {
                MessageBox.Show("Please input account info");
            }
            Program.AccountGets.SingleOrDefault(x => x.Key == _acccount.Key).UserName = txtUsnB.Text.Trim();
            Program.AccountGets.SingleOrDefault(x => x.Key == _acccount.Key).Password = txtPwdB.Text.Trim();
            Program.AccountGets.SingleOrDefault(x => x.Key == _acccount.Key).FakeIp = txtIpB.Text.Trim();
            Program.AccountGets.SingleOrDefault(x => x.Key == _acccount.Key).HostUrl = cbHostUrl.SelectedItem.ToString();
            Program.SaveConfig();
            MessageBox.Show("Save sucessfully!");
        }

        private void btnLoginB_Click(object sender, EventArgs e)
        {
            Login();
        }
        public async void Login()
        {
            if (btnLoginB.Text == "Logout")
            {
                Program.AccountGets.SingleOrDefault(x => x.Key == _acccount.Key).LoginName = null;
                Program.AccountGets.SingleOrDefault(x => x.Key == _acccount.Key).Cookie = new System.Net.CookieCollection();
                btnLoginB.Text = "Login";
                btnLoginB.Enabled = true;
                btnDelete.Enabled = true;
                this.BackColor = SystemColors.Info;
                lblMessage.Invoke(new Action(() => lblMessage.Text = "."));
                return;
            }

            if (Program.AccountGets.SingleOrDefault(x => x.Key == _acccount.Key).UserName == "" || Program.AccountGets.SingleOrDefault(x => x.Key == _acccount.Key).Password == "")
            {
                MessageBox.Show("Please enter UserName & password!");
                return;
            }

            btnLoginB.Enabled = false;

            Task<Response<bool>> loginTask = Task.Run(() => new Sbobet().Login(Program.AccountGets.SingleOrDefault(x => x.Key == _acccount.Key)));
            var res = await loginTask;
            if (res.Data && !res.HasError)
            {
                if (Program.AccountGets.SingleOrDefault(x => x.Key == _acccount.Key).Cookie.Count > 3 && !String.IsNullOrEmpty(Program.AccountGets.SingleOrDefault(x => x.Key == _acccount.Key).LoginName))
                {
                    btnLoginB.Text = "Logout";
                    btnLoginB.Enabled = true;
                    btnDelete.Enabled = false;
                    this.BackColor = SystemColors.GradientActiveCaption;
                    lblMessage.Invoke(new Action(() => lblMessage.Text = "Login success!"));
                    lblMessage.Invoke(new Action(() => lblMessage.ForeColor = Color.Green));
                    new Thread(new ThreadStart(CheckLogin)).Start();
                    var x = (frmMain)this.FindForm();
                    x.ViewMatchInfo(_acccount);
                }
            }
            else
            {
                MessageBox.Show(res.ToErrorMsg());
                btnLoginB.Enabled = true;
                this.BackColor = SystemColors.Info;
            }
        }

        public async void LoginFromMain(string txt)
        {
            if (Program.AccountGets.SingleOrDefault(x => x.Key == _acccount.Key).UserName == "" || Program.AccountGets.SingleOrDefault(x => x.Key == _acccount.Key).Password == "")
            {
                return;
            }

            if (txt == "Logout")
            {
                Program.AccountGets.SingleOrDefault(x => x.Key == _acccount.Key).LoginName = null;
                Program.AccountGets.SingleOrDefault(x => x.Key == _acccount.Key).Cookie = new System.Net.CookieCollection();
                btnLoginB.Text = "Login";
                btnDelete.Enabled = true;
                this.BackColor = SystemColors.Info;
                return;
            }

            btnLoginB.Enabled = false;

            Task<Response<bool>> loginTask = Task.Run(() => new Sbobet().Login(Program.AccountGets.SingleOrDefault(x => x.Key == _acccount.Key)));
            var res = await loginTask;
            if (res.Data && !res.HasError)
            {
                var acc = Program.AccountGets.SingleOrDefault(x => x.Key == _acccount.Key);
                if (acc.Cookie.Count > 3 && !String.IsNullOrEmpty(acc.LoginName))
                {
                    btnLoginB.Text = "Logout";
                    btnLoginB.Enabled = true;
                    btnDelete.Enabled = false;
                    this.BackColor = SystemColors.GradientActiveCaption;
                    lblMessage.Invoke(new Action(() => lblMessage.Text = "Login success!"));
                    lblMessage.Invoke(new Action(() => lblMessage.ForeColor = Color.Green));
                    new Thread(new ThreadStart(CheckLogin)).Start();
                    var x = (frmMain)this.FindForm();
                    x.ViewMatchInfo(_acccount);
                }
            }
            else
            {
                btnLoginB.Enabled = true;
                this.BackColor = SystemColors.Info;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var acc = Program.AccountGets.SingleOrDefault(x => x.Key == _acccount.Key);
            if (acc != null) Program.AccountGets.Remove(acc);
            this.Parent.Controls.Remove(this);
            Program.SaveConfig();
        }
    }
}
