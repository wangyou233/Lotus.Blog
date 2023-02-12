using AutoMapper;
using Lotus.Blog.Application.Contracts.Dto.Admin.Log;
using Lotus.Blog.Domain.Entities;

namespace Lotus.Blog.Application.Profiles;

public class LogProfile : Profile
{
    public LogProfile()
    {
        CreateMap<LogCreateOrUpdateDto, Log>();
        CreateMap<Log, LogDto>();
    }
}