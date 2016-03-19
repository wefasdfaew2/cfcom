using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.Model;
using System.Threading;
using ModelObjects;
using Utils;
using SbobetLib;
using log4net;
using System.Diagnostics;

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
        public Account Acc { get; set; }
        bool _flag = true;

        public UCConfig()
        {
            InitializeComponent();
            AllowDrag = true;
        }

        public UCConfig(Account account)
        {
            InitializeComponent();
            AllowDrag = true;
            this.Name = account.Key;
            if (account.MatchOdds == null) account.MatchOdds = new List<MatchOdd>();
            if (account.CurrentMatch == null) account.CurrentMatch = new MatchOdd();
            Acc = account;
            if (!Program.Accounts.Any(x => x.Key == account.Key))
            {
                Program.Accounts.Add(account);
            }
            ReFreshAccountView(account);
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
        
        public void ReFreshAccountView(Account account)
        {
            lblAccount.LinkColor = account.Choose == 1 ? Color.Green : Color.Red;
            lblAccount.Text = account.UserName;
            txtStake.Text = account.Stake.ToString();
            txtRate.Text = account.Rate.ToString();
            txtOdd.Text = account.Choose == 1 ? account.UnderOdd.ToString() : account.OverOdd.ToString();
            lblStakeVnd.Text = ConvertUtils.ToMoneyText(account.StakeVnd);
            cbDigit.SelectedIndex = account.Choose == 1 ? account.UnderDigit : account.OverDigit;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).Stake = int.Parse(txtStake.Text.Trim());
            Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).StakeBalance = int.Parse(txtStake.Text.Trim());
            Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).Rate = double.Parse(txtRate.Text.Trim());
            if (Acc.Choose == 0)
            {
                Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).OverOdd = double.Parse(txtOdd.Text.Trim());
                Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).OverDigit = cbDigit.SelectedIndex;
            }
            else
            {
                Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).UnderOdd = double.Parse(txtOdd.Text.Trim());
                Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).UnderDigit = cbDigit.SelectedIndex;
            }

            Program.SaveBetConfig();
            _log.InfoFormat("Acc Settings {0} saved successfully!", Acc.UserName);
        }

        public void ChangeBetType(ConfigAll config)
        {
            Acc.Choose = config.Choose;
            Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).Choose = config.Choose;
            if (Acc.Choose == 0)
            {
                Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).OverOdd = double.Parse(txtOdd.Text.Trim());
                Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).OverDigit = cbDigit.SelectedIndex;
            }
            else
            {
                Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).UnderOdd = double.Parse(txtOdd.Text.Trim());
                Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).UnderDigit = cbDigit.SelectedIndex;
            }

            ReFreshAccountView(Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key));
        }

        public void SaveConfig(ConfigAll config)
        {
            txtStake.Text = config.Stake.ToString();
            txtRate.Text = config.Rate.ToString();
            txtOdd.Text = config.OddRate.ToString();
            lblStakeVnd.Text = ConvertUtils.ToMoneyText(config.StakeVnd);
            cbDigit.SelectedIndex = config.Digit;

            Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).Stake = int.Parse(txtStake.Text.Trim());
            Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).Rate = int.Parse(txtRate.Text.Trim());
            if (Acc.Choose == 1)
            {
                Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).UnderOdd = double.Parse(txtOdd.Text.Trim());
                Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).UnderDigit = cbDigit.SelectedIndex;
            }
            else
            {
                Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).OverOdd = double.Parse(txtOdd.Text.Trim());
                Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).OverDigit = cbDigit.SelectedIndex;
            }
        }

        public void SaveConfigStakeOnly(ConfigAll config)
        {
            txtStake.Text = config.Stake.ToString();
            lblStakeVnd.Text = ConvertUtils.ToMoneyText(config.StakeVnd);
            Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).Stake = int.Parse(txtStake.Text.Trim());
        }

        private void btnLoginB_Click(object sender, EventArgs e)
        {
            Login();
        }
        public async void Login()
        {
            if (btnLoginB.Text == "Logout")
            {
                Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).LoginName = null;
                Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).Cookie = new System.Net.CookieCollection();
                btnLoginB.Text = "Login";
                btnDelete.Enabled = true;
                btnTrans.Enabled = false;
                this.BackColor = SystemColors.Info;
                _log.InfoFormat("Logout account {0} successfully!", Acc.UserName);
                return;
            }

            if (Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).UserName == "" || Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).Password == "")
            {
                MessageBox.Show("Please enter UserName & password!");
                return;
            }

            btnLoginB.Enabled = false;

            Task<Response<bool>> loginTask = Task.Run(() => new Sbobet().Login(Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key)));
            var res = await loginTask;
            if (res.Data && !res.HasError)
            {
                if (Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).Cookie.Count > 3 && !String.IsNullOrEmpty(Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).LoginName))
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

                    // get status filter
                    var combobox = (this.FindForm() as frmMain).Controls.Find("cbMatchStyle", true).FirstOrDefault() as ComboBox;
                    if (combobox != null)
                    {
                        MatchStatus status = GetMatchStatus(combobox.SelectedIndex);
                        var thread = new Thread(new ParameterizedThreadStart(OnBetStolen));
                        thread.Start(status);
                    }
                }
            }
            else
            {
                btnLoginB.Enabled = true;
                this.BackColor = SystemColors.Info;
            }
        }

        private MatchStatus GetMatchStatus(int index)
        {
            if (index == 0)
            {
                return MatchStatus.Today;
            }
            else if (index == 1)
            {
                return MatchStatus.Running;
            }
            else return MatchStatus.Early;
        }

        public async void LoginFromMain(string txt)
        {
            if (txt == "Logout")
            {
                Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).LoginName = null;
                Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).Cookie = new System.Net.CookieCollection();
                btnLoginB.Text = "Login";
                btnDelete.Enabled = true;
                btnTrans.Enabled = false;
                this.BackColor = SystemColors.Info;
                _log.InfoFormat("Logout account {0} successfully!", Acc.UserName);
                return;
            }

            if (Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).UserName == "" || Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).Password == "")
            {
                MessageBox.Show("Please enter UserName & password!");
                return;
            }

            btnLoginB.Enabled = false;

            Task<Response<bool>> loginTask = Task.Run(() => new Sbobet().Login(Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key)));
            var res = await loginTask;
            if (res.Data && !res.HasError)
            {
                if (Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).Cookie.Count > 3 && !String.IsNullOrEmpty(Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key).LoginName))
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
            var acc = Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key);
            if (acc != null) Program.Accounts.Remove(acc);
            this.Parent.Controls.Remove(this);
            Program.SaveBetConfig();
        }
        private void btnTrans_Click(object sender, EventArgs e)
        {
            WSubTran f = new WSubTran(string.Format("{0}/web-root/restricted/betlist/running-bet-list.aspx?popout=1", Acc.BaseUrl), Acc.Cookie);
            f.ShowDialog();
        }
        private void lblAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var _frmConfig = new frmAccConfig(Acc);
            _frmConfig.ShowDialog();
            ReFreshAccountView(Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key));
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
                while (true)
                {
                    if (_flag == false)
                    {
                        await Task.Delay(10);
                        continue;
                    }   

                    // go far here when get odd success

                    if (Program._isStartRunning == 1 && Acc.AccountType == 0)
                    {
                        // bet account
                        Log("starting bet: account type = {0}, flag = {1}", Acc.AccountType, Program._isStartRunning);

                        var acc = Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key);
                        if (acc != null && acc.Cookie.Count > 0 && !string.IsNullOrEmpty(acc.LoginName))
                        {
                            var odd = acc.CurrentMatch;
                            _flag = false;

                            Task<Response<bool>> loginTask = Task.Run(() => new Sbobet().BetOverUnder(acc, odd));
                            var result = await loginTask;
                            acc.LastBetId = odd.MatchId.ToString() + odd.OddType.ToString() + odd.Goal.ToString();
                            if (result.Data && !result.HasError) lbl_BetSuccess.Invoke(new Action(() => lbl_BetSuccess.Text = ConvertUtils.ToMoneyText(acc.StakeVnd)));
                        }
                    }
                    else if (Program._isStartRunning == 2 && (Acc.AccountType == 1 || Acc.AccountType == 3))
                    {
                        // odd account
                        Log("starting odd: account type = {0}, flag = {1}", Acc.AccountType, Program._isStartRunning);

                        var acc = Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key);
                        if (acc != null && acc.Cookie.Count > 0 && !string.IsNullOrEmpty(acc.LoginName))
                        {
                            var odd = acc.CurrentMatch;
                            _flag = false;

                            Task<Response<bool>> loginTask = Task.Run(() => new Sbobet().BetOverUnder(acc, odd));
                            var result = await loginTask;
                            if (result.Data && !result.HasError) lbl_BetSuccess.Invoke(new Action(() => lbl_BetSuccess.Text = ConvertUtils.ToMoneyText(acc.StakeVnd)));
                        }
                    }
                    else if (Program._isStartRunning == 3 && (Acc.AccountType == 2))
                    {
                        Log("starting ..., account type = {0}, flag = {1}", Acc.AccountType, Program._isStartRunning);

                        var acc = Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key);
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
                Log("bet error: {0}", ex.Message);
            }
        }

        private async void GetOdds()
        {
            try
            {
                var acc = Program.Accounts.SingleOrDefault(x => x.Key == Acc.Key);
                while (true)
                {
                    // not click run button yet
                    if (Program._isStartRunning != 0)
                    {
                        await Task.Delay(2000);
                        continue;
                    }

                    // login success and have cookies in account
                    if (acc != null && acc.Cookie.Count > 3 && !String.IsNullOrEmpty(acc.LoginName))
                    {
                        if (acc.CurrentMatch.MatchId != Program._currentMatch.MatchId || acc.CurrentMatch.OddType != Program._currentMatch.OddType || acc.CurrentMatch.Goal != Program._currentMatch.Goal)
                        {
                            // get odd from service
                            var result = new Sbobet().GetOdds(acc, Program.MatchFilter.MatchStyle);
                            if (!result.HasError && result.Data != null)
                            {
                                acc.MatchOdds = result.Data.Where(x => x.OddType == (int)Program.MatchFilter.OddStyle).ToList();
                                var odd = acc.MatchOdds.SingleOrDefault(x => x.Match.MatchId == Program._betMatch.MatchId && x.OddType == Program._betMatch.OddType && x.Goal == Program._betMatch.Goal);
                                acc.CurrentMatch = odd;
                                lblBetCredit.Invoke(new Action(() => lblBetCredit.Text = odd.OddId.ToString()));
                                _flag = true; // matched condition, start bet
                            }
                        }
                    }
                    // await Task.Delay(2000);
                }
            }
            catch (Exception ex)
            {
                Log("get odds error: {0}", ex.Message);
            }
        }
        

        #endregion

        private double _previousO1 = 0;
        private double _previousU1 = 0;

        /// <summary>
        /// stolen bet
        /// </summary>
        /// <param name="obj">obj is MatchStatus</param>
        public async void OnBetStolen(object obj)
        {
            MatchStatus matchStatus = (MatchStatus)obj;
            
            try
            {
                while (true)
                {
                    // get odds account
                    var service = new Sbobet();
                    var resp = service.GetOdds(this.Acc, matchStatus);

                    if (resp != null && resp.Data != null && resp.Data.Count > 0)
                    {
                        var matches = resp.Data;

                        Log("bet stolen found: {0} matches", matches.Count);


                    }

                    // delay for next run
                    await Task.Delay(100);
                }
            }
            catch (Exception ex)
            {
                Log("Bet stolen error: {0}", ex.Message);
            }
        }


        private void Log(string format, params object[] args)
        {
            var msg = "----------------------------------------";
            if (string.IsNullOrEmpty(format) == false && string.IsNullOrWhiteSpace(format) == false)
            {
                try
                {
                    format = format.Replace("{", "[{").Replace("}", "}]");
                    msg = string.Format(format, args);
                }
                catch (Exception)
                {
                    msg = string.Format("Invalid format {0} parameter: {1}", args.Length, format);
                }
            }

            Debug.WriteLine(msg);
            _log.Info(msg);
        }
    }
}
