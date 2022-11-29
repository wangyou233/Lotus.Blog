using Lotus.Blog.Application.Contracts.Services;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Autofac;
using Lotus.Blog.TNT.Data.Repository;
using Lotus.Blog.TNT.Service;
using Microsoft.AspNetCore.Http;

namespace Lotus.Blog.Application.Impl;

public class UploadFileService : BaseService<UploadFile, IBaseDbRepository>, IDependency, IUploadFileService
{
    
    public UploadFileService(IBaseDbRepository repo) : base(repo)
    {
    }

    public Task<byte[]> GetFileAsync(string filePath)
    {
        
        
        var path = GetAbsolutePath(filePath);

        if (!File.Exists(path))
        {
            return null;
        }

        throw new NotImplementedException();
    }

    public Task<UploadFile> ProcessUploadedFile(IFormFile file)
    {
        throw new NotImplementedException();
    }

    public Task<IList<UploadFile>> ProcessUploadedFiles(IList<IFormFile> files)
    {
        throw new NotImplementedException();
    }


    private string GetAbsolutePath(string filePath)
    {
        return "";
        // return Path.Combine(App, relatePath)
    }
}