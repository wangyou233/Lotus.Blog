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

//ע��Swagger
builder.Services.AddSwaggerUI();
//����ȫ�ִ���
builder.Services.AddGlobalException();
//ע����־
builder.WebHost.UseSerilogDefault();
//��������  ע��
// builder.WebHost.UseDefaultAgileConfig();
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
// builder.Services.AddSkyApmExtensions();
// builder.Services.AddSkyAPM();

//ע��һ��һ�����ݿ�
DbConfig config = builder.Configuration.GetSection("Database").Get<DbConfig>();
builder.Services.AddAppDbContext<AppMasterDbContext, AppSlaveDbContext, AppDbRepository>(config);

//ע��Jwt��Ȩ
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
//ʹ��Swagger
app.UseSwaggerUI();
//�Զ�Ǩ�����ݿ�
app.Services.MigrateMarketingDatabase();


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
