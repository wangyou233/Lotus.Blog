using AutoMapper;
using Lotus.Blog.Application.Contracts.Dto.Admin.Log;
using Lotus.Blog.Application.Contracts.Services;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Web;
using Microsoft.AspNetCore.Components;

namespace Lotus.Blog.Api.Controllers.admin;

/// <summary>
/// 日志
/// </summary>
[Route("admin/logs")]
public class LogController: BackGroupEntityController<Log,LogDto,LogCreateOrUpdateDto>
{
    public LogController( IMapper mapper, ILogService LogService) : base(LogService, mapper)
    {
    }
}