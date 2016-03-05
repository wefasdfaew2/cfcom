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
using System.Threading;
using ModelObjects;
using Utils;
using SbobetLib;
using log4net;

namespace App
{
    public partial class UCConfig : UserControl
    {
        public bool AllowDrag { get; set; }
        private bool _isDragging = false;
        private int _DDradius = 40;
        private int _mX = 0;
        private int _mY = 0;

        private static readonly ILog _log = LogManager.GetLogger(typeof(UCConfig));
        Account _account;
        bool _flag = true;    
        public UCConfig()
        {
            InitializeComponent();
            AllowDrag = true;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Focus();
            base.OnMouseDown(e);
            _mX = e.X;
            _mY = e.Y;
            this._isDragging = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!_isDragging)
            {
                // This is a check to see if the mouse is moving while pressed.
                // Without this, the DragDrop is fired directly when the control is clicked, now you have to drag a few pixels first.
                if (e.Button == MouseButtons.Left && _DDradius > 0 && this.AllowDrag)
                {
                    int num1 = _mX - e.X;
                    int num2 = _mY - e.Y;
                    if (((num1 * num1) + (num2 * num2)) > _DDradius)
                    {
                        DoDragDrop(this, DragDropEffects.All);
                        _isDragging = true;
                        return;
                    }
                }
                base.OnMouseMove(e);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _isDragging = false;
            base.OnMouseUp(e);
        }        

        public UCConfig(Account account)
        {
            InitializeComponent();
            AllowDrag = true;
            this.Name = account.Key;
            if (account.MatchOdds == null) account.MatchOdds = new List<MatchOdd>();
            if (account.CurrentMatch == null) account.CurrentMatch = new MatchOdd();
            _account = account;
            if (!Program.Accounts.Any(x => x.Key == account.Key))
            {
                Program.Accounts.Add(account);
            }
            ReFreshAccountView(account);
        }

        async void ReFreshAccountView(Account account)
        {
            lblAccount.LinkColor = account.Choose == 1 ? Color.Green : Color.Red;
            lblAccount.Text = account.UserName;
            txtStake.Text = account.Stake.ToString();
            txtRate.Text = account.Rate.ToString();
            txtOdd.Text = account.Choose == 1 ? account.UnderOdd.ToString() : account.OverOdd.ToString();
            lblStakeVnd.Text = ConvertUtils.ToMoneyText(account.StakeVnd);
            cbDigit.SelectedIndex = account.Choose == 1 ? account.UnderDigit : account.OverDigit;
        }

