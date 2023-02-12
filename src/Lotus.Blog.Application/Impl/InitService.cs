using Lotus.Blog.Application.Contracts.Dto.Admin.Home;
using Lotus.Blog.Application.Contracts.Services;
using Lotus.Blog.Domain;
using Lotus.Blog.Domain.Shared.TermNode;
using Lotus.Blog.TNT.Data.Ext;
using Lotus.Blog.TNT.Ext;
using Microsoft.Extensions.Caching.Memory;

namespace Lotus.Blog.Application.Impl;

public class InitService 
{
    private readonly IMemoryCache _memoryCache;
    private readonly IPostService _postService;
    private readonly ICommentService _commentService;
    private readonly ITermNodeService _termNodeService;
    private readonly ITagService _tagService;

    public InitService(IMemoryCache memoryCache,IPostService postService,ICommentService commentService,ITermNodeService termNodeService,ITagService tagService)
    {
        _memoryCache = memoryCache;
        _postService = postService;
        _commentService = commentService;
        _termNodeService = termNodeService;
        _tagService = tagService;
    }


    /// <summary>
    /// 初始化数据
    /// </summary>
    public void Init()
    {
        //初始化缓存
        var dto = new HomeViewDto();
        var termNode = _termNodeService.Query().FirstOrDefault(x => x.Type == TermNodeType.CreateSiteTime);
        if (termNode != null)
        {
            var dateTime = termNode.Code.ToDateTime();
            if (dateTime != null) dto.CreatedDay = DateTime.Now.Day + 1  - dateTime.Value.Day;
        }
        
        dto.ReadCount = _postService.Query().Sum(x=>x.ReadCount);
        
        dto.PostCount = _postService.Query().Count();
        
        dto.CommitCount = _commentService.Query().Count();
        
        dto.TagCount = _tagService.Query().Count();
        
        _memoryCache.Set(CacheKey.InitKey, dto);
        
        var executablePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        var uploadPath = Path.Combine(executablePath, "upload");
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }
    }
}