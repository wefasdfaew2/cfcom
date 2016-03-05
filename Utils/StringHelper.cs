using HtmlAgilityPack;
using System;
namespace Utils
{
    public static class StringHelper
    {
        private const string VIEW_STATE = "id=\"__VIEWSTATE\" value=\"";
        private const string EVENT_STATE_GEN = "id=\"__VIEWSTATEGENERATOR\" value=\"";
        private const string EVENT_VALIDATION_FLAG = "id=\"__EVENTVALIDATION\" value=\"";
        private const string RequestVerificationToken = "name=\"__RequestVerificationToken\" type=\"hidden\" value=\"";
        private const string DataForm_L = "DataForm_L";

        public static string GetViewState(this string str)
        {
            return ParseString(str, VIEW_STATE);
        }

        public static string GetEventValidation(this string str)
        {
            return ParseString(str, EVENT_VALIDATION_FLAG);   
        }

        public static string GetViewStateGenerator(this string str)
        {            
            return ParseString(str, EVENT_STATE_GEN);
        }

        public static string GetRequestVerificationToken(this string str)
        {
            return ParseString(str, RequestVerificationToken);
        }

        public static string ParseString(this string str, string flag)
        {
            var i = str.IndexOf(flag) + flag.Length;
            var j = str.IndexOf("\"", i);

            return str.Substring(i, j - i);
        }

        public static string GetValueElementByName(string html, string name)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            var item = document.DocumentNode.SelectSingleNode("//input[@name='" + name + "']");
            return item.Attributes["value"].Value;
        }

        public static string GetValueElementLikeName(string html, string name)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            foreach (var item in document.DocumentNode.SelectNodes("//input[@type='hidden']"))
            {
                if (item.Attributes["name"].Value.StartsWith(name))
                    return item.Attributes["name"].Value + "-" + item.Attributes["value"].Value;
            }
            return null;
        }        
    }
}