using App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ModelObjects
{
    public class Account
    {
        public string Key { get; set; }
        public string HostUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string FakeIp { get; set; }
        public CookieCollection Cookie { get; set; }
        public string BaseUrl { get; set; }
        public string LoginName { get; set; }
        public string HomeUrl { get; set; }
        public double Stake { get; set; }
        public double Rate { get; set; }
        public double StakeVnd
        {
            get { return this.Rate * this.Stake; }
            set { }
        }
        public double StakeBalance { get; set; }
        public double StakeVndBalance
        {
            get { return this.Rate * this.StakeBalance; }
            set { }
        }
        public double OverOdd { get; set; }
        public int OverDigit { get; set; }
        public int UnderDigit { get; set; }
        public double UnderOdd { get; set; }
        public int Choose { get; set; }
        public int AccountType { get; set; }
        public double Wait { get; set; }
        public int Pos { get; set; }
        public int BetType { get; set; }
        public MatchOdd CurrentMatch { get; set; }
        public List<MatchOdd> MatchOdds { get; set; }
        public string LastBetId { get; set; }
    }
}
