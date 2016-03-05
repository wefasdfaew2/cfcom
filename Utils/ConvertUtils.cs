using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class ConvertUtils
    {
        public static int ToInt(string value)
        {
            int r = 0;
            int.TryParse(value, out r);
            return r;
        }

        public static double ToDouble(string value)
        {
            double r = 0;
            double.TryParse(value, out r);
            return r;
        }

        public static decimal ToDecimal(string value)
        {
            decimal r = 0;
            decimal.TryParse(value, out r);
            return r;
        }

        public static string ToMoneyText(double value)
        {
            if (value==0) return "0";
            return String.Format(CultureInfo.InvariantCulture,
                                 "{0:0,0}", value);
        }

        public static string ToRateText(double? value)
        {
            if (!value.HasValue) return "";
            else return Math.Round(value.Value, 2).ToString();
        }
    }
}
