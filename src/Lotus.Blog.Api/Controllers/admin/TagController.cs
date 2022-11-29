using AutoMapper;
using Lotus.Blog.Application.Contracts.Dto.Admin;
using Lotus.Blog.Application.Contracts.Dto.Admin.Tag;
using Lotus.Blog.Application.Contracts.Services;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Swagger;
using Lotus.Blog.TNT.Web;
using Microsoft.AspNetCore.Mvc;

namespace Lotus.Blog.Api.Controllers.admin;


/// <summary>
/// 标签
/// </summary>
public class TagController: BackGroupEntityController<Tag,TagDto,TagCreateOrUpdateDto>
{
    public TagController( IMapper mapper, ITagService tagService) : base(tagService, mapper)
    {
    }
}