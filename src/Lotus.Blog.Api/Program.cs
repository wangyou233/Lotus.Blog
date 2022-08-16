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

//ע��Swagger
builder.Services.AddSwaggerUI();
//����ȫ�ִ���
builder.Services.AddGlobalException();
//ע����־
builder.WebHost.UseSerilogDefault();
//��������
builder.WebHost.UseDefaultAgileConfig();
//ע��autoMapper
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(AdminProfile)));


//����ע��
builder.Services.AddScoped<IHttpContextAccessor, HttpContextAccessor>()
    .AddTransient<IActionContextAccessor, ActionContextAccessor>().AddSingleton(builder.Configuration);
//AutoFac ע��
builder.Host.AddService();
//Ipע��
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

//ע��һ��������ݿ�
DbConfig config = builder.Configuration.GetSection("Database").Get<DbConfig>();
builder.Services.AddAppDbContext<AppMasterDbContext, AppSlaveDbContext, AppDbRepository>(config);

//ע��Jwt��Ȩ
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
//ʹ��Swagger
app.UseSwaggerUI();
//�Զ�Ǩ�����ݿ�

//app.Services.MigrateMarketingDatabase();


app.UseHttpsRedirection();
// 跨域处理
app.UseCors(options =>
{

    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowAnyOrigin();
});
//��¼ȫ������
app.UseMiddleware<GlobalMiddleware>();
//AutoFacȫ��
AutofacExtensions.Container = ((IApplicationBuilder)app).ApplicationServices.GetAutofacRoot();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
