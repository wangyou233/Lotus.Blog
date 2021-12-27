using Lotus.Blog.TNT.Attribute;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Lotus.Blog.TNT.Attributes
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
                context.Result = Error(busEx.Message, busEx.ErrorCode.ToString());

            }
            else
            {
                _logger.LogError($"{ex.Message}");
                context.Result = Error(ex.Message);
            }
        }
    }
}