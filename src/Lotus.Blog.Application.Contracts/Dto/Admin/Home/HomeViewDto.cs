namespace Lotus.Blog.Application.Contracts.Dto.Admin.Home;

public class HomeCacheViewDto
{
    
    /// <summary>
    /// 创建天数
    /// </summary>
    public int CreatedDay { get; set; }
    
    
    /// <summary>
    /// 文章数 
    /// </summary>
    public int PostCount { get; set; }
    
    /// <summary>
    /// 评论数
    /// </summary>
    public int CommitCount { get; set; }
    
    
    /// <summary>
    /// 阅读数
    /// </summary>
    public int ReadCount { get; set; }
    
    /// <summary>
    /// 标签数
    /// </summary>
    public int TagCount { get; set; }
}

public class HomeViewDto : HomeCacheViewDto
{
    
}