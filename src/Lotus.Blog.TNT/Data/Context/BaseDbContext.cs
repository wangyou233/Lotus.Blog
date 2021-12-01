using Lotus.Blog.TNT.Data.Ext;
using Microsoft.EntityFrameworkCore;

namespace Lotus.Blog.TNT.Data.Context
{
    public abstract class BaseDbContext : DbContext
    {
        public DbConfig Configuration { get; }
        protected BaseDbContext(DbContextOptions options, DbConfig config) : base(options)
        {
            Configuration = config;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //设置表前缀
            foreach( var entity in modelBuilder.Model.GetEntityTypes())
            {
                var entityType = entity.GetType();
                if (entityType == null)
                {
                    continue;
                }
                var baseType = entityType.BaseType;
                if (baseType == null || (baseType != null && baseType.IsAbstract))
                {
                    if (!Configuration.Prefix.IsNullOrEmpty())
                    {
                        var tableName = Configuration.Prefix + entity.GetTableName();
                        entity.SetTableName(tableName);
                    }

                }
            }
        }
    }
}
