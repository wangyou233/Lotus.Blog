using AutoMapper;
using Lotus.Blog.Application.Contracts.Dto.Admin;
using Lotus.Blog.Application.Contracts.Dto.Admin.Home;
using Lotus.Blog.Application.Contracts.Dto.Admin.Tag;
using Lotus.Blog.Application.Contracts.Services;
using Lotus.Blog.Domain;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Swagger;
using Lotus.Blog.TNT.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Lotus.Blog.Api.Controllers.admin;


/// <summary>
/// 标签
/// </summary>
///
[Route("admin/tags")]
public class TagController: BackGroupEntityController<Tag,TagDto,TagCreateOrUpdateDto>
{
    private readonly IMemoryCache _memoryCache;

    public TagController( IMapper mapper, ITagService tagService,IMemoryCache memoryCache) : base(tagService, mapper)
    {
        _memoryCache = memoryCache;
    }

    public override Task<TagDto> InsertAsync(TagCreateOrUpdateDto input)
    {
        var entity = _memoryCache.Get<HomeViewDto>(CacheKey.InitKey);
        entity.TagCount++;
        _memoryCache.Set(CacheKey.InitKey, entity);
        return base.InsertAsync(input);
    }

    public override void DeleteAsync(int entityId)
    {
        var entity = _memoryCache.Get<HomeViewDto>(CacheKey.InitKey);
        entity.TagCount--;
        _memoryCache.Set(CacheKey.InitKey, entity);
        base.DeleteAsync(entityId);
    }
}