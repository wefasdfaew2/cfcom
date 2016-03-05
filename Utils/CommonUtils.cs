using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Utils
{
    public static class CommonUtils
    {
        public static double ConvertOddToHK(double odd)
        {
            if (odd >= 0) return odd;
            else return 2 + odd;
        }
        public static int RevertType(int bettype)
        {
            if (bettype == 0) return 1;
            else return 0;
        }

        public static string RevertBetResult(string result)
        {
            if (result.ToLower() == "win") return "Lose";
            else return "Win";
        }

        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector)
            {
                var knownKeys = new HashSet<TKey>();
                return source.Where(element => knownKeys.Add(keySelector(element)));
            }
    }
}
