using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotus.Blog.TNT.Ext
{
    public  static class EnumExtensions
    {
        /// <summary>
        /// 枚举转字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eff"></param>
        /// <returns></returns>
        public static string EnumToString<T>(this T eff)
        {
            return Enum.GetName(typeof(T), eff);
        }
        /// <summary>
        /// 字符串转枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>

        public static T StringToEnum<T>(this string str)
        {
            return (T)Enum.Parse(typeof(T), str, true);
        }
    }
}
