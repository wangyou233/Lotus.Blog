using AutoMapper;
using Lotus.Blog.Application.Contracts.Dto.Admin.Comment;
using Lotus.Blog.Application.Contracts.Dto.Admin.CustomView;
using Lotus.Blog.Application.Contracts.Services;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Web;

namespace Lotus.Blog.Api.Controllers.admin;


/// <summary>
/// 自定义页面
/// </summary>
public class CustomViewController: BackGroupEntityController<CustomView,CustomViewDto,CustomViewCreateOrUpdateDto>
{
    public CustomViewController( IMapper mapper, ICustomViewService CustomViewService) : base(CustomViewService, mapper)
    {
    }
}