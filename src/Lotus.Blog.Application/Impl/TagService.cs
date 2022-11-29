using Lotus.Blog.Application.Contracts.Services;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Autofac;
using Lotus.Blog.TNT.Data.Repository;
using Lotus.Blog.TNT.Service;

namespace Lotus.Blog.Application.Impl;



public class TagService : BaseService<Tag, IBaseDbRepository>, IDependency, ITagService
{
    
    public TagService(IBaseDbRepository repo) : base(repo)
    {
    }
}