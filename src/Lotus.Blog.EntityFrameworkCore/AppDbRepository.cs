using Lotus.Blog.TNT.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotus.Blog.EntityFrameworkCore
{
    public class AppDbRepository : IBaseDbRepository
    {
        public AppDbRepository(AppMasterDbContext masterDbContext, AppSlaveDbContext slaveDbContext)
        {
            MasterDb = masterDbContext;
            SlaveDb = slaveDbContext;
        }

        public DbContext MasterDb { get; }

        public DbContext SlaveDb { get; }
    }
}
