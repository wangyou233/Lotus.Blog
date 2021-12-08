using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotus.Blog.TNT.AgileConfig
{
    public static class AgileConfigExtensions
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
        public static IWebHostBuilder UseDefaultAgileConfig(this IWebHostBuilder hostBuilder)
        {
            AddDefaultConfiguration();
            hostBuilder.ConfigureAppConfiguration((context, config) =>
             {
                 config.AddAgileConfig((arg) =>
                 {
                     Console.WriteLine($"action:{arg.Action} key:{arg.Key}");
                 });
             });
            return hostBuilder;
        }
    }
}
