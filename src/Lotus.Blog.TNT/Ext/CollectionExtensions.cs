using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotus.Blog.TNT.Ext
{
    public static class CollectionExtensions
    {
        public static string Join<T>(this IEnumerable<T> values, string separator)
        {
            var s = string.Join<T>(separator, values);

            return s;
        }
    }
}
