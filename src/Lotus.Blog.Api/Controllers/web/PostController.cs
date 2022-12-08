using Lotus.Blog.Application.Contracts.Dto.Admin.Post;
using Lotus.Blog.Application.Contracts.Dto.Web;
using Lotus.Blog.Application.Contracts.Dto.Web.Post;
using Lotus.Blog.Application.Contracts.Services;
using Lotus.Blog.TNT.Attribute;
using Lotus.Blog.TNT.Swagger;
using Lotus.Blog.TNT.Web;
using Microsoft.AspNetCore.Mvc;

namespace Lotus.Blog.Api.Controllers.web;

[Route("/web/posts")]
[ApiExplorerSettings(GroupName = SwaggerExtensions.Grouping.GroupName_v1)]
public class PostController : BaseController
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }


    [HttpGet]
    public async  Task<PageList<PostWebDto>> Index(PostQueryDto model)
    {
        if (!ModelState.IsValid)
        {
            throw new EventException("参数错误");
        }

        return await _postService.Query(model);
    }
}