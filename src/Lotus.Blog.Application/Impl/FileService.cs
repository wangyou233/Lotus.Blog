using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Autofac;
using Lotus.Blog.TNT.Data.Repository;
using Lotus.Blog.TNT.Service;

namespace Lotus.Blog.Application.Impl;

public interface IFileService: IService<UploadFile>,IDependency
{
    
}

public class FileService : BaseService<UploadFile, IBaseDbRepository>, IDependency, IFileService
{
    
    public FileService(IBaseDbRepository repo) : base(repo)
    {
    }
}
