using Lotus.Blog.Application.Contracts.Dto.Admin.TermNode;
using Lotus.Blog.Application.Contracts.Services;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Attribute;
using Lotus.Blog.TNT.Swagger;
using Lotus.Blog.TNT.Web;
using Microsoft.AspNetCore.Mvc;

namespace Lotus.Blog.Api.Controllers;

[ApiExplorerSettings(GroupName = SwaggerExtensions.Grouping.GroupName_v3)]

public class FileController : BaseController
{
    private readonly IUploadFileService _uploadFileService;

    public FileController(IUploadFileService uploadFileService)
    {
        _uploadFileService = uploadFileService;
    }

    /// <summary>
    /// 获取指定文件
    /// </summary>
    /// <param name="fileId">文件Id</param>
    /// <returns></returns>
    [HttpGet("files/{fileId}/download")]
    public async Task<FileContentResult> GetFileAsync(int fileId)
    {
        var file = await _uploadFileService.FindAsync(fileId);
        if (file == null)
        {
            throw new EventException("文件不存在");
        }

        var content = await _uploadFileService.GetFileAsync(file.Path);
        Response.Headers.Add("Access-Control-Expose-Headers","Content-Disposition,X-Suggested-Filename");
        return File(content, file.Type,file.FileName);
    }

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="files">文件</param>
    /// <returns></returns>
    [HttpPost("upload")]
    [DisableRequestSizeLimit]
    public async Task<IList<UploadFile>> UploadFile([FromForm] IList<IFormFile> files) 
    {
        if (files == null || files.Count == 0)
        {
            throw new EventException("参数错误");
        }

        var uploadFiles = await _uploadFileService.ProcessUploadedFiles(files);

        return uploadFiles;
    }
    
    
}