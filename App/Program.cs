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
            ServicePointManager.DefaultConnectionLimit = 500;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.MaxServicePoints = 500;
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
    }
}
