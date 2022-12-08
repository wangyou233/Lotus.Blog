using AutoMapper;
using Lotus.Blog.Application.Contracts.Dto.Category;
using Lotus.Blog.Application.Contracts.Services;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Swagger;
using Lotus.Blog.TNT.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lotus.Blog.Api.Controllers.web;

[Route("/web/categories")]
[ApiExplorerSettings(GroupName = SwaggerExtensions.Grouping.GroupName_v1)]
public class CategoryController : BaseController
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryService categoryService,IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    [HttpGet]
    public async  Task<IList<CategoryDto>> Index()
    {
        var data = await _categoryService.FindAll().ToListAsync();
        var result = _mapper.Map<IList<Category>, IList<CategoryDto>>(data);
        return result;
    }
}