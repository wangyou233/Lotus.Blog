using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotus.Blog.TNT.Data.Context
{
    public class DbOptions : IDbOptions
    {
        public string ConnectionString { get; set; }

        public bool Readonly { get; set; }

    }
}
