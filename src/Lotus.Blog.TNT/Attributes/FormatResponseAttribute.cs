using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lotus.Blog.TNT.Attributes
{
    /// <summary>
    /// 全局返回封装
    /// </summary>
    public class FormatResponseAttribute : BaseActionFilter, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is EmptyResult)
                context.Result = Success();
            else if (context.Result is ObjectResult res)
            {
                context.Result = Success(res.Value);
            }
        }
    }
}