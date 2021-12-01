using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Lotus.Blog.TNT.Data.Ext
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static string FormatString(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

        public static int? ToInt(this string str, int? def = null)
        {
            if (str.IsNullOrEmpty())
            {
                return def;
            }

            if (int.TryParse(str, out int number))
            {
                return number;
            }

            return def;
        }
        public static Decimal? ToDecimal(this string str, decimal? def = null)
        {
            if (str.IsNullOrEmpty())
            {
                return def;
            }

            if (decimal.TryParse(str, out decimal number))
            {
                return number;
            }

            return def;
        }
        public static float? ToFloat(this string str, float? def = null)
        {
            if (str.IsNullOrEmpty())
            {
                return def;
            }

            if (float.TryParse(str, out float number))
            {
                return number;
            }

            return def;
        }
        public static DateTime? ToDateTime(this string str, DateTime? def = null)
        {
            if (str.IsNullOrEmpty())
            {
                return def;
            }

            if (DateTime.TryParse(str, out DateTime datetime))
            {
                return datetime;
            }

            return def;
        }
        public static bool? ToBool(this string str, bool? def = null)
        {
            if (str.IsNullOrEmpty())
            {
                return def;
            }

            if (bool.TryParse(str, out bool b))
            {
                return b;
            }

            return def;
        }

        public static string UrlEncode(this string str)
        {
            return HttpUtility.UrlEncode(str, Encoding.UTF8);
        }
        public static string UrlEncode(this string str, Encoding encode)
        {
            return HttpUtility.UrlEncode(str, encode);
        }
        public static string ToMD5(this string str)
        {
            return str.ToMD5(Encoding.UTF8);
        }
        public static string ToMD5(this string str, Encoding encode)
        {
            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                byte[]
                    _byte = encode.GetBytes(str),
                    _byteRst = md5.ComputeHash(_byte);
                StringBuilder
                    builder = new StringBuilder();
                foreach (byte i in _byteRst)
                    builder.Append(i.ToString("X2"));
                return builder.ToString();
            }
        }
        public static string ToBase64(this string txt)
        {
            byte[] bytedata = Encoding.UTF8.GetBytes(txt);

            string base64 = Convert.ToBase64String(bytedata, 0, bytedata.Length);

            return base64;
        }
        public static T ParseJson<T>(this string input)
        {
            T obj = JsonConvert.DeserializeObject<T>(input);

            return obj;
        }
        public static bool IsMatch(this string text, string pattern)
        {
            return Regex.IsMatch(text, pattern);
        }
    }
}
