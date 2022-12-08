using AutoMapper;
using Lotus.Blog.Application.Contracts.Dto.Admin.Post;
using Lotus.Blog.Application.Contracts.Dto.Category;
using Lotus.Blog.Application.Contracts.Dto.Web;
using Lotus.Blog.Application.Contracts.Dto.Web.Post;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Data.Ext;
using Lotus.Blog.TNT.Ext;

namespace Lotus.Blog.Application.Profiles;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<Post, PostWebDto>()
            .ForMember(dest => dest.IsPassword, opt => opt.MapFrom(x => !x.PassWord.IsNullOrEmpty()));
        CreateMap<PostCreateOrUpdateDto, Post>()
            .ForMember(x => x.Categories,
                opt => opt.MapFrom(x => x.CategoryIds.IdsToEntitys<Category>()))
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(x => x.TagIds.IdsToEntitys<Tag>()))
            ;
        CreateMap<Post, PostDto>();
    }
}