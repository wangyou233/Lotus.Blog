using System.ComponentModel.DataAnnotations.Schema;
using Lotus.Blog.TNT.Data.Entity;

namespace Lotus.Blog.Domain.Entities;

public class PostCategory: BaseEntity
{
    /// <summary>
    /// 文章Id
    /// </summary>
    [ForeignKey("PostId")]
    public int PostId { get; set; }
    public Post Post { get; set; }
    /// <summary>
    /// 标签Id
    /// </summary>
    [ForeignKey("CategoryId")]
    public int CategoryId { get; set; }
        
    public Category Category { get; set; }
    
}