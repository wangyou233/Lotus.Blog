using AutoMapper;
using Lotus.Blog.Application.Contracts.Dto.Category;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Service;
using Lotus.Blog.TNT.Swagger;
using Lotus.Blog.TNT.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lotus.Blog.Api.Controllers.admin
{
    [AllowAnonymous]
    public class CategoryController : BackGroupEntityController<Category, CategoryDto, CreateOrUpdateCategoryDto>
    {
        public CategoryController(IService<Category> entityService, IMapper mapper) : base(entityService, mapper)
        {
        }
    }
}
