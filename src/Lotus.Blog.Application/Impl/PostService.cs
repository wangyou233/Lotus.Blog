using Lotus.Blog.Application.Contracts.Services;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Autofac;
using Lotus.Blog.TNT.Data.Repository;
using Lotus.Blog.TNT.Service;

namespace Lotus.Blog.Application.Impl;



public class PostService : BaseService<Post, IBaseDbRepository>, IDependency, IPostService
{
    
    public PostService(IBaseDbRepository repo) : base(repo)
    {
    }
}