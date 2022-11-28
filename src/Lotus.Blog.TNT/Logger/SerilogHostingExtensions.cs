using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.Grafana.Loki;

namespace Lotus.Blog.TNT.Logger
{
    public static class SerilogHostingExtensions
    {

        /// <summary>
        /// 默认配置
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <returns></returns>
        public static IWebHostBuilder UseSerilogDefault(this IWebHostBuilder hostBuilder)
        {

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables()
                .Build();
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Enrich.WithMachineName()
                .WriteTo.Debug()
                .WriteTo.File(configuration.GetLogFile("logs/blog-.log"),rollingInterval: RollingInterval.Day)
                .WriteTo.Console()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            
            hostBuilder.UseSerilog();

            return hostBuilder;

        }

        public static string GetLogFile(this IConfiguration config, string defaultValue)
        {
            return config.GetValue<string>("LogFile", defaultValue);
        }
    }
}
