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

builder.Services.AddSwagger();

//���ȫ�ִ���
builder.Services.AddGlobalException();
//ע����־
builder.WebHost.UseSerilogDefault();
//��������
builder.WebHost.UseDefaultAgileConfig();

//ע�����
AutofacExtensions.AddService();

//���ݿ�
DbConfig config = builder.Configuration.GetSection("Database").Get<DbConfig>();
builder.Services.AddAppDbContext<AppMasterDbContext, AppSlaveDbContext, AppDbRepository>(config,
                "__EFMigrationsHistory"); ;


var app = builder.Build();



// Configure the HTTP request pipeline.

app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseSerilogRequestLogging(options =>
{
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) => {
        //This didn't work when tested
    };
});
app.UseMiddleware<GlobalMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
