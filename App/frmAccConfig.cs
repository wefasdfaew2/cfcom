using App.Model;
using ModelObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utils;

namespace App
{
    public partial class frmAccConfig : Form
    {
        Account _acccount;
        public frmAccConfig()
        {
            InitializeComponent();
        }

        public frmAccConfig(Account account)
        {
            InitializeComponent();
            _acccount = account;

            txtUsnB.Text = account.UserName;
            txtPwdB.Text = account.Password;
            txtIpB.Text = account.FakeIp;
            txtStake.Text = account.Stake.ToString();
            txtRate.Text = account.Rate.ToString();
            lblVndStake.Text = ConvertUtils.ToMoneyText(account.StakeVnd);
            txtOverOdd.Text = account.OverOdd.ToString();
            txtUnderOdd.Text = account.UnderOdd.ToString();
            rdOver.Checked = (account.Choose != 1);
            rdUnder.Checked = (account.Choose == 1);
            cbOver.SelectedIndex = account.OverDigit;
            cbUnder.SelectedIndex = account.UnderDigit;
            txtWait.Text = account.Wait.ToString();
            cbHostUrl.SelectedItem = account.HostUrl;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Program.Accounts.Any(x => x.UserName == txtUsnB.Text.Trim() && x.UserName != _acccount.UserName))
            {
                MessageBox.Show("Acc already exits!");
                return;
            }
            Program.Accounts.SingleOrDefault(x => x.Key == _acccount.Key).HostUrl = cbHostUrl.SelectedItem.ToString();
            Program.Accounts.SingleOrDefault(x => x.Key == _acccount.Key).UserName = txtUsnB.Text.Trim();
            Program.Accounts.SingleOrDefault(x => x.Key == _acccount.Key).Password = txtPwdB.Text.Trim();
            Program.Accounts.SingleOrDefault(x => x.Key == _acccount.Key).FakeIp = txtIpB.Text.Trim();
            Program.Accounts.SingleOrDefault(x => x.Key == _acccount.Key).Stake = ConvertUtils.ToInt(txtStake.Text.Trim());
            Program.Accounts.SingleOrDefault(x => x.Key == _acccount.Key).StakeBalance = ConvertUtils.ToInt(txtStake.Text.Trim());
            Program.Accounts.SingleOrDefault(x => x.Key == _acccount.Key).Rate = ConvertUtils.ToInt(txtRate.Text.Trim());
            Program.Accounts.SingleOrDefault(x => x.Key == _acccount.Key).OverOdd = ConvertUtils.ToDouble(txtOverOdd.Text.Trim());
            Program.Accounts.SingleOrDefault(x => x.Key == _acccount.Key).UnderOdd = ConvertUtils.ToDouble(txtUnderOdd.Text.Trim());
            Program.Accounts.SingleOrDefault(x => x.Key == _acccount.Key).Choose = rdOver.Checked ? 0 : 1;
            Program.Accounts.SingleOrDefault(x => x.Key == _acccount.Key).OverDigit = cbOver.SelectedIndex;
            Program.Accounts.SingleOrDefault(x => x.Key == _acccount.Key).UnderDigit = cbUnder.SelectedIndex;
            Program.Accounts.SingleOrDefault(x => x.Key == _acccount.Key).Wait = ConvertUtils.ToDouble(txtWait.Text.Trim());
            Program.SaveBetConfig();
            this.Close();
        }

        private void txtStake_TextChanged(object sender, EventArgs e)
        {
            var value = ConvertUtils.ToMoneyText(ConvertUtils.ToInt(txtStake.Text.Trim()) * ConvertUtils.ToInt(txtRate.Text.Trim()));
            lblVndStake.Text = value;
        }
    }
}
