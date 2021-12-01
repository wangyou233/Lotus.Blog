using Autofac;
using Lotus.Blog.EntityFrameworkCore;
using Lotus.Blog.TNT.AgileConfig;
using Lotus.Blog.TNT.Attribute;
using Lotus.Blog.TNT.Data;
using Lotus.Blog.TNT.Data.Context;
using Lotus.Blog.TNT.Logger;
using Lotus.Blog.TNT.Swagger;
using Lotus.Blog.TNT.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Context;
using Serilog.Events;
using System.Configuration;
using Lotus.Blog.TNT.Autofac;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
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

//AutoFac注册服务
AutofacExtensions.AddService();

//注册一主多从数据库
DbConfig config = builder.Configuration.GetSection("Database").Get<DbConfig>();
builder.Services.AddAppDbContext<AppMasterDbContext, AppSlaveDbContext, AppDbRepository>(config);


var app = builder.Build();



// Configure the HTTP request pipeline.

//使用Swagger
app.UseSwaggerUI();
app.UseHttpsRedirection();

//记录全局请求
app.UseMiddleware<GlobalMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
