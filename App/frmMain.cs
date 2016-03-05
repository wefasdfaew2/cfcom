using App.Model;
using log4net;
using log4net.Appender;
using ModelObjects;
using SbobetLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;
using Utils;

namespace App
{
    public partial class frmMain : Form, IAppender
    {
        private System.Media.SoundPlayer player = new System.Media.SoundPlayer();
        private static readonly ILog _log = LogManager.GetLogger(typeof(frmMain));
        public void DoAppend(log4net.Core.LoggingEvent loggingEvent)
        {
            lblMess.Invoke(new Action(() => lblMess.Items.Add(String.Format("{0}: {1}", loggingEvent.TimeStamp.ToString("dd/MM HH:mm:ss.fff"), loggingEvent.MessageObject.ToString()))));
        }
        public frmMain()
        {
            InitializeComponent();

            this.flpBet.DragEnter += new DragEventHandler(flpBet_DragEnter);
            this.flpBet.DragDrop += new DragEventHandler(flpBet_DragDrop);
            this.flpOdd.DragEnter += new DragEventHandler(flpBet_DragEnter);
            this.flpOdd.DragDrop += new DragEventHandler(flpBet_DragDrop);

            InitTable();            
        }

        void flpBet_DragDrop(object sender, DragEventArgs e)
        {
            UCConfig data = (UCConfig)e.Data.GetData(typeof(UCConfig));
            FlowLayoutPanel _destination = (FlowLayoutPanel)sender;
            FlowLayoutPanel _source = (FlowLayoutPanel)data.Parent;

            if (_source != _destination)
            {
                // Add control to panel
                _destination.Controls.Add(data);
                data.Size = new Size(_destination.Width, 50);

                // Reorder
                Point p = _destination.PointToClient(new Point(e.X, e.Y));
                var item = _destination.GetChildAtPoint(p);
                int index = _destination.Controls.GetChildIndex(item, false);
                _destination.Controls.SetChildIndex(data, index);

                // Invalidate to paint!
                _destination.Invalidate();
                _source.Invalidate();
            }
            else
            {
                // Just add the control to the new panel.
                // No need to remove from the other panel, this changes the Control.Parent property.
                
                Point p = _destination.PointToClient(new Point(e.X, e.Y));
                var item = _destination.GetChildAtPoint(p);
                int i = 1;
                while (item == null)
                {
                    p = _destination.PointToClient(new Point(e.X, e.Y + i));
                    item = _destination.GetChildAtPoint(p);
                    i++;
                }
                int index = _destination.Controls.GetChildIndex(item, false);
                _destination.Controls.SetChildIndex(data, index);
                _destination.Invalidate();
                UpdateAccountPos(_destination);
            }
        }

        void flpBet_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        void UpdateAccountPos(object sender)
        {
            FlowLayoutPanel flp = (FlowLayoutPanel)sender;
            int i = 0;
            foreach (var item in flp.Controls)
            {
                var x = (UCConfig)item;
                var acc = Program.Accounts.SingleOrDefault(u=>u.Key==x.Name);
                acc.Pos = i;
                i++;
            }
            Program.SaveBetConfig();
        }

        private void btnAddGetAccount_Click(object sender, EventArgs e)
        {
            var account = new Account()
            {
                Key = Guid.NewGuid().ToString(),
                UserName = "",
                Password = "",
                FakeIp = "",
                Cookie = new System.Net.CookieCollection()
            };
            flpGetAccounts.Controls.Add(new UCAccount(account));
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadGetAccount();
            LoadBetAccount();
            LoadMatchSettings();
            lblMess.SelectedIndex = -1;
        }

