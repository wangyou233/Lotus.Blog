using AutoMapper;
using Lotus.Blog.Application.Contracts.Dto.Admin.Link;
using Lotus.Blog.Application.Contracts.Services;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Web;

namespace Lotus.Blog.Api.Controllers.admin;

/// <summary>
/// 友链
/// </summary>
public class LinkController: BackGroupEntityController<Link,LinkDto,LinkCreateOrUpdateDto>
{
    public LinkController( IMapper mapper, ILinkService LinkService) : base(LinkService, mapper)
    {
    }
}