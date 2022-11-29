using AutoMapper;
using Lotus.Blog.Application.Contracts.Dto.Admin.Log;
using Lotus.Blog.Application.Contracts.Services;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Web;

namespace Lotus.Blog.Api.Controllers.admin;

/// <summary>
/// 友链
/// </summary>
public class LogController: BackGroupEntityController<Log,LogDto,LogCreateOrUpdateDto>
{
    public LogController( IMapper mapper, ILogService LogService) : base(LogService, mapper)
    {
    }
}