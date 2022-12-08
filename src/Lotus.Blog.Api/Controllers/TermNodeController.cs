using AutoMapper;
using Lotus.Blog.Application.Contracts.Dto.Admin.TermNode;
using Lotus.Blog.Application.Contracts.Services;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Swagger;
using Lotus.Blog.TNT.Web;
using Microsoft.AspNetCore.Mvc;

namespace Lotus.Blog.Api.Controllers;

[Route("terms")]
[ApiController]
[ApiExplorerSettings(GroupName = SwaggerExtensions.Grouping.GroupName_v3)]
public class TermNodeController: BackGroupEntityController<TermNode,TermNodeDto,TermNodeCreateOrUpdateDto>
{

    public TermNodeController( IMapper mapper, ITermNodeService TermNodeService) : base(TermNodeService, mapper)
    {
    }
}