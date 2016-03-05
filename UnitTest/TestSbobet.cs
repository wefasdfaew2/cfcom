using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelObjects;
using App.Model;
using System.Collections.Generic;
using SbobetLib;

namespace UnitTest
{
    [TestClass]
    public class TestSbobet
    {
        Account acc = new Account();
        public void Init(){
            acc.HostUrl = "http://www.com3456.com";
            acc.UserName = "mqvzzla001";
            acc.Password = "Qqqq1111";
            acc.FakeIp = "171.99.128.30";
            acc.Cookie = new System.Net.CookieCollection();
        }

        [TestMethod]
        public void TestLogin()
        {
            Init();
            var result = new Sbobet().Login( acc);
            Assert.AreEqual(true, result.Data, "Login success!");
        }

        [TestMethod]
        public void TestGetRunningOdds()
        {
            Init();
            var result = new Sbobet().Login( acc);
            if (result.Data)
            {
                var oddResult = new Sbobet().GetRunningOdds(acc);
                Assert.AreEqual(false, oddResult.HasError, "Get running odds success!");
            }
        }

        [TestMethod]
        public void TestGetTodayOdds()
        {
            Init();
            var result = new Sbobet().Login(acc);
            if (result.Data)
            {
                var oddResult = new Sbobet().GetTodayOdds(acc);
                Assert.AreEqual(false, oddResult.HasError, "Get today odds success!");
            }
        }

        [TestMethod]
        public void TestBetToDay()
        {
            Init();
            var result = new Sbobet().Login(acc);
            if (result.Data)
            {
                var oddResult = new Sbobet().GetTodayOdds(acc);
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
                    ticket.Op = "h";
                    ticket.HdpType = 1;
                    ticket.Odds = odd.h;
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

                    var betresult = new Sbobet().Bet(acc,ticket);
                    Assert.AreEqual(true, betresult.Data, "Bet running success!");
                }                
            }
        }
    }
}
