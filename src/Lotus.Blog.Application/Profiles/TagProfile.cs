using AutoMapper;
using Lotus.Blog.Application.Contracts.Dto.Admin.Tag;
using Lotus.Blog.Domain.Entities;

namespace Lotus.Blog.Application.Profiles;

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<TagCreateOrUpdateDto, Tag>();
        CreateMap<Tag, TagDto>();
    }
    
}