        async void btnSave_Click(object sender, EventArgs e)
        {
            Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).Stake = int.Parse(txtStake.Text.Trim());
            Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).StakeBalance = int.Parse(txtStake.Text.Trim());
            Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).Rate = double.Parse(txtRate.Text.Trim());
            if (_account.Choose == 0)
            {
                Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).OverOdd = double.Parse(txtOdd.Text.Trim());
                Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).OverDigit = cbDigit.SelectedIndex;
            }
            else
            {
                Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).UnderOdd = double.Parse(txtOdd.Text.Trim());
                Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).UnderDigit = cbDigit.SelectedIndex;
            }

            Program.SaveBetConfig();
            _log.InfoFormat("Account Settings {0} saved successfully!", _account.UserName);
        }

        public async void ChangeBetType(ConfigAll config)
        {
            _account.Choose = config.Choose;
            Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).Choose = config.Choose;
            if (_account.Choose == 0)
            {
                Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).OverOdd = double.Parse(txtOdd.Text.Trim());
                Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).OverDigit = cbDigit.SelectedIndex;
            }
            else
            {
                Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).UnderOdd = double.Parse(txtOdd.Text.Trim());
                Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).UnderDigit = cbDigit.SelectedIndex;
            }

            ReFreshAccountView(Program.Accounts.SingleOrDefault(x => x.Key == _account.Key));
        }

        public async void SaveConfig(ConfigAll config)
        {
            txtStake.Text = config.Stake.ToString();
            txtRate.Text = config.Rate.ToString();
            txtOdd.Text = config.OddRate.ToString();
            lblStakeVnd.Text = ConvertUtils.ToMoneyText(config.StakeVnd);
            cbDigit.SelectedIndex = config.Digit;

            Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).Stake = int.Parse(txtStake.Text.Trim());
            Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).Rate = int.Parse(txtRate.Text.Trim());
            if (_account.Choose == 1)
            {
                Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).UnderOdd = double.Parse(txtOdd.Text.Trim());
                Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).UnderDigit = cbDigit.SelectedIndex;
            }
            else
            {
                Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).OverOdd = double.Parse(txtOdd.Text.Trim());
                Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).OverDigit = cbDigit.SelectedIndex;                
            }
        }

        public async void SaveConfigStakeOnly(ConfigAll config)
        {
            txtStake.Text = config.Stake.ToString();
            lblStakeVnd.Text = ConvertUtils.ToMoneyText(config.StakeVnd);
            Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).Stake = int.Parse(txtStake.Text.Trim());
        }
        private void btnLoginB_Click(object sender, EventArgs e)
        {
            Login();
        }
        public async void Login()
        {
            if (btnLoginB.Text == "Logout")
            {
                Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).LoginName = null;
                Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).Cookie = new System.Net.CookieCollection();
                btnLoginB.Text = "Login";
                btnDelete.Enabled = true;
                btnTrans.Enabled = false;
                this.BackColor = SystemColors.Info;
                _log.InfoFormat("Logout account {0} successfully!", _account.UserName);
                return;
            }

            if (Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).UserName == "" || Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).Password == "")
            {
                MessageBox.Show("Please enter UserName & password!");
                return;
            }

            btnLoginB.Enabled = false;

            Task<Response<bool>> loginTask = Task.Run(() => new Sbobet().Login(Program.Accounts.SingleOrDefault(x => x.Key == _account.Key)));
            var res = await loginTask;
            if (res.Data && !res.HasError)
            {
                if (Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).Cookie.Count > 3 && !String.IsNullOrEmpty(Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).LoginName))
                {
                    ((Button)((frmMain)this.FindForm()).Controls.Find("btnStart", true).FirstOrDefault()).Enabled = true;
                    ((Button)((frmMain)this.FindForm()).Controls.Find("btnStop", true).FirstOrDefault()).Enabled = true;
                    btnLoginB.Text = "Logout";
                    btnLoginB.Enabled = true;
                    btnTrans.Enabled = true;
                    btnDelete.Enabled = false;
                    this.BackColor = SystemColors.GradientActiveCaption;
                    new Thread(new ThreadStart(GetOdds)).Start();
                    new Thread(new ThreadStart(Bet)).Start();

                    //var firstBets = new List<Task>();
                    //var bet = Task.Run(async () => await Bet());
                    //firstBets.Add(bet);
                    //Task.s
                }
            }
            else
            {
                btnLoginB.Enabled = true;
                this.BackColor = SystemColors.Info;
            }
        }
        public async void LoginFromMain(string txt)
        {
            if (txt == "Logout")
            {
                Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).LoginName = null;
                Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).Cookie = new System.Net.CookieCollection();
                btnLoginB.Text = "Login";
                btnDelete.Enabled = true;
                btnTrans.Enabled = false;
                this.BackColor = SystemColors.Info;
                _log.InfoFormat("Logout account {0} successfully!", _account.UserName);
                return;
            }

            if (Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).UserName == "" || Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).Password == "")
            {
                MessageBox.Show("Please enter UserName & password!");
                return;
            }

            btnLoginB.Enabled = false;

            Task<Response<bool>> loginTask = Task.Run(() => new Sbobet().Login(Program.Accounts.SingleOrDefault(x => x.Key == _account.Key)));
            var res = await loginTask;
            if (res.Data && !res.HasError)
            {
                if (Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).Cookie.Count > 3 && !String.IsNullOrEmpty(Program.Accounts.SingleOrDefault(x => x.Key == _account.Key).LoginName))
                {
                    ((Button)((frmMain)this.FindForm()).Controls.Find("btnStart", true).FirstOrDefault()).Enabled = true;
                    ((Button)((frmMain)this.FindForm()).Controls.Find("btnStop", true).FirstOrDefault()).Enabled = true;
                    btnLoginB.Text = "Logout";
                    btnLoginB.Enabled = true;
                    btnTrans.Enabled = true;
                    btnDelete.Enabled = false;
                    this.BackColor = SystemColors.GradientActiveCaption;
                    //new Thread(new ThreadStart(GetOdds)).Start();
                    //new Thread(new ThreadStart(Bet)).Start();
                    //Task.Factory.StartNew(GetOdds).Start();
                    var task1 = new Task(() => { GetOdds(); });
                    task1.RunSynchronously();
                    var task = new Task(() => { Bet(); });
                    task.RunSynchronously();

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
            var acc = Program.Accounts.SingleOrDefault(x => x.Key == _account.Key);
            if (acc != null) Program.Accounts.Remove(acc);
            this.Parent.Controls.Remove(this);
            Program.SaveBetConfig();
        }
        private void btnTrans_Click(object sender, EventArgs e)
        {
            WSubTran f = new WSubTran(string.Format("{0}/web-root/restricted/betlist/running-bet-list.aspx?popout=1", _account.BaseUrl), _account.Cookie);
            f.ShowDialog();
        }
        private void lblAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var _frmConfig = new frmAccConfig(_account);
            _frmConfig.ShowDialog();
            ReFreshAccountView(Program.Accounts.SingleOrDefault(x => x.Key == _account.Key));
        }
        private void txtStake_TextChanged(object sender, EventArgs e)
        {
            var value = ConvertUtils.ToMoneyText(ConvertUtils.ToInt(txtStake.Text.Trim()) * ConvertUtils.ToInt(txtRate.Text.Trim()));
            lblStakeVnd.Text = value;
        }

        #region Bet Schedule
        private async void Bet()
        {
            try
            {                            
                while (1 == 1)
                {
                    if (Program.___flag==1 && _account.AccountType==0 && _flag)
                    {
                        var acc = Program.Accounts.SingleOrDefault(x => x.Key == _account.Key);
                        if ( acc!= null && acc.Cookie.Count>0 && !string.IsNullOrEmpty(acc.LoginName))
                        {
                            var odd = acc.CurrentMatch;
                            _flag = false;
                            Task<Response<bool>> loginTask = Task.Run(() => new Sbobet().BetOverUnder(acc, odd));
                            var result = await loginTask;
                            acc.LastBetId = odd.MatchId.ToString() + odd.OddType.ToString() + odd.Goal.ToString();
                            if (result.Data && !result.HasError) lbl_BetSuccess.Invoke(new Action(() => lbl_BetSuccess.Text = ConvertUtils.ToMoneyText(acc.StakeVnd)));
                        }
                    }
                    else if (Program.___flag == 2 && (_account.AccountType == 1 || _account.AccountType == 3) && _flag)
                    {
                        var acc = Program.Accounts.SingleOrDefault(x => x.Key == _account.Key);
                        if (acc != null && acc.Cookie.Count > 0 && !string.IsNullOrEmpty(acc.LoginName))
                        {
                            var odd = acc.CurrentMatch;
                            _flag = false;
                            Task<Response<bool>> loginTask = Task.Run(() => new Sbobet().BetOverUnder(acc, odd));
                            var result = await loginTask;
                            if (result.Data && !result.HasError) lbl_BetSuccess.Invoke(new Action(() => lbl_BetSuccess.Text = ConvertUtils.ToMoneyText(acc.StakeVnd)));
                        }
                    }
                    else if (Program.___flag == 3 && (_account.AccountType == 2) && _flag)
                    {
                        var acc = Program.Accounts.SingleOrDefault(x => x.Key == _account.Key);
                        if (acc != null && acc.Cookie.Count > 0 && !string.IsNullOrEmpty(acc.LoginName))
                        {
                            var odd = acc.CurrentMatch;
                            _flag = false;
                            Task<Response<bool>> loginTask = Task.Run(() => new Sbobet().BetOverUnder(acc, odd));
                            var result = await loginTask;
                            if (result.Data && !result.HasError) lbl_BetSuccess.Invoke(new Action(() => lbl_BetSuccess.Text = ConvertUtils.ToMoneyText(acc.StakeVnd)));
                        }
                    }
                    await Task.Delay(10);
                }
            }
            catch (Exception ex)
            {
            }
        }
        private async void GetOdds()
        { 
            try
            {
                var acc = Program.Accounts.SingleOrDefault(x => x.Key == _account.Key);
                while (1==1)
                {
                    if (Program.___flag==0)
                    {
                        if (acc != null && acc.Cookie.Count > 3 && !String.IsNullOrEmpty(acc.LoginName))
                        {
                            if (acc.CurrentMatch.MatchId != Program._currentMatch.MatchId || acc.CurrentMatch.OddType != Program._currentMatch.OddType || acc.CurrentMatch.Goal != Program._currentMatch.Goal)
                            {
                                var result = new Sbobet().GetOdds(acc, Program.MatchFilter.MatchStyle);
                                if (!result.HasError && result.Data != null)
                                {
                                    acc.MatchOdds = result.Data.Where(x => x.OddType == (int)Program.MatchFilter.OddStyle).ToList();
                                    var odd = acc.MatchOdds.SingleOrDefault(x => x.Match.MatchId == Program._betMatch.MatchId && x.OddType == Program._betMatch.OddType && x.Goal == Program._betMatch.Goal);
                                    acc.CurrentMatch = odd;
                                    lblBetCredit.Invoke(new Action(() => lblBetCredit.Text = odd.OddId.ToString()));
                                    _flag = true;
                                }
                            }
                        }
                    }
                    await Task.Delay(2000);
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion
    }
}
