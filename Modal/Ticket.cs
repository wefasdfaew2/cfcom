using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelObjects
{
    public class SbobetTicket
    {
        public int MatchOddId { get; set; }
        public string Op { get; set; }
        public double Odds { get; set; }
        public int HdpType { get; set; }
        public int IsOr { get; set; }
        public int IsLive { get; set; }
        public int BetPage { get; set; }
        public int Style { get; set; }

        public int SameTicket { get; set; }
        public int Betcount { get; set; }
        public double Stake { get; set; }
        public double StakeInAuto { get; set; }
        public int AcceptIfAny { get; set; }
        public int AutoProcess { get; set; }
        public int AutoRefresh { get; set; }
        public int TimeDiff { get; set; }
    }
}
