using App.Model;
using ModelObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SbobetLib
{
    public class Str2Obj
    {
        public Response<List<MatchOdd>> RunningOdds(string s)
        {
            Response<List<MatchOdd>> res = new Response<List<MatchOdd>>();
            List<Match> matchs = new List<Match>();
            List<MatchOdd> odds = new List<MatchOdd>();
            res.Data = odds;
            try
            {                
                s = s.Replace("\\u200C", "");
                if (s.Length < 100) return res;
                var m = s.Replace("\\u200C", "").Split(new string[1] { "]],[[", }, StringSplitOptions.None);
                var m1 = m[1].Replace("[", "").Split(new string[1] { "],", }, StringSplitOptions.None);
                foreach (var item in m1)
                {
                    var k = item.Split(',');
                    Match match = new Match();
                    match.MatchId = int.Parse(k[0]);
                    match.ContestId = int.Parse(k[2]);
                    match.Home = k[3];
                    match.Away = k[4];
                    match.MatchOdds = new List<MatchOdd>();
                    matchs.Add(match);
                }

                var m2 = m[2].Replace("[", "").Split(new string[1] { "],", }, StringSplitOptions.None);
                foreach (var item in m2)
                {
                    var k = item.Split(',');
                    Match match = matchs.SingleOrDefault(x => x.MatchId == int.Parse(k[1]) && k[2]=="0");
                    if(match!=null) match.MatchNumber = int.Parse(k[0]);
                }

                var md = m[3].Split(new string[1] { "]],,[[", }, StringSplitOptions.None)[0];
                var md1 = md.Split(new string[1] { "],[" }, StringSplitOptions.None);
                foreach (var item in md1)
                {
                    var k = item.Split(',');
                    Match match = matchs.SingleOrDefault(x => x.MatchNumber == int.Parse(k[0]));
                    if (match != null)
                    {
                        match.Period = int.Parse(k[2]);
                        match.Minute = int.Parse(k[3]);
                    }
                }

                var m3 = m[3].Split(new string[1] { "]],,[[", }, StringSplitOptions.None)[1];
                var m31 = m3.Split(new string[1] { "]],[" }, StringSplitOptions.None);
                foreach (var item in m31)
                {
                    var k = item.Replace("[", "").Replace("]", "").Split(',');
                    Match match = matchs.SingleOrDefault(x => x.MatchNumber == int.Parse(k[1]));
                    if (match != null)
                    {
                        MatchOdd odd;
                        odd = new MatchOdd();
                        odd.Match = match;
                        odd.MatchId = match.MatchId;
                        odd.OddId = int.Parse(k[0]);
                        odd.Goal = Double.Parse(k[5]);
                        odd.OddType = int.Parse(k[2]);
                        odd.OddTypeDisplay = odd.OddType == 1 ? "H" : odd.OddType == 3 ? "H" : odd.OddType == 7 ? "H1" : odd.OddType == 9 ? "H1" : "";
                        odd.HdpType = 2; //Live
                        switch (k[2])
                        {
                            case "1":
                                odd.h = double.Parse(k[6]);
                                odd.a = double.Parse(k[7]);
                                break;
                            case "3":
                                odd.o = double.Parse(k[6]);
                                odd.u = double.Parse(k[7]);
                                break;
                            case "7":
                                odd.h1 = double.Parse(k[6]);
                                odd.a1 = double.Parse(k[7]);
                                break;
                            case "9":
                                odd.o1 = double.Parse(k[6]);
                                odd.u1 = double.Parse(k[7]);
                                break;
                        }
                        odds.Add(odd);
                    }
                }
                res.Data = odds;
            }
            catch (Exception ex)
            {
                res.Errors.Add("Convert Sbobet Running Odds Errors: " + ex.Message);
                res.Data = odds;
            }
            return res;
        }

        public Response<List<MatchOdd>> TodayOdds(string s)
        {
            Response<List<MatchOdd>> res = new Response<List<MatchOdd>>();
            List<Match> matchs = new List<Match>();
            List<MatchOdd> odds = new List<MatchOdd>();
            res.Data = odds;
            try
            {
                s = s.Replace("\\u200C", "");
                if (s.Length < 100) return res;
                var m = s.Replace("\\u200C", "").Split(new string[1] { "]],[[", }, StringSplitOptions.None);

                var m1 = m[1].Replace("[", "").Split(new string[1] { "],", }, StringSplitOptions.None);
                foreach (var item in m1)
                {
                    var k = item.Split(',');
                    Match match = new Match();
                    match.MatchId = int.Parse(k[0]);
                    match.ContestId = int.Parse(k[2]);
                    match.Home = k[3].Replace("'", "");
                    match.Away = k[4].Replace("'", "");
                    match.MatchOdds = new List<MatchOdd>();
                    matchs.Add(match);
                }

                var m0 = m[0].Substring(m[0].IndexOf("[[[") + 3, m[0].Length - (m[0].IndexOf("[[[") + 3)).Split(new string[1] { "],[", }, StringSplitOptions.None);
                foreach (var item in m0)
                {
                    var k = item.Split(',');
                    foreach (var match in matchs.Where(x => x.ContestId == int.Parse(k[0])))
                    {
                        match.Contest = k[1].Replace("'","");
                    }
                }

                var m2 = m[2].Replace("[", "").Split(new string[1] { "]],,," }, StringSplitOptions.None);
                var m20 = m2[0].Split(new string[1] { "]," }, StringSplitOptions.None);
                foreach (var item in m20)
                {
                    var k = item.Split(',');
                    Match match = matchs.SingleOrDefault(x => x.MatchId == int.Parse(k[1]));
                    if (match != null) match.MatchNumber = int.Parse(k[0]);
                }

                var m3 = m2[1].Split(new string[1] { "]],"}, StringSplitOptions.None);
                foreach (var item in m3)
                {
                    var k = item.Replace("[", "").Replace("]", "").Split(',');
                    Match match = matchs.SingleOrDefault(x => x.MatchNumber == int.Parse(k[1]));
                    if (match != null)
                    {
                        MatchOdd odd;
                        odd = new MatchOdd();
                        odd.Match = match;
                        odd.MatchId = match.MatchId;
                        odd.OddId = int.Parse(k[0]);
                        odd.Goal = double.Parse(k[5]);
                        odd.OddType = int.Parse(k[2]);
                        odd.OddTypeDisplay = odd.OddType == 1 ? "H" : odd.OddType == 3 ? "H" : odd.OddType == 7 ? "H1" : odd.OddType == 9 ? "H1" : "";
                        odd.HdpType = 1; //Live
                        switch (k[2])
                        {
                            case "1":
                                odd.h = double.Parse(k[6]);
                                odd.a = double.Parse(k[7]);
                                break;
                            case "3":
                                odd.o = double.Parse(k[6]);
                                odd.u = double.Parse(k[7]);
                                break;
                            case "7":
                                odd.h1 = double.Parse(k[6]);
                                odd.a1 = double.Parse(k[7]);
                                break;
                            case "9":
                                odd.o1 = double.Parse(k[6]);
                                odd.u1 = double.Parse(k[7]);
                                break;
                        }
                        odds.Add(odd);
                    }
                }
                res.Data = odds;
            }
            catch (Exception ex)
            {
                res.Errors.Add("Convert Sbobet Today Odds Errors: " + ex.Message);
                res.Data = odds;
            }
            return res;
        }

        public Response<List<MatchOdd>> EarlyOdds(string s)
        {
            Response<List<MatchOdd>> res = new Response<List<MatchOdd>>();
            List<Match> matchs = new List<Match>();
            List<MatchOdd> odds = new List<MatchOdd>();
            res.Data = odds;
            try
            {
                s = s.Replace("\\u200C", "");
                if (s.Length < 100) return res;
                var m = s.Replace("\\u200C", "").Split(new string[1] { "]],[[", }, StringSplitOptions.None);

                var m1 = m[1].Replace("[", "").Split(new string[1] { "],", }, StringSplitOptions.None);
                foreach (var item in m1)
                {
                    var k = item.Split(',');
                    Match match = new Match();
                    match.MatchId = int.Parse(k[0]);
                    match.ContestId = int.Parse(k[2]);
                    match.Home = k[3].Replace("'", "");
                    match.Away = k[4].Replace("'", "");
                    match.MatchOdds = new List<MatchOdd>();
                    matchs.Add(match);
                }

                var m0 = m[0].Substring(m[0].IndexOf("[[[") + 3, m[0].Length - (m[0].IndexOf("[[[") + 3)).Split(new string[1] { "],[", }, StringSplitOptions.None);
                foreach (var item in m0)
                {
                    var k = item.Split(',');
                    foreach (var match in matchs.Where(x => x.ContestId == int.Parse(k[0])))
                    {
                        match.Contest = k[1].Replace("'", "");
                    }
                }

                var m2 = m[2].Replace("[", "").Split(new string[1] { "]],,," }, StringSplitOptions.None);
                var m20 = m2[0].Split(new string[1] { "]," }, StringSplitOptions.None);
                foreach (var item in m20)
                {
                    var k = item.Split(',');
                    Match match = matchs.SingleOrDefault(x => x.MatchId == int.Parse(k[1]));
                    if (match != null) match.MatchNumber = int.Parse(k[0]);
                }

                var m3 = m2[1].Split(new string[1] { "]]," }, StringSplitOptions.None);
                foreach (var item in m3)
                {
                    var k = item.Replace("[", "").Replace("]", "").Split(',');
                    Match match = matchs.SingleOrDefault(x => x.MatchNumber == int.Parse(k[1]));
                    if (match != null)
                    {
                        MatchOdd odd;
                        odd = new MatchOdd();
                        odd.Match = match;
                        odd.MatchId = match.MatchId;
                        odd.OddId = int.Parse(k[0]);
                        odd.Goal = double.Parse(k[5]);
                        odd.OddType = int.Parse(k[2]);
                        odd.OddTypeDisplay = odd.OddType == 1 ? "H" : odd.OddType == 3 ? "H" : odd.OddType == 7 ? "H1" : odd.OddType == 9 ? "H1" : "";
                        odd.HdpType = 0; //Early
                        switch (k[2])
                        {
                            case "1":
                                odd.h = double.Parse(k[6]);
                                odd.a = double.Parse(k[7]);
                                break;
                            case "3":
                                odd.o = double.Parse(k[6]);
                                odd.u = double.Parse(k[7]);
                                break;
                            case "7":
                                odd.h1 = double.Parse(k[6]);
                                odd.a1 = double.Parse(k[7]);
                                break;
                            case "9":
                                odd.o1 = double.Parse(k[6]);
                                odd.u1 = double.Parse(k[7]);
                                break;
                        }
                        odds.Add(odd);
                    }
                }
                res.Data = odds;
            }
            catch (Exception ex)
            {
                res.Errors.Add("Convert Sbobet Today Odds Errors: " + ex.Message);
                res.Data = odds;
            }
            return res;
        }
    }
}
