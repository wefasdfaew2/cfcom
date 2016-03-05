using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelObjects
{
    public class BetResult
    {
        public string UserName { get; set; }
        public int Oid { get; set; }
        public int Status { get; set; } 
    }

    public class MatchBet
    {
        public int MatchId { get; set; }
        public int OddType { get; set; }
        public double Goal { get; set; }
    }

    public class OddBet
    {
        public int OddId { get; set; }
        public int HdpType { get; set; }
        public double Odd { get; set; }
    }
}
