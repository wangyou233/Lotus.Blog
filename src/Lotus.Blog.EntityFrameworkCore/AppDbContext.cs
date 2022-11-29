using Lotus.Blog.TNT.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.Domain.Shared.TermNode;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Lotus.Blog.EntityFrameworkCore
{
    public abstract class AppDbContext<TContext> : BaseDbContext where TContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<UploadFile> UploadFiles { get; set; }


        public DbSet<Tag> Tags { get; set; }

        public DbSet<PostTag> PostTags { get; set; }

        public DbSet<SystemLog> SystemLogs { get; set; }

        public DbSet<Link> Links { get; set; }


        public DbSet<Admin> Admins { get; set; }

        public DbSet<TermNode> TermNodes { get; set; }

        public DbSet<Log> Logs { get; set; }

        public DbSet<CustomView> CustomViews { get; set; }

        public AppDbContext(DbContextOptions options, DbConfig config) : base(options, config)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TermNode>().HasData(new TermNode(){Type = TermNodeType.CreateSiteTime,Code = DateTime.Now.ToString()});
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