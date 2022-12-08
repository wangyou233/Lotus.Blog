using AutoMapper;
using Lotus.Blog.Application.Contracts.Dto.Admin.Tag;
using Lotus.Blog.Application.Contracts.Services;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Swagger;
using Lotus.Blog.TNT.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lotus.Blog.Api.Controllers.web;

[Route("/web/tags")]
[ApiExplorerSettings(GroupName = SwaggerExtensions.Grouping.GroupName_v1)]
public class TagController : BaseController
{
    private readonly ITagService _tagService;
    private readonly IMapper _mapper;

    public TagController(ITagService tagService,IMapper mapper)
    {
        _tagService = tagService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<List<TagDto>> Index()
    {
        var result = await _tagService.FindAll().ToListAsync();
        var data =_mapper.Map<List<Tag>, List<TagDto>>(result);
        return data ;
    }
}