using Lotus.Blog.TNT.Autofac;
using Lotus.Blog.TNT.Ext;
using Lotus.Blog.TNT.Logger;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Lotus.Blog.TNT.Attribute
{
    public class GlobalExceptionFilter: BaseActionFilter, IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception;
            if (ex is EventException busEx)
            {
                _logger.LogError($"{busEx.ErrorCode}_{busEx.Message}");
            }
            else
            {
                _logger.LogError($"{ex.Message}");
            }
        }
    }
}