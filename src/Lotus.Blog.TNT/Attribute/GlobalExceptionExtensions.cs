using Microsoft.Extensions.DependencyInjection;

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
