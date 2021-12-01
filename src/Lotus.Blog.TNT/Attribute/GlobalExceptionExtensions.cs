using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotus.Blog.TNT.Attribute
{
    public static class GlobalExceptionExtensions
    {
        public static void AddGlobalException(this IServiceCollection services)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (environmentName == "Prodction")
            {
                services.AddControllers(option =>
                {
                    option.Filters.Add<GlobalExceptionFilter>();
                });
                
            }
            else
            {
                services.AddControllers();
            }
           
        }
    }
}
