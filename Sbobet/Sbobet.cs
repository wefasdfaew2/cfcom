using ModelObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Text;
using System.Web;
using System.IO;
using App.Model;
using log4net;

namespace SbobetLib
{
    public class Sbobet
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Sbobet));
        public Response<bool> Login(Account account)
        {
            var res = new Response<bool>();
            try
            {
                string url = ""; 
                string surl = ""; 
                string baseUrl = "";

                var str = account.HostUrl.GetJsonFromUrl(
                    requestFilter: getRequest =>
                    {
                        getRequest.CookieContainer = new CookieContainer();
                        getRequest.CookieContainer.Add(account.Cookie);
                        getRequest.AllowAutoRedirect = true;
                        getRequest.Headers.Add("X-Forwarded-For", account.FakeIp);
                    },
                    responseFilter: response =>
                    {
                        account.Cookie.Add(response.Cookies);
                    });

                if (string.IsNullOrEmpty(str))
                    throw new NullReferenceException("Get state value is null");

                var scr = str.Substring(str.IndexOf("tilib_Token("), 500);
                scr = scr.Substring(scr.IndexOf("],['") + 4, scr.IndexOf("]))") - scr.IndexOf("],['") - 4);
                var t = scr.Split(',');
                var lang = t[3];
                var seo = t[5];

                scr = str.Substring(str.IndexOf("'od',new tilib_Token("), 500);
                scr = scr.Substring(scr.IndexOf("],[") + 3, scr.IndexOf("]))") - scr.IndexOf("],[") - 3);
                var token = scr.Replace("'", "");
                var s = token.Split(',');
                var tk = String.Join(",", s);

                var postData =
                    string.Format(
                        "id={0}&password={1}&lang={2}&tk={3}&5={4}&type={5}&tzDiff={6}",
                        account.UserName,
                        account.Password,
                        HttpUtility.UrlEncode("en"),
                        HttpUtility.UrlEncode(tk),
                        1,
                        "form",
                        1);

                var byteArray = Encoding.ASCII.GetBytes(postData);

                string.Format("{0}/web/public/process-sign-in.aspx", account.HostUrl).PostBytesToUrl(byteArray,
                    requestFilter: getRequest =>
                    {
                        getRequest.CookieContainer = new CookieContainer();
                        getRequest.CookieContainer.Add(account.Cookie);
                        getRequest.AllowAutoRedirect = false;
                        getRequest.ContentType = "application/x-www-form-urlencoded";
                        getRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                        getRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.116 Safari/537.36";
                        getRequest.Headers.Add("Accept-Language", "vi,en-US;q=0.8,en;q=0.6");
                        getRequest.Referer = account.HostUrl;
                        getRequest.Headers.Add("X-Forwarded-For", account.FakeIp);
                    },
                    responseFilter: response =>
                    {
                        account.Cookie.Add(response.Cookies);
                        url = response.Headers["Location"];
                        baseUrl = url.Substring(0, url.IndexOf("/welcome.aspx"));
                    });

                surl = url.GetJsonFromUrl(
                    requestFilter: getRequest =>
                    {
                        getRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                        getRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.116 Safari/537.36";
                        getRequest.Headers.Add("Accept-Language", "vi,en-US;q=0.8,en;q=0.6");
                        getRequest.Referer = account.HostUrl;
                        getRequest.CookieContainer = new CookieContainer();
                        getRequest.CookieContainer.Add(account.Cookie);
                        getRequest.AllowAutoRedirect = false;
                        getRequest.Headers.Add("X-Forwarded-For", account.FakeIp);
                    },
                    responseFilter: response =>
                    {
                        account.Cookie.Add(response.Cookies);
                        url = response.Headers["Location"];
                    });

                url = (baseUrl + url);
                surl = url.GetJsonFromUrl(
                    requestFilter: getRequest =>
                    {
                        getRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                        getRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.116 Safari/537.36";
                        getRequest.Headers.Add("Accept-Language", "vi,en-US;q=0.8,en;q=0.6");
                        getRequest.Referer = account.HostUrl;
                        getRequest.CookieContainer = new CookieContainer();
                        getRequest.CookieContainer.Add(account.Cookie);
                        getRequest.AllowAutoRedirect = false;
                        getRequest.Headers.Add("X-Forwarded-For", account.FakeIp);
                    },
                    responseFilter: response =>
                    {
                        account.Cookie.Add(response.Cookies);
                        url = response.Headers["Location"];
                    });

                url = (baseUrl + url);
                account.HomeUrl = url;
                account.BaseUrl = baseUrl;
                account.LoginName = url.Substring(url.IndexOf("loginname=") + 10, url.IndexOf("&") - url.IndexOf("loginname=") - 10);

                surl = url.GetJsonFromUrl(
                    requestFilter: getRequest =>
                    {
                        getRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                        getRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.116 Safari/537.36";
                        getRequest.Headers.Add("Accept-Language", "vi,en-US;q=0.8,en;q=0.6");
                        getRequest.Referer = account.HostUrl;
                        getRequest.CookieContainer = new CookieContainer();
                        getRequest.CookieContainer.Add(account.Cookie);
                        getRequest.AllowAutoRedirect = false;
                        getRequest.Headers.Add("X-Forwarded-For", account.FakeIp);
                    },
                    responseFilter: response =>
                    {
                        account.Cookie.Add(response.Cookies);
                        url = response.Headers["Location"];
                    });

                res.Data = (account.Cookie.Count > 5 && account.BaseUrl != null) ? true : false;
                if (res.Data)
                {
                    res.Infos.Add("Login success!");
                    _log.InfoFormat("Login {0} success!",account.UserName);
                }
            }
            catch (System.Exception ex)
            {
                _log.ErrorFormat("Login {0} error: {1}",account.UserName, ex.Message);
                res.Errors.Add("Lỗi login: " + ex.Message);
            }
            return res;
        }
        public Response<List<MatchOdd>> GetOdds(Account account, MatchStatus oddStatus)
        {
            Response<List<MatchOdd>> res = new Response<List<MatchOdd>>();
            try
            {
                string url ="";
                if (oddStatus == MatchStatus.Running)
                {
                    url = string.Format("{0}/web-root/restricted/odds-display/today-data.aspx?od-param={1}&fi={2}&v={3}",
                            account.BaseUrl,
                            "1,1,1,1,1,2,2,2,0",
                            0,
                            0);
                }
                else if (oddStatus == MatchStatus.Today)
                {
                    url = string.Format("{0}/web-root/restricted/odds-display/today-data.aspx?od-param={1}&fi={2}&v={3}&dl={4}",
                            account.BaseUrl,
                            "1,1,1,1,1,2,2,2,0",
                            1,
                            0,
                            0);
                }
                else
                {
                    //http://602iy850872o.asia.com3456.com/web-root/restricted/odds-display/early-market-data.aspx?od-param=1,1,2,1,1,2,2,2,0&v=0
                    url = string.Format("{0}/web-root/restricted/odds-display/early-market-data.aspx?od-param={1}&v={2}",
                            account.BaseUrl,
                            "1,1,2,1,1,2,2,2,0",
                            0);
                }

                var str = url.GetStringFromUrl(
                        requestFilter: getRequest =>
                        {
                            getRequest.CookieContainer = new CookieContainer();
                            getRequest.CookieContainer.Add(account.Cookie);
                            getRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                            getRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:41.0) Gecko/20100101 Firefox/41.0";
                            getRequest.Headers.Add("Accept-Language", "vi-VN,vi;q=0.8,en-US;q=0.5,en;q=0.3");
                            getRequest.Referer = account.HomeUrl;
                            getRequest.Headers.Add("X-Forwarded-For", account.FakeIp);
                        },
                        responseFilter: response =>
                        {
                            account.Cookie.Add(response.Cookies);
                        });

                res = oddStatus == MatchStatus.Running ? new Str2Obj().RunningOdds(str) : oddStatus==MatchStatus.Today ? new Str2Obj().TodayOdds(str) : new Str2Obj().EarlyOdds(str);
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("GetOdds by {0} error: {1}",account.UserName, ex.Message);
                res.Data = new List<MatchOdd>();
                res.Errors.Add("Get Sbobet Odds Errors: " + ex.Message);
            }
            return res;
        }
        public Response<bool> GetUpdateOdds(Account account, MatchStatus oddStatus)
        {
            Response<bool> res = new Response<bool>();
            res.Data = false;
            try
            {
                string url = "";
                if (oddStatus == MatchStatus.Running)
                {
                    url = string.Format("{0}/web-root/restricted/odds-display/today-data.aspx?od-param={1}&fi={2}&v={3}",
                            account.BaseUrl,
                            "1,1,1,1,1,2,2,2,0",
                            0,
                            0);
                }
                else if (oddStatus == MatchStatus.Today)
                {
                    url = string.Format("{0}/web-root/restricted/odds-display/today-data.aspx?od-param={1}&fi={2}&v=",
                            account.BaseUrl,
                            "1,1,1,1,1,2,2,2,0",
                            0,
                            2077);
                }
                else
                {
                    url = string.Format("{0}/web-root/restricted/odds-display/early-market-data.aspx?od-param={1}&v={2}",
                            account.BaseUrl,
                            "1,1,2,1,1,2,2,2,0",
                            0);
                }

                var str = url.GetStringFromUrl(
                        requestFilter: getRequest =>
                        {
                            getRequest.CookieContainer = new CookieContainer();
                            getRequest.CookieContainer.Add(account.Cookie);
                            getRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                            getRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:41.0) Gecko/20100101 Firefox/41.0";
                            getRequest.Headers.Add("Accept-Language", "vi-VN,vi;q=0.8,en-US;q=0.5,en;q=0.3");
                            getRequest.Referer = account.HomeUrl;
                            getRequest.Headers.Add("X-Forwarded-For", account.FakeIp);
                        },
                        responseFilter: response =>
                        {
                            account.Cookie.Add(response.Cookies);
                        });

                res.Data = true;
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("Update Odds by {0} error: {1}", account.UserName, ex.Message);
                res.Errors.Add("Get update Sbobet Odds Errors: " + ex.Message);
            }
            return res;
        }
        public Response<List<MatchOdd>> GetRunningOdds(Account account)
        {
            return GetOdds(account, MatchStatus.Running);
        }
        public Response<List<MatchOdd>> GetTodayOdds(Account account)
        {
            return GetOdds(account, MatchStatus.Today);
        }
        public Response<List<MatchOdd>> GetEarlyOdds(Account account)
        {
            return GetOdds(account, MatchStatus.Early);
        }
        public Response<bool> Bet(Account account, SbobetTicket ticket)
        {
            Response<bool> res = new Response<bool>();
            res.Data = false;
            try
            {
                string turl = string.Format("{0}/web-root/restricted/ticket/ticket.aspx?loginname={1}&id={2}&op={3}&odds={4}&hdpType={5}&isor={6}&isLive={7}&betpage={8}&style={9}",
                    account.BaseUrl,
                    account.LoginName,
                    ticket.MatchOddId,
                    ticket.Op,
                    ticket.Odds,
                    ticket.HdpType, //a,h
                    ticket.IsOr,
                    ticket.IsLive,
                    ticket.BetPage,
                    ticket.Style);

                var str = turl.GetStringFromUrl(
                    requestFilter: getRequest =>
                    {
                        getRequest.CookieContainer = new CookieContainer();
                        getRequest.CookieContainer.Add(account.Cookie);
                        getRequest.Referer = account.HomeUrl;
                        getRequest.Accept = "*/*";
                        getRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.116 Safari/537.36";
                        getRequest.Headers.Add("Accept-Language", "vi-VN,vi;q=0.8,en-US;q=0.5,en;q=0.3");
                        getRequest.Headers.Add("X-Forwarded-For", account.FakeIp);
                    },
                    responseFilter: response =>
                    {
                        //account.Cookie.Add(response.Cookies);
                    });

                ticket.TimeDiff = 6780;
                string url = string.Format("{0}/web-root/restricted/ticket/confirm.aspx?loginname={1}&sameticket={2}&betcount={3}&stake={4}&ostyle={5}&stakeInAuto={6}&betpage={7}&acceptIfAny={8}&autoProcess={9}&autoRefresh={10}&oid={11}&timeDiff={12}",
                    account.BaseUrl,
                    account.LoginName,
                    ticket.SameTicket,
                    ticket.Betcount,
                    ticket.Stake,
                    ticket.Style,
                    ticket.StakeInAuto,
                    ticket.BetPage,
                    ticket.AcceptIfAny,
                    ticket.AutoProcess,
                    ticket.AutoRefresh,
                    ticket.MatchOddId,
                    ticket.TimeDiff);
                _log.InfoFormat("Bet success: {0} - {1} - {2} - {3}", account.UserName, ticket.Stake, ticket.Odds, ticket.Op == "h" ? "Over" : "Under");
                //str = url.GetStringFromUrl(
                //    requestFilter: getRequest =>
                //    {
                //        getRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                //        getRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:41.0) Gecko/20100101 Firefox/41.0";
                //        getRequest.Headers.Add("Accept-Language", "vi-VN,vi;q=0.8,en-US;q=0.5,en;q=0.3");
                //        getRequest.Referer = account.HomeUrl;
                //        getRequest.CookieContainer = new CookieContainer();
                //        getRequest.CookieContainer.Add(account.Cookie);
                //        getRequest.Headers.Add("X-Forwarded-For", account.FakeIp);
                //    },
                //    responseFilter: response =>
                //    {
                //        //account.Cookie.Add(response.Cookies);
                //    });

                //_log.InfoFormat("Bet result {0} - {1}", account.UserName, str);
                //if (str.Contains("onOrderSubmitted"))
                //{
                //    _log.InfoFormat("Bet success: {0} - {1} - {2} - {3}", account.UserName, ticket.Stake, ticket.Odds, ticket.Op == "h" ? "Over" : "Under");
                    res.Data = true;
                //}
            }
            catch (Exception ex)
            {
                _log.ErrorFormat("Bet by {0} error: {1}", account.UserName, ex.Message);
                res.Errors.Add("Bet Sbobet Errors: " + ex.Message);
            }
            return res;
        }

        public Response<bool> BetOverUnder(Account account, MatchOdd odd)
        {
            Response<bool> res = new Response<bool>();
            res.Data = false;
            if (odd == null || (odd!=null && odd.OddType != 3 && odd.OddType != 9)) return res;
            try
            {
                SbobetTicket ticket = new SbobetTicket();
                //Request
                ticket.MatchOddId = odd.OddId;
                ticket.HdpType =  odd.HdpType; //Live =2 , today=1
                ticket.Op = account.Choose==0? "h": "a"; //Over 
                if (account.Choose == 0)  
                {
                    ticket.Odds = odd.OddType == 3 ? odd.o : odd.o1; //Home , Over =h , Away, Under = a
                }
                else if (account.Choose == 1)
                {
                    ticket.Odds = odd.OddType == 3 ? odd.u : odd.u1; //Home , Over =h , Away, Under = a
                }
                ticket.IsLive = 0;
                ticket.IsOr = 0;
                ticket.BetPage = 18;//check
                ticket.Style = 1;

                //Confirm
                ticket.SameTicket = 0;
                ticket.Betcount = 0;
                ticket.Stake = account.Stake;//Maxbet
                ticket.StakeInAuto = account.Stake;
                ticket.AcceptIfAny = 1;
                ticket.AutoProcess = 0;
                ticket.AutoRefresh = 0;  //check                                
                ticket.TimeDiff = 6735;

                return Bet(account, ticket);
            }
            catch (Exception ex)
            {
                res.Errors.Add("Bet Sbobet Errors: " + ex.Message);
                return res;
            }
        }
    }
}
