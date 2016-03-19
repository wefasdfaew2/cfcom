using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Model
{
    public class Match
    {
        public int MatchId { get; set; }
        public int MatchNumber { get; set; }
        public int ContestId { get; set; }
        public string Contest { get; set; }
        public string Home { get; set; }
        public string Away { get; set; }
        public DateTime StartDate { get; set; }
        public int Period { get; set; }
        public int Minute { get; set; }
        public List<MatchOdd> MatchOdds { get; set; }
    }

    public class MatchOdd
    {
        public int OddId { get; set; }
        public int HdpType { get; set; }
        public string OddTypeDisplay { get; set; }
        public double Goal { get; set; }
        public int OddType { get; set; }
        public double h { get; set; } // home
        public double a { get; set; }
        public double o { get; set; } // over
        public double u { get; set; } // under
        public double h1 { get; set; }
        public double a1 { get; set; }
        public double o1 { get; set; } // first match over
        public double u1 { get; set; } // first match under
        public int MatchId { get; set; }
        public virtual Match Match { get; set; }
    }
}
