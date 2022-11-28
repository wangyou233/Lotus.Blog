using Lotus.Blog.TNT.Swagger;
using Lotus.Blog.TNT.Web;
using Microsoft.AspNetCore.Mvc;

namespace Lotus.Blog.Api.Controllers.web;

/// <summary>
/// 首页
/// </summary>
[ApiExplorerSettings(GroupName = SwaggerExtensions.Grouping.GroupName_v1)]
public class HomeController : BaseController
{
    /// <summary>
    /// 首页
    /// </summary>
    public HomeController()
    {
        
    }
    
}