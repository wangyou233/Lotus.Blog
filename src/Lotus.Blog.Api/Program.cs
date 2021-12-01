using System.Reflection;
using Lotus.Blog.Application.Profiles;
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
using Lotus.Blog.TNT.Autofac;
using Lotus.Blog.TNT.Jwt;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//ע��Swagger
builder.Services.AddSwaggerUI();
//���ȫ�ִ���
builder.Services.AddGlobalException();
//ע����־
builder.WebHost.UseSerilogDefault();
//��������
builder.WebHost.UseDefaultAgileConfig();
//ע��autoMapper
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(AdminProfile)));

//AutoFacע�����
AutofacExtensions.AddService();

//ע��һ��������ݿ�
DbConfig config = builder.Configuration.GetSection("Database").Get<DbConfig>();
builder.Services.AddAppDbContext<AppMasterDbContext, AppSlaveDbContext, AppDbRepository>(config);

//ע��Jwt��Ȩ
JwtConfig jwtConfig = builder.Configuration.GetSection("jwtconfig").Get<JwtConfig>();
builder.Services.AddJwtAuthentication(jwtConfig);


var app = builder.Build();



// Configure the HTTP request pipeline.

//ʹ��Swagger
app.UseSwaggerUI();
app.UseHttpsRedirection();

//��¼ȫ������
app.UseMiddleware<GlobalMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
