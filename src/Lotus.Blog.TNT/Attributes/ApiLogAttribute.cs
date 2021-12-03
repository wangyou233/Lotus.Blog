
using System;
using System.Collections.Concurrent;
using System.Linq;
using Autofac;
using Lotus.Blog.TNT.Autofac;
using Lotus.Blog.TNT.Data.Ext;
using Lotus.Blog.TNT.Ext;
using Lotus.Blog.TNT.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Lotus.Blog.TNT.Attribute
{
    public class ApiLogAttribute: System.Attribute, IActionFilter
    {
        static ConcurrentDictionary<HttpContext, DateTime> _requesTime { get; }
            = new ConcurrentDictionary<HttpContext, DateTime>();
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _requesTime[HttpContextCore.Current] = DateTime.Now;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var time = DateTime.Now - _requesTime[HttpContextCore.Current];
            _requesTime.TryRemove(HttpContextCore.Current, out _);
            var request = context.HttpContext.Request;
            string resContent = string.Empty;
            if (context.Result is ContentResult result)
                resContent = result.Content;
            
            if (resContent?.Length > 200)
            {
                resContent = new string(resContent.Copy(0, 200).ToArray());
                resContent += "......";
            }
            var Body = HttpContextCore.Current.Request.Body.ReadToString();
            if (Body.Length > 500)
            {
                Body = new string(Body.Copy(0, 500).ToArray());
                Body += "......";
            }
            string log =
                $@"方向:请求本系统
                ip:{context.HttpContext?.Connection?.RemoteIpAddress?.MapToIPv4().ToString() ?? ""}
                url:{request.GetDisplayUrl()}
                method:{request.Method}
                contentType:{request.ContentType}
                body:{Body}
                耗时:{(int)time.TotalMilliseconds}ms
                返回:{resContent}";
            using (var lifescope = AutofacExtensions.Container.BeginLifetimeScope())
            {
                lifescope.Resolve<ILogger<ApiLogAttribute>>().Log(LogLevel.Information,log);
            }
        }
    }
}