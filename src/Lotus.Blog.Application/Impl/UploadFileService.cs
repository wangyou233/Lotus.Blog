using Lotus.Blog.Application.Contracts.Services;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Autofac;
using Lotus.Blog.TNT.Data.Repository;
using Lotus.Blog.TNT.Files;
using Lotus.Blog.TNT.Service;
using Microsoft.AspNetCore.Http;

namespace Lotus.Blog.Application.Impl;

public class UploadFileService : BaseService<UploadFile, IBaseDbRepository>, IDependency, IUploadFileService
{
    private readonly IFileStorage _fileStorage;

    public UploadFileService(IBaseDbRepository repo, IFileStorage fileStorage) : base(repo)
    {
        _fileStorage = fileStorage;
    }

    private readonly string UploadPath =
        Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase ?? string.Empty, "upload");

    public async Task<byte[]> GetFileAsync(string filePath)
    {
        var savePath = Path.Combine(UploadPath, filePath);
        if (!File.Exists(savePath))
        {
            return null;
        }

        return await File.ReadAllBytesAsync(savePath);

    }

    public async Task<UploadFile> ProcessUploadedFile(IFormFile file)
    {
        var contentType = file.ContentType;
        var originalFileName = file.FileName;
        var ext = Path.GetExtension(originalFileName);
        var newFileName = Guid.NewGuid().ToString();
        var fileName = Path.HasExtension(originalFileName) ? $"{newFileName}{ext}" : newFileName;
        var filePath = _fileStorage.GenerateFilePath(fileName);
        var savePath = Path.Combine(UploadPath, _fileStorage.GenerateFilePath(fileName));
        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            var fileContent = memoryStream.ToArray();
            await _fileStorage.SaveFileAsync(savePath, fileContent);
            return await InsertAsync(new UploadFile
            {
                Name = originalFileName,
                Path = filePath,
                Size = fileContent.Length,
                Type = contentType,
            });
        }
    }

    public async Task<IList<UploadFile>> ProcessUploadedFiles(IList<IFormFile> files)
    {
        IList<UploadFile> uploadFiles = new List<UploadFile>();
        foreach (var file in files)
        {
            var uploadFile = await ProcessUploadedFile(file);
            uploadFiles.Add(uploadFile);
        }

        return uploadFiles;
    }
}