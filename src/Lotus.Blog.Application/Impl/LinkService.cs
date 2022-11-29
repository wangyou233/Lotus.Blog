using Lotus.Blog.Application.Contracts.Services;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Autofac;
using Lotus.Blog.TNT.Data.Repository;
using Lotus.Blog.TNT.Service;

namespace Lotus.Blog.Application.Impl;


public class LinkService : BaseService<Link, IBaseDbRepository>, IDependency, ILinkService
{
    
    public LinkService(IBaseDbRepository repo) : base(repo)
    {
    }
}
