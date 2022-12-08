using Lotus.Blog.Application.Contracts.Dto.Web;
using Lotus.Blog.Application.Contracts.Dto.Web.Post;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Autofac;
using Lotus.Blog.TNT.Service;
using Lotus.Blog.TNT.Web;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lotus.Blog.Application.Contracts.Services;

public interface IPostService : IService<Post>,IDependency
{

    Task<PageList<PostWebDto>> Query(PostQueryDto model);

}