using AutoMapper;
using Lotus.Blog.Application.Contracts.Dto.Admin;
using Lotus.Blog.Application.Contracts.Dto.Admin.Home;
using Lotus.Blog.Application.Contracts.Dto.Admin.Post;
using Lotus.Blog.Application.Contracts.Dto.Admin.Tag;
using Lotus.Blog.Application.Contracts.Services;
using Lotus.Blog.Application.Impl;
using Lotus.Blog.Domain;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Swagger;
using Lotus.Blog.TNT.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Lotus.Blog.Api.Controllers.admin;

/// <summary>
/// 文章
/// </summary>
[Route("admin/posts")]
public class PostController : BackGroupEntityController<Post, PostDto, PostCreateOrUpdateDto>
{
    private readonly InitService _initService;
    private readonly IMemoryCache _memoryCache;

    public PostController(IMapper mapper, IPostService PostService, InitService initService,IMemoryCache memoryCache) : base(PostService, mapper)
    {
        _initService = initService;
        _memoryCache = memoryCache;
    }

    public override async Task<PostDto> InsertAsync(PostCreateOrUpdateDto input)
    {
        var entity = await base.InsertAsync(input);
        
        _initService.Init();
        return entity;
    }

    public override void SoftDeleteAsync(int entityId)
    {
        base.SoftDeleteAsync(entityId);
        var dto  = _memoryCache.Get<HomeViewDto>(CacheKey.InitKey);
        dto.PostCount--;
        _memoryCache.Set(CacheKey.InitKey, dto);


    }
}