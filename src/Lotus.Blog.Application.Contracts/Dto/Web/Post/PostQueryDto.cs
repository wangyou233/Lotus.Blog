using Lotus.Blog.TNT.Data.Dto;

namespace Lotus.Blog.Application.Contracts.Dto.Web;

/// <summary>
/// 文章
/// </summary>
public class PostQueryDto : QueryDto
{
    
    /// <summary>
    /// 关键字
    /// </summary>
    public string KeyWord { get; set; }
    
    
    /// <summary>
    /// 分类Id
    /// </summary>
    public int? CategoryId { get; set; }
    
    
    /// <summary>
    /// 标签Id
    /// </summary>
    public int? TagId { get; set; }
}