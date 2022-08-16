using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotus.Blog.TNT.Logger
{
    public static class SerilogHostingExtensions
    {
        /// <summary>
        /// 加载Serilog配置文件
        /// </summary>
        /// <returns></returns>
        public static IConfiguration AddDefaultConfiguration()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
        }
        /// <summary>
        /// 使用Serilog日志记录 以文件的方式记录
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <returns></returns>
        public static IWebHostBuilder UseSerilogDefault(this IWebHostBuilder hostBuilder)
        {
            
            var config = AddDefaultConfiguration();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .WriteTo.Console()
                .WriteTo.File(config.GetLogFile("logs/.log"), rollingInterval: RollingInterval.Day)
                .CreateLogger();
            hostBuilder.UseSerilog();

            return hostBuilder;

        }
        /// <summary>
        /// 获取log文件
        /// </summary>
        /// <param name="config"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetLogFile(this IConfiguration config, string defaultValue)
        {
            return config.GetValue<string>("LogFile", defaultValue);
        }
    }
}
