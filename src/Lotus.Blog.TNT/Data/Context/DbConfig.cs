using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotus.Blog.TNT.Data.Context
{
    public class DbConfig: IDbConfig
    {
        public string Master { get; set; }  

        public string[] Slave { get; set; }

        public string Prefix { get; set; }

        public string Schema { get; set; }

        public bool GenerateTestData { get; set; } = false;

        /// <summary>
        /// 随机取一个从库连接
        /// </summary>
        /// <returns></returns>
        public string NextSlave()
        {
            if (Slave == null || Slave.Length == 0)
            {
                return Master;
            }

            if (Slave.Length == 1)
                return Slave[0];

            int i = DateTime.Now.Millisecond % Slave.Length;

            return Slave[i];
        }
    }


}
