using AutoMapper;
using Lotus.Blog.Application.Contracts.Dto.Admin;
using Lotus.Blog.Application.Contracts.Dto.Admin.Post;
using Lotus.Blog.Application.Contracts.Dto.Admin.Tag;
using Lotus.Blog.Application.Contracts.Services;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Swagger;
using Lotus.Blog.TNT.Web;
using Microsoft.AspNetCore.Mvc;

namespace Lotus.Blog.Api.Controllers.admin;


/// <summary>
/// 文章
/// </summary>
public class PostController: BackGroupEntityController<Post,PostDto,PostCreateOrUpdateDto>
{
    public PostController( IMapper mapper, IPostService PostService) : base(PostService, mapper)
    {
    }
}