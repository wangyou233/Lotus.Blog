using Lotus.Blog.Application.Contracts.Services;
using Lotus.Blog.TNT.Swagger;
using Lotus.Blog.TNT.Web;
using Microsoft.AspNetCore.Mvc;

namespace Lotus.Blog.Api.Controllers.web;

/// <summary>
/// 首页
/// </summary>
[ApiExplorerSettings(GroupName = SwaggerExtensions.Grouping.GroupName_v1)]
[Route("home")]
public class HomeController : BaseController
{
    private readonly IPostService _postService;
    private readonly ICommentService _commentService;
    private readonly ILogService _logService;

    /// <summary>
    /// 首页
    /// </summary>
    public HomeController(IPostService postService,ICommentService commentService,ILogService logService)
    {
        _postService = postService;
        _commentService = commentService;
        _logService = logService;
    }
    
    
    
    
    
    
}