        private void addCol(string colCap, int colWidth, int pos, Color? color)
        {
            grvMatch.Columns[pos].HeaderText = colCap;
            grvMatch.Columns[pos].Width = colWidth;
            if (color != null) grvMatch.Columns[pos].DefaultCellStyle.BackColor = color.Value;
        }
        void InitTable()
        {
            Program._dtMatch.Columns.Add("MatchId", typeof(int));            
            Program._dtMatch.Columns.Add("Contest", typeof(String));
            Program._dtMatch.Columns.Add("Home", typeof(String));
            Program._dtMatch.Columns.Add("Away", typeof(String));
            Program._dtMatch.Columns.Add("OddTypeDisplay", typeof(String));
            Program._dtMatch.Columns.Add("Goal", typeof(String));
            Program._dtMatch.Columns.Add("Over", typeof(double));
            Program._dtMatch.Columns.Add("Under", typeof(double));

            grvMatch.DataSource = Program._dtMatch;

            addCol("Id", 60, 0, null);            
            addCol("Contest", 200, 1, null);
            addCol("Home", 100, 2, null);
            addCol("Away", 100, 3, null);
            addCol("T", 50, 4, Color.LightSkyBlue);
            addCol("Goal", 50, 5, Color.LightSkyBlue);
            addCol("Over", 70, 6, Color.Yellow);
            addCol("Under", 70, 7, Color.Lime);
        }
        private void addTable(MatchOdd _data)
        {
            try
            {
                Program._dtMatch.Rows.Add(new object[] {
               _data.Match.MatchId,               
               _data.Match.Contest,
               _data.Match.Home,
               _data.Match.Away,
               _data.OddTypeDisplay,
               _data.Goal,
               _data.OddType==3? _data.o: _data.OddType==9?_data.o1:0,
               _data.OddType==3? _data.u: _data.OddType==9?_data.u1:0
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void LoadGetAccount()
        {
            Program.LoadConfig();
            flpGetAccounts.Controls.Clear();
            foreach (var account in Program.AccountGets.Where(x => x.Key != null))
            {
                flpGetAccounts.Controls.Add(new UCAccount(account));
            }
        }
        void LoadBetAccount()
        {
            Program.LoadBetConfig();
            flpBet.Controls.Clear();
            flpOdd.Controls.Clear();
            foreach (var account in Program.Accounts.Where(x => x.AccountType == 0).OrderBy(x => x.Pos))
            {
                flpBet.Controls.Add(new UCConfig(account));
            }
            foreach (var account in Program.Accounts.Where(x => x.AccountType == 1).OrderBy(x => x.Pos))
            {
                flpOdd.Controls.Add(new UCConfig(account));
            }
            foreach (var account in Program.Accounts.Where(x => x.AccountType == 2).OrderBy(x => x.Pos))
            {
                flpBuy.Controls.Add(new UCConfig(account));
            }
            foreach (var account in Program.Accounts.Where(x => x.AccountType == 3).OrderBy(x => x.Pos))
            {
                flpKeep.Controls.Add(new UCConfig(account));
            }
        }
        void LoadMatchSettings()
        {
            Program.LoadMatchSettings();
            cbMatchStyle.SelectedIndex = (int)Program.MatchFilter.MatchStyle;
            int odstyle = (int)Program.MatchFilter.OddStyle;
            cbOddStyle.SelectedIndex = odstyle == 3 ? 0 : odstyle == 9 ? 1 : odstyle==1?2:3;
        }
        public async void ViewMatchInfo(Account acc)
        {
            try
            {
                Program.SaveMatchSettings();
                if (acc != null)
                {
                    var result = new Sbobet().GetOdds(acc, Program.MatchFilter.MatchStyle);
                    if (!result.HasError && result.Data != null)
                    {
                        Program._matchs = result.Data.Where(x => x.OddType == (int)Program.MatchFilter.OddStyle).ToList();//Keo tai xiu (tran, h1)
                        Program._dtMatch.Clear();
                        acc.MatchOdds = Program._matchs;
                        foreach (var item in Program._matchs)
                        {
                            addTable(item);
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                lblMess.Invoke(new Action(() => lblMess.Text = "Get Account Infomation Failed: " + ex.Message));
            }
        }

        private async void btnF5Match_Click(object sender, EventArgs e)
        {
            var acc = Program.AccountGets.Where(x => x.Cookie.Count > 0 && x.UserName != null).FirstOrDefault();
            Program.MatchFilter.MatchStyle = cbMatchStyle.SelectedIndex == 0 ? MatchStatus.Today : cbMatchStyle.SelectedIndex == 1 ? MatchStatus.Running : MatchStatus.Early;
            Program.MatchFilter.OddStyle = cbOddStyle.SelectedIndex == 0 ? OddsStatus.OU : cbMatchStyle.SelectedIndex == 1 ? OddsStatus.OUH1 :cbMatchStyle.SelectedIndex == 2 ? OddsStatus.FT: OddsStatus.H1;
            ViewMatchInfo(acc);
        }

        private void grvMatch_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                if (Program.___flag == 0 || (Program.___flag > 0 && MessageBox.Show("Chọn trận khác, chắc chắn trận đã xong trân đang chay?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes))
                {
                    int matchId = int.Parse(grvMatch.Rows[e.RowIndex].Cells[0].Value.ToString());
                    int oddType = grvMatch.Rows[e.RowIndex].Cells[4].Value.ToString() == "H1" ? 9 : 3;
                    double goal = double.Parse(grvMatch.Rows[e.RowIndex].Cells[5].Value.ToString());
                    Program._betMatch = new MatchBet() { MatchId = matchId, OddType = oddType, Goal = goal };

                    var acc = Program.AccountGets.FirstOrDefault(x => x.Cookie.Count > 0 && x.LoginName != null);
                    if (acc != null)
                    {
                        var odd = acc.MatchOdds.SingleOrDefault(x => x.MatchId == Program._betMatch.MatchId && x.OddType == Program._betMatch.OddType && x.Goal == Program._betMatch.Goal);
                        if (odd != null)
                        {
                            Program.___flag = 0;
                            Program._currentMatch = odd;
                            btnStart.Enabled = true;
                            btnStop.Enabled = false;
                            Program._lastBetId = odd.MatchId.ToString() + odd.OddType.ToString() + odd.Goal.ToString();
                            lblMatchSelect.Text = string.Format("{0} - {1}: {2} vs {3} ( {4} - {5} )", matchId, odd.Match.Contest, odd.Match.Home, odd.Match.Away, odd.OddTypeDisplay, odd.Goal);
                            lblOverOdd.Text = Program._betMatch.OddType == 3 ? odd.o.ToString() : odd.o1.ToString();
                            lblUnderOdd.Text = Program._betMatch.OddType == 3 ? odd.u.ToString() : odd.u1.ToString();                            
                        }
                    }
                }
            }
        }

        private void btnLoginAllGetAccount_Click(object sender, EventArgs e)
        {
            var t = btnLoginAllGetAccount.Text;
            foreach (var item in flpGetAccounts.Controls)
            {
                try
                {
                    var x = (UCAccount)item;
                    x.LoginFromMain(t == "Login All" ? "Login" : "Logout");
                }
                catch (Exception) { }
            }
            btnLoginAllGetAccount.Text = t == "Login All" ? "Logout All" : "Login All";
        }

        private void btnAddBetAccount_Click(object sender, EventArgs e)
        {
            var account = new Account()
            {
                Key = Guid.NewGuid().ToString(),
                Pos = Program.Accounts.Count,
                UserName = "AccountBET-" + flpBet.Controls.Count.ToString(),
                Password = "",
                FakeIp = "",
                Stake = 10,
                Rate = 3000,
                OverOdd = 0,
                UnderOdd = 0,
                Choose = 0,
                OverDigit = 0,
                UnderDigit = 0,
                Wait = 0,
                Cookie = new System.Net.CookieCollection(),
                BetType = 0
            };
            flpBet.Controls.Add(new UCConfig(account));
        }

        private void btnAddOddAccount_Click(object sender, EventArgs e)
        {
            var account = new Account()
            {
                Key = Guid.NewGuid().ToString(),
                Pos = Program.Accounts.Count,
                UserName = "AccountODD-" + flpOdd.Controls.Count.ToString(),
                Password = "",
                FakeIp = "",
                Stake = 10,
                Rate = 22000,
                OverOdd = 0,
                UnderOdd = 0,
                Choose = 1,
                OverDigit = 0,
                UnderDigit = 0,
                Wait = 0,
                Cookie = new System.Net.CookieCollection(),
                BetType = 1
            };
            flpOdd.Controls.Add(new UCConfig(account));
        }

        private async void btnRun_Click(object sender, EventArgs e)
        {            
            Program.___flag = 1;
            _log.Info("Start Betting ..");
            btnStart.Enabled = false;
            btnStop.Enabled = true;
    
            new Thread(new ThreadStart(CheckStatus)).Start();

        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            Program.___flag = 0;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            _log.Info("Stop Betting ..");
        }

        private void btnLoginAllBet_Click(object sender, EventArgs e)
        {
            var t = btnLoginAllBet.Text;
            foreach (var item in flpBet.Controls)
            {
                try
                {
                    var x = (UCConfig)item;
                    x.LoginFromMain(t == "Login All" ? "Login" : "Logout");
                }
                catch (Exception) { }
            }

            btnLoginAllBet.Text = t == "Login All" ? "Logout All" : "Login All";
        }

        private void btnLoginAllOdd_Click(object sender, EventArgs e)
        {
            foreach (var item in flpOdd.Controls)
            {
                try
                {
                    var x = (UCConfig)item;
                    x.Login();
                }
                catch (Exception) { }
            }
            var t = btnLoginAllOdd.Text;
            btnLoginAllOdd.Text = t == "Login All" ? "Logout All" : "Login All";
        }

        private async void CheckStatus()
        {
            try
            {
                while (1 == 1)
                {
                    //Sl da bet trong crmatch
                    var b = Program.Accounts.Count(x => x.AccountType == 0);
                    var c = Program.Accounts.Count(x => x.LastBetId == Program._lastBetId && x.AccountType == 0);
                   // _log.InfoFormat("count {0} - {1}", b, c);
                    if (c>=b-1 && Program.___flag==1)
                    {
                        _log.Info("Start Odd!");
                        Program.___flag = 2;
                    }
                    await Task.Delay(100);
                }                
            }
            catch (Exception ex)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var accs = Program.Accounts.Where(x => x.Choose == 0 && x.Cookie.Count > 0);
            var accs1 = Program.Accounts.Where(x => x.Choose == 1 && x.Cookie.Count > 0);
            Task.Factory.StartNew(() => Parallel.ForEach(accs, (acc) =>
            //foreach (var acc in )
            {
                var odd = acc.CurrentMatch;// acc.MatchOdds.SingleOrDefault(x => x.Match.MatchId == Program._betMatch.MatchId && x.OddType == Program._betMatch.OddType && x.Goal == Program._betMatch.Goal);
                //Task<Response<bool>> loginTask = Task.Run(() => new Sbobet().BetOverUnder(acc, odd));
                new Sbobet().BetOverUnder(acc, odd);
            }));

            //.ContinueWith((antecedent) => {
            //    Parallel.ForEach(accs1, (acc) =>
            //    {
            //        var odd = acc.CurrentMatch;// acc.MatchOdds.SingleOrDefault(x => x.Match.MatchId == Program._betMatch.MatchId && x.OddType == Program._betMatch.OddType && x.Goal == Program._betMatch.Goal);
            //        new Sbobet().BetOverUnder(acc, odd);
            //    });
            //}); 

        }

        private void btnAddBuy_Click(object sender, EventArgs e)
        {
            // ashgajhs
            var account = new Account()
            {
                Key = Guid.NewGuid().ToString(),
                Pos = Program.Accounts.Count,
                UserName = "AccountBUY-" + flpBuy.Controls.Count.ToString(),
                Password = "",
                FakeIp = "",
                Stake = 10,
                Rate = 22000,
                OverOdd = 0,
                UnderOdd = 0,
                Choose = 0,
                AccountType=2,
                OverDigit = 0,
                UnderDigit = 0,
                Wait = 0,
                Cookie = new System.Net.CookieCollection(),
                BetType = 0
            };
            flpBuy.Controls.Add(new UCConfig(account));
        }

        private void btnAddKeep_Click(object sender, EventArgs e)
        {
            var account = new Account()
            {
                Key = Guid.NewGuid().ToString(),
                Pos = Program.Accounts.Count,
                UserName = "AccountKEEP-" + flpKeep.Controls.Count.ToString(),
                Password = "",
                FakeIp = "",
                Stake = 10,
                Rate = 3000,
                OverOdd = 0,
                UnderOdd = 0,
                Choose = 0,
                AccountType = 3,
                OverDigit = 0,
                UnderDigit = 0,
                Wait = 0,
                Cookie = new System.Net.CookieCollection(),
                BetType = 0
            };
            flpKeep.Controls.Add(new UCConfig(account));
        }

        private void btnLoginAllBuy_Click(object sender, EventArgs e)
        {
            var t = btnLoginAllBuy.Text;
            foreach (var item in flpBuy.Controls)
            {
                try
                {
                    var x = (UCConfig)item;
                    x.LoginFromMain(t == "Login All" ? "Login" : "Logout");
                }
                catch (Exception) { }
            }

            btnLoginAllBuy.Text = t == "Login All" ? "Logout All" : "Login All";
        }

        private void btnLoginAllKeep_Click(object sender, EventArgs e)
        {
            var t = btnLoginAllKeep.Text;
            foreach (var item in flpKeep.Controls)
            {
                try
                {
                    var x = (UCConfig)item;
                    x.LoginFromMain(t == "Login All" ? "Login" : "Logout");
                }
                catch (Exception) { }
            }

            btnLoginAllKeep.Text = t == "Login All" ? "Logout All" : "Login All";
        }

        private void btnApplyStake_Click(object sender, EventArgs e)
        {
            if (txtStakeBet.Text.Trim() == "")
            {
                MessageBox.Show("Please insert stake!");
                return;
            }
            foreach (var item in flpBet.Controls)
            {
                try
                {
                    var x = (UCConfig)item;
                    var config = new ConfigAll()
                    {
                        Stake = ConvertUtils.ToDouble(txtStakeBet.Text.Trim())
                    };

                    x.SaveConfigStakeOnly(config);
                }
                catch (Exception) { }
            }
            Program.SaveBetConfig();
        }

        private void btnApplyStakeOdd_Click(object sender, EventArgs e)
        {
            if (txtStakeOdd.Text.Trim() == "")
            {
                MessageBox.Show("Please insert stake!");
                return;
            }
            foreach (var item in flpOdd.Controls)
            {
                try
                {
                    var x = (UCConfig)item;
                    var config = new ConfigAll()
                    {
                        Stake = ConvertUtils.ToDouble(txtStakeOdd.Text.Trim())
                    };

                    x.SaveConfigStakeOnly(config);
                }
                catch (Exception) { }
            }
            Program.SaveBetConfig();
        }

        private void btnApplyStakeKeep_Click(object sender, EventArgs e)
        {
            if (txtStakeKeep.Text.Trim() == "")
            {
                MessageBox.Show("Please insert stake!");
                return;
            }
            foreach (var item in flpKeep.Controls)
            {
                try
                {
                    var x = (UCConfig)item;
                    var config = new ConfigAll()
                    {
                        Stake = ConvertUtils.ToDouble(txtStakeKeep.Text.Trim())
                    };

                    x.SaveConfigStakeOnly(config);
                }
                catch (Exception) { }
            }
            Program.SaveBetConfig();
        }

        private void btnApplyStakeBuy_Click(object sender, EventArgs e)
        {
            if (txtStakeBuy.Text.Trim() == "")
            {
                MessageBox.Show("Please insert stake!");
                return;
            }
            foreach (var item in flpBuy.Controls)
            {
                try
                {
                    var x = (UCConfig)item;
                    var config = new ConfigAll()
                    {
                        Stake = ConvertUtils.ToDouble(txtStakeBuy.Text.Trim())
                    };

                    x.SaveConfigStakeOnly(config);
                }
                catch (Exception) { }
            }
            Program.SaveBetConfig();
        }

        private void btnBetAllBuy_Click(object sender, EventArgs e)
        {
            Program.___flag = 3;
        }

        SemaphoreSlim _mutex = new SemaphoreSlim(10);
        private void button1_Click_1(object sender, EventArgs e)
        {

            _log.Info(System.Diagnostics.Process.GetCurrentProcess().Threads.Count);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text File | *.txt";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) 
            {
                string file = openFileDialog1.FileName;

                if (!File.Exists(file))
                {
                    throw new FileNotFoundException();
                }

                var appSettings = Properties.Settings.Default;
                try
                {
                    // Open settings file as XML
                    var import = XDocument.Load(file);
                    // Get the <setting> elements
                    var settings = import.XPathSelectElements("//setting");
                    foreach (var setting in settings)
                    {
                        string name = setting.Attribute("name").Value;
                        string value = setting.XPathSelectElement("value").FirstNode.ToString();
                        try
                        {
                            appSettings[name] = value; 
                        }
                        catch (SettingsPropertyNotFoundException ex)
                        {
                        }
                    }

                    LoadGetAccount();
                    LoadBetAccount();
                    LoadMatchSettings();

                    Program.SaveConfig();
                    Program.SaveBetConfig();
                    Program.SaveMatchSettings();
                }
                catch (Exception exc)
                {
                    appSettings.Reload(); 
                }                
            }
            
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "CFCOMSettings.txt";
            saveFileDialog1.Filter = "Text File | *.txt";
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string name = saveFileDialog1.FileName;
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            config.SaveAs(name); 
        }

    }
}