using Lotus.Blog.TNT.Data.Context;
using Lotus.Blog.TNT.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotus.Blog.TNT.Data
{
    public static class DatabaseExtensions
    {
        public static void AddAppDbContext<TMasterDbContext, TSlaveDbContext, TDbRepository>(
            this IServiceCollection services, DbConfig dbConfig, string efTableName = "__EFMH")
            where TDbRepository : class, IBaseDbRepository
            where TMasterDbContext : DbContext
            where TSlaveDbContext : DbContext
        {
            services.AddScoped<IBaseDbRepository, TDbRepository>();

            services.AddSingleton(dbConfig);

            //数据上下文（单库，主从，多主多从）
            //注册主库
            services
                .AddEntityFrameworkMySql()
                .AddDbContext<TMasterDbContext>(options =>
                {
                    //多个主库的情况下，每个scope请求一个主库上下文实例，在此选择一个数据库源，实现多主数据库
                    //可以根据一定的规则来实现（比如一个会话或客户端访问同一个源），这里是随机取一个
                    var connectionString = dbConfig.Master;
                    var serverVersion = new MySqlServerVersion(new Version(8,0,0));

                    options.UseMySql(connectionString, serverVersion, x => x.MigrationsHistoryTable(dbConfig.Prefix + efTableName))
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
                    

                    ;
                });

            //注册从库
            services
                .AddEntityFrameworkMySql()
                .AddDbContext<TSlaveDbContext>(options =>
                {

                    //多个从库的情况下，每个scope请求一个从库上下文实例，在此选择一个数据库源，实现多从数据库
                    //可以根据一定的规则来实现（比如一个会话或客户端访问同一个源），这里是随机取一个
                    var connectionString = dbConfig.NextSlave();
                    var serverVersion = new MySqlServerVersion(new Version(8, 0, 0));

                    options.UseMySql(connectionString, serverVersion, x => x.MigrationsHistoryTable(dbConfig.Prefix + efTableName))
                     .LogTo(Console.WriteLine, LogLevel.Information)
                     .EnableSensitiveDataLogging()
                     .EnableDetailedErrors();
                });

        }
    }
}
