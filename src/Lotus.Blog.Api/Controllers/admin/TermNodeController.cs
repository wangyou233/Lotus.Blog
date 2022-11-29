using AutoMapper;
using Lotus.Blog.Application.Contracts.Dto.Admin.TermNode;
using Lotus.Blog.Application.Contracts.Services;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Web;

namespace Lotus.Blog.Api.Controllers.admin;

/// <summary>
/// 字典
/// </summary>
public class TermNodeController: BackGroupEntityController<TermNode,TermNodeDto,TermNodeCreateOrUpdateDto>
{
    public TermNodeController( IMapper mapper, ITermNodeService TermNodeService) : base(TermNodeService, mapper)
    {
    }
}