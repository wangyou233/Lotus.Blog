﻿using Lotus.Blog.TNT.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.Domain.Shared.TermNode;
using NETCore.Encrypt;
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

            modelBuilder.Entity<TermNode>()
                .Property(p => p.Type)
                .HasConversion<string>();

            modelBuilder.Entity<TermNode>()
                .HasIndex(p => new {p.Code, p.Type});

            modelBuilder.Entity<TermNode>()
                .HasMany(p => p.Children)
                .WithOne(p => p.Parent)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TermNode>().HasData(new TermNode()
            {
                Id = 1, Type = TermNodeType.CreateSiteTime, Code = DateTime.Now.ToString(), Description = "",
                ExtData = "{}", Name = "", ParentId = null
            });
            modelBuilder.Entity<Admin>().HasData(new Admin()
            {
                Id = 1,
                UserName = "admin",
                NiceName = "admin",
                AvatarUrl = "",
                CustomDescription = "",
                Password = EncryptProvider.Md5("admin")
            });
            modelBuilder.Entity<Menu>().HasData(new List<Menu>()
            {
                new Menu()
                {
                    Id = 1,
                    Sort = 1,
                    Title = "首页",
                    Path = "/",
                    IsBlank = false,
                },
                new Menu()
                {
                    Id = 2,
                    Sort = 2,
                    Title = "文章归档",
                    Path = "/archives",
                    IsBlank = false,
                },
                new Menu()
                {
                    Id = 3,
                    Sort = 3,
                    Title = "日志",
                    Path = "/journal",
                    IsBlank = false,
                },
                new Menu()
                {
                    Id = 4,
                    Sort = 4,
                    Title = "关于",
                    Path = "/view/about",
                    IsBlank = false,
                },
            });
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