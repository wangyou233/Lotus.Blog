using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Autofac;
using Lotus.Blog.TNT.Service;

namespace Lotus.Blog.Application.Contracts.Services;

public interface ITermNodeService: IService<TermNode>,IDependency
{
    
}