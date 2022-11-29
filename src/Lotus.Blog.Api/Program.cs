using System.Reflection;
using Autofac.Extensions.DependencyInjection;
using Lotus.Blog.Api;
using Lotus.Blog.Application.Profiles;
using Lotus.Blog.EntityFrameworkCore;
using Lotus.Blog.TNT.AgileConfig;
using Lotus.Blog.TNT.Attributes;
using Lotus.Blog.TNT.Data;
using Lotus.Blog.TNT.Data.Context;
using Lotus.Blog.TNT.Logger;
using Lotus.Blog.TNT.Swagger;
using Lotus.Blog.TNT.Middleware;
using Lotus.Blog.TNT.Autofac;
using Lotus.Blog.TNT.Jwt;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using SkyApm.Utilities.Configuration;
using SkyApm.Utilities.DependencyInjection;
using SkyApm.Utilities.Logging;

var builder = WebApplication.CreateBuilder(args);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerUI();
builder.Services.AddGlobalException();
builder.WebHost.UseSerilogDefault();
// builder.WebHost.UseDefaultAgileConfig();
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(AdminProfile)));


builder.Services.AddScoped<IHttpContextAccessor, HttpContextAccessor>()
    .AddTransient<IActionContextAccessor, ActionContextAccessor>().AddSingleton(builder.Configuration);
builder.Host.AddService();
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});
// builder.Services.AddSkyApmExtensions();
// builder.Services.AddSkyAPM();

DbConfig config = builder.Configuration.GetSection("Database").Get<DbConfig>();
builder.Services.AddAppDbContext<AppMasterDbContext, AppSlaveDbContext, AppDbRepository>(config);

JwtConfig jwtConfig = builder.Configuration.GetSection("JwtConfig").Get<JwtConfig>();
builder.Services.AddJwtAuthentication(jwtConfig);

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd hh-mm-ss";
    options.SerializerSettings.Converters.Add(new StringEnumConverter());
});

var app = builder.Build();

IApplicationBuilder applicationBuilder = app;
applicationBuilder.Use(next => context =>
{
    context.Request.EnableBuffering();

    return next(context);
});
app.UseSwaggerUI();
app.Services.MigrateMarketingDatabase();


app.UseHttpsRedirection();
// 跨域处理
app.UseCors(options =>
{

    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowAnyOrigin();
});
app.UseMiddleware<GlobalMiddleware>();
AutofacExtensions.Container = ((IApplicationBuilder)app).ApplicationServices.GetAutofacRoot();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
