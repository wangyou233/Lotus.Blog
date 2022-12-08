using Lotus.Blog.Application.Impl;
using Lotus.Blog.TNT.Files;
using Microsoft.Extensions.DependencyInjection;

namespace Lotus.Blog.Application;

public static class Extensions
{

    public static IServiceCollection AddInitService(this IServiceCollection services)
    {

        services.AddScoped<InitService>();
        services.AddSingleton<IFileStorage, LocalFileStorage>();
        return services;
    }
}