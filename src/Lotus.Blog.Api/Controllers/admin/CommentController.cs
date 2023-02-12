using AutoMapper;
using Lotus.Blog.Application.Contracts.Dto.Admin;
using Lotus.Blog.Application.Contracts.Dto.Admin.Comment;
using Lotus.Blog.Application.Contracts.Dto.Admin.Post;
using Lotus.Blog.Application.Contracts.Dto.Admin.Tag;
using Lotus.Blog.Application.Contracts.Services;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Swagger;
using Lotus.Blog.TNT.Web;
using Microsoft.AspNetCore.Mvc;

namespace Lotus.Blog.Api.Controllers.admin;


/// <summary>
/// 评论
/// </summary>
[Route("admin/comments")]
public class CommentController: BackGroupEntityController<Comment,CommentDto,CommentCreateOrUpdateDto>
{
    public CommentController( IMapper mapper, ICommentService CommentService) : base(CommentService, mapper)
    {
    }
}