using App.Model;
using log4net.Config;
using ModelObjects;
using Newtonsoft.Json;
using SbobetLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        #region static variable
        public static MatchSettings MatchFilter = new MatchSettings();
        public static List<Account> Accounts = new List<Account>();
        public static List<Account> AccountGets = new List<Account>();
        public static DataTable _dtMatch = new DataTable();
        public static List<MatchOdd> _matchs = new List<MatchOdd>();
        public static MatchBet _betMatch = new MatchBet();
        public static MatchOdd _currentMatch = new MatchOdd();
        public static List<BetResult> BetResults = new List<BetResult>();
        public static string  _lastBetId = "";
        public static int ___flag = 0;
        #endregion

        [STAThread]
        static void Main()
        {
            XmlConfigurator.Configure();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ServicePointManager.Expect100Continue = false;
            var frmMain = new frmMain();
            ((log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetLoggerRepository()).Root.AddAppender(frmMain);
            Application.Run(frmMain);
            System.Environment.Exit(1);
        }

        public static void LoadConfig()
        {
            AccountGets.Clear();
            if (!string.IsNullOrEmpty((Properties.Settings.Default.AccountGets)))
            {
                foreach (var acc in JsonConvert.DeserializeObject<List<AccountConfig>>(Properties.Settings.Default.AccountGets))
                {
                    var account = new Account();
                    account.Key = Guid.NewGuid().ToString();
                    account.UserName = acc.UserName;
                    account.Password = acc.Password;
                    account.FakeIp = acc.FakeIp;
                    account.HostUrl = acc.HostUrl;
                    account.Cookie = new CookieCollection();
                    AccountGets.Add(account);
                }
            }
        }

        public static void SaveConfig()
        {
            List<AccountConfig> accs = new List<AccountConfig>();
            foreach (var item in AccountGets)
            {
                accs.Add(new AccountConfig()
                {
                    UserName = item.UserName,
                    Password = item.Password,
                    FakeIp = item.FakeIp,
                    HostUrl = item.HostUrl
                });
            }
            Properties.Settings.Default.AccountGets = JsonConvert.SerializeObject(accs);
            Properties.Settings.Default.Save();
        }

        public static void SaveMatchSettings()
        {
            Properties.Settings.Default.MatchSettings = JsonConvert.SerializeObject(MatchFilter);
            Properties.Settings.Default.Save();
        }
        public static void LoadMatchSettings()
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.MatchSettings))
                MatchFilter = JsonConvert.DeserializeObject<MatchSettings>(Properties.Settings.Default.MatchSettings);
        }

        public static void LoadBetConfig()
        {
            Accounts.Clear();
            if (!string.IsNullOrEmpty((Properties.Settings.Default.Accounts)))
            {
                foreach (var acc in JsonConvert.DeserializeObject<List<AccountBetConfig>>(Properties.Settings.Default.Accounts))
                {
                    var account = new Account();
                    account.Key = Guid.NewGuid().ToString();
                    account.UserName = acc.UserName;
                    account.Password = acc.Password;
                    account.FakeIp = acc.FakeIp;
                    account.HostUrl = acc.HostUrl;
                    account.Choose= acc.Choose;
                    account.AccountType = acc.AccountType;
                    account.OverDigit= acc.OverDigit;
                    account.UnderDigit=acc.UnderDigit;
                    account.OverOdd= acc.OverOdd;
                    account.UnderOdd= acc.UnderOdd;
                    account.Pos= acc.Pos;
                    account.Rate= acc.Rate;
                    account.Stake=acc.Stake;
                    account.Wait = acc.Wait;
                    account.Cookie = new CookieCollection();
                    Accounts.Add(account);
                }
            }
        }
        public static void SaveBetConfig()
        {
            List<AccountBetConfig> accs = new List<AccountBetConfig>();
            foreach (var item in Accounts)
            {
                accs.Add(new AccountBetConfig()
                {
                    UserName = item.UserName,
                    Password = item.Password,
                    FakeIp = item.FakeIp,
                    HostUrl = item.HostUrl,
                    Choose= item.Choose,
                    AccountType= item.AccountType,
                    OverDigit= item.OverDigit,
                    UnderDigit=item.UnderDigit,
                    OverOdd= item.OverOdd,
                    UnderOdd= item.UnderOdd,
                    Pos= item.Pos,
                    Rate= item.Rate,
                    Stake=item.Stake,
                    Wait=item.Wait                    
                });
            }
            Properties.Settings.Default.Accounts = JsonConvert.SerializeObject(accs);
            Properties.Settings.Default.Save();
        }
        static void TestGet()
        {
            var result = new Sbobet().Login(acc);
            if (result.Data)
            {
                var oddResult = new Sbobet().GetRunningOdds(acc);
            }
        }
        static void TestBet()
        {
            var result = new Sbobet().Login(acc);
            if (result.Data)
            {
                var oddResult = new Sbobet().GetRunningOdds(acc);
                if (!oddResult.HasError && oddResult.Data.Count > 0)
                {
                    var odd = oddResult.Data[0];
                    SbobetTicket ticket = new SbobetTicket();
                    //Home:id=30292694&op=h&odds=-0.91&hdpType=1&isor=0&isLive=0&betpage=18&style=1
                    //Away:id=30292694&op=a&odds=0.85&hdpType=1&isor=0&isLive=0&betpage=18&style=1
                    //Over:id=30170989&op=h&odds=0.90&hdpType=1&isor=0&isLive=0&betpage=18&style=1
                    //Under:id=30170989&op=a&odds=-1.00&hdpType=1&isor=0&isLive=0&betpage=18&style=1

                    //Request
                    ticket.MatchOddId = odd.OddId;
                    ticket.HdpType = 1; //Live =2 , today=1
                    ticket.Op = "h";                   
                    ticket.Odds = odd.h; //Home , Over =h , Away, Under = a
                    ticket.IsLive = 0;
                    ticket.IsOr = 0;
                    ticket.BetPage = 18;//check
                    ticket.Style = 1;

                    //sameticket=0&betcount=0&stake=10&ostyle=1&stakeInAuto=10&betpage=18&acceptIfAny=1&autoProcess=0&autoRefresh=0&oid=30292694&timeDiff=6735
                    //Confirm
                    ticket.SameTicket = 0;
                    ticket.Betcount = 0;
                    ticket.Stake = 10;
                    ticket.StakeInAuto = 10;
                    ticket.AcceptIfAny = 1;
                    ticket.AutoProcess = 0;
                    ticket.AutoRefresh = 0;  //check                                
                    ticket.TimeDiff = 6735;

                    var betresult = new Sbobet().Bet(acc, ticket);
                }
            }
        }
    }
}
