using Lotus.Blog.TNT.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotus.Blog.EntityFrameworkCore
{
    public abstract class AppDbContext<TContext> : BaseDbContext where TContext : DbContext
    {
        public AppDbContext(DbContextOptions options, DbConfig config) : base(options, config)
        {
        }
    }
    public class AppMasterDbContext : AppDbContext<AppMasterDbContext>
    {
        public AppMasterDbContext(DbContextOptions<AppMasterDbContext> options, DbConfig config) : base(options, config)
        {
        }
    }

    public class AppSlaveDbContext : AppDbContext<AppSlaveDbContext>
    {
        public AppSlaveDbContext(DbContextOptions<AppSlaveDbContext> options, DbConfig config) : base(options, config)
        {
        }
    }
}
