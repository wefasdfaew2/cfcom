using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelObjects
{
    public class Config
    {
        public string App { get; set; }
        public int BetType { get; set; }
        public string Accounts { get; set; }
        public int Stake { get; set; }
        public double OverOdd { get; set; }
        public double UnderOdd { get; set; }
        public int Choose { get; set; }
    }

    public class ConfigAll
    {
        public string App { get; set; }
        public int Digit { get; set; }
        public double OddRate { get; set; }
        public double Stake { get; set; }
        public double Rate { get; set; }
        public double StakeVnd { get { return this.Stake * this.Rate; } set { } }
        public int Choose { get; set; }
    }
}
