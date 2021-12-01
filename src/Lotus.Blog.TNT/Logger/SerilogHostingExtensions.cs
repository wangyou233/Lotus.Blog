using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotus.Blog.TNT.Logger
{
    public static class SerilogHostingExtensions
    {
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
        public static string GetLogFile(this IConfiguration config, string defaultValue)
        {
            return config.GetValue<string>("LogFile", defaultValue);
        }
    }
}
