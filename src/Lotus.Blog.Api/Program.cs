using System.Reflection;
using Autofac.Extensions.DependencyInjection;
using Lotus.Blog.Application.Profiles;
using Lotus.Blog.EntityFrameworkCore;
using Lotus.Blog.TNT.AgileConfig;
using Lotus.Blog.TNT.Attribute;
using Lotus.Blog.TNT.Attributes;
using Lotus.Blog.TNT.Data;
using Lotus.Blog.TNT.Data.Context;
using Lotus.Blog.TNT.Logger;
using Lotus.Blog.TNT.Swagger;
using Lotus.Blog.TNT.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Lotus.Blog.TNT.Autofac;
using Lotus.Blog.TNT.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using Serilog;
using Serilog.Core;
using Lotus.Blog.Api;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//注入Swagger
builder.Services.AddSwaggerUI();
//添加全局错误
builder.Services.AddGlobalException();
//注入日志
builder.WebHost.UseSerilogDefault();
//配置中心
builder.WebHost.UseDefaultAgileConfig();
//注入autoMapper
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(AdminProfile)));


//请求注册
builder.Services.AddScoped<IHttpContextAccessor, HttpContextAccessor>()
    .AddTransient<IActionContextAccessor, ActionContextAccessor>().AddSingleton(builder.Configuration);
//AutoFac 注入
builder.Host.AddService();
//Ip注入
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

//注册一主多从数据库
DbConfig config = builder.Configuration.GetSection("Database").Get<DbConfig>();
builder.Services.AddAppDbContext<AppMasterDbContext, AppSlaveDbContext, AppDbRepository>(config);

//注册Jwt鉴权
JwtConfig jwtConfig = builder.Configuration.GetSection("jwtconfig").Get<JwtConfig>();
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
// Configure the HTTP request pipeline.
//使用Swagger
app.UseSwaggerUI();
//自动迁移数据库

//app.Services.MigrateMarketingDatabase();


app.UseHttpsRedirection();
app.UseCors(options =>
{

    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowAnyOrigin();
});
//记录全局请求
app.UseMiddleware<GlobalMiddleware>();
//AutoFac全局
AutofacExtensions.Container = ((IApplicationBuilder)app).ApplicationServices.GetAutofacRoot();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
