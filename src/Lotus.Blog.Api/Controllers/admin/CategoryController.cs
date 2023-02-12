using AutoMapper;
using Lotus.Blog.Application.Contracts.Dto.Admin.Home;
using Lotus.Blog.Application.Contracts.Dto.Category;
using Lotus.Blog.Domain;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Service;
using Lotus.Blog.TNT.Swagger;
using Lotus.Blog.TNT.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Lotus.Blog.Api.Controllers.admin
{
    
    /// <summary>
    /// 分类
    /// </summary>
    [Route("admin/categories")]
    public class CategoryController : BackGroupEntityController<Category, CategoryDto, CreateOrUpdateCategoryDto>
    {
        private readonly IMemoryCache _memoryCache;

        public CategoryController(IService<Category> entityService, IMapper mapper,IMemoryCache memoryCache) : base(entityService, mapper)
        {
            _memoryCache = memoryCache;
        }
        public override Task<CategoryDto> InsertAsync(CreateOrUpdateCategoryDto input)
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
}
