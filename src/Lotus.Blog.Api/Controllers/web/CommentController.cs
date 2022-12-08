using AutoMapper;
using Lotus.Blog.Application.Contracts.Services;
using Lotus.Blog.TNT.Swagger;
using Lotus.Blog.TNT.Web;
using Microsoft.AspNetCore.Mvc;

namespace Lotus.Blog.Api.Controllers.web;
[Route("/web/comments")]
[ApiExplorerSettings(GroupName = SwaggerExtensions.Grouping.GroupName_v1)]
public class CommentController : BaseController
{
    private readonly IMapper _mapper;
    private readonly ICommentService _commentService;


    public CommentController(IMapper mapper,ICommentService commentService)
    {
        _mapper = mapper;
        _commentService = commentService;
    }
    
  
}