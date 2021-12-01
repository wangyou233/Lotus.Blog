using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotus.Blog.TNT.Data.Context
{
    public interface IDbConfig
    {
        /// <summary>
        /// 主库链接字符串
        /// </summary>
        public string Master { get; set; }

        /// <summary>
        /// 从库链接字符串
        /// </summary>
        string[] Slave { get; set; }

        /// <summary>
        /// 表前缀
        /// </summary>
        string Prefix { get; set; }


        /// <summary>
        /// 是否插入默认数据
        /// </summary>
        bool GenerateTestData { get; set; }

        /// <summary>
        /// 随机从库
        /// </summary>
        /// <returns></returns>
        string NextSlave();
    }
}
