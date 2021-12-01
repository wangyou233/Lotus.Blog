using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotus.Blog.TNT.Data.Context
{
    public interface IDbOptions
    {
        /// <summary>
        /// 链接字符串
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool Readonly { get; set; }
    }
}
