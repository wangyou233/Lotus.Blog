using Lotus.Blog.TNT.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Lotus.Blog.Domain.Entities;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Lotus.Blog.EntityFrameworkCore
{
    public abstract class AppDbContext<TContext> : BaseDbContext where TContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<PostComment> PostComments { get; set; }

        public DbSet<Domain.Entities.File> Files { get; set; }
        

        public DbSet<Tag> Tags { get; set; }

        public DbSet<PostTag> PostTags { get; set; }

        public DbSet<SystemLog> SystemLogs { get; set; }

        public DbSet<Link> Links { get; set; }


        public DbSet<Admin> Admins { get; set; }
        
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
