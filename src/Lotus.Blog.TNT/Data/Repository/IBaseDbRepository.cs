using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotus.Blog.TNT.Data.Repository
{
    /// <summary>
    /// 仓储
    /// </summary>
    public interface IBaseDbRepository
    {
        DbContext MasterDb { get; }
        DbContext SlaveDb { get; }
    }
}
