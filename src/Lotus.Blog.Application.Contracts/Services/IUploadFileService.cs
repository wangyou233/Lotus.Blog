using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Autofac;
using Lotus.Blog.TNT.Service;
using Microsoft.AspNetCore.Http;

namespace Lotus.Blog.Application.Contracts.Services;

public interface IUploadFileService : IService<UploadFile>,IDependency
{
    
    
    Task<byte[]> GetFileAsync(string filePath);

    Task<UploadFile> ProcessUploadedFile(IFormFile file);
    
    Task<IList<UploadFile>> ProcessUploadedFiles(IList<IFormFile> files);

}