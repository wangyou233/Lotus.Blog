using AutoMapper;
using Lotus.Blog.Application.Contracts.Dto.Admin.Comment;
using Lotus.Blog.Domain.Entities;

namespace Lotus.Blog.Application.Profiles;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<CommentCreateOrUpdateDto, Comment>();
        CreateMap<Comment, CommentDto>();
    }
}