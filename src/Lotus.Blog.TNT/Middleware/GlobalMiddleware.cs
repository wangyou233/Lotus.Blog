using Lotus.Blog.TNT.Ext;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotus.Blog.TNT.Middleware
{
    public class GlobalMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalMiddleware> _logger;

        public GlobalMiddleware(RequestDelegate next, ILogger<GlobalMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(context.Request.Method == "GET" || context.Request.Method == "DELETE")
            {
                var obj = new
                {
                    method = context.Request.Method,
                    url = $"{context.Request.Host}{context.Request.Path}"
                };
                _logger.LogInformation(obj.ToJson());
            }
            else
            {
               
                var obj = new
                {
                    
                    method = context.Request.Method,
                    url = $"{context.Request.Host}{context.Request.Path}"
                };
                _logger.LogInformation(obj.ToJson());
            }
            

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
