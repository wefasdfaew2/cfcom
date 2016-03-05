using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelObjects
{
    public class AccountConfig
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FakeIp { get; set; }
        public string HostUrl { get; set; }
    }

    public class AccountBetConfig
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FakeIp { get; set; }
        public string HostUrl { get; set; }
        public double Stake { get; set; }
        public double Rate { get; set; }
        public double OverOdd { get; set; }
        public int OverDigit { get; set; }
        public int UnderDigit { get; set; }
        public double UnderOdd { get; set; }
        public int Choose { get; set; }
        public int AccountType { get; set; }
        public double Wait { get; set; }
        public int Pos { get; set; }
    }

}
