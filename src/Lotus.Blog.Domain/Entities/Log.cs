using System.ComponentModel.DataAnnotations.Schema;
using Lotus.Blog.TNT;
using Lotus.Blog.TNT.Data.Entity;

namespace Lotus.Blog.Domain.Entities;

public class Log : BaseEntity
{
    
    /// <summary>
    /// 是否公开
    /// </summary>
    public bool IsPublic { get; set; }
    
    
    [Column(TypeName = FieldTypes.INT)]
    public int LikeNumbers { get; set; }
    
    /// <summary>
    /// Markdown
    /// </summary>
    [Column(TypeName = FieldTypes.LONGTEXT)]
    public string Markdown { get; set; }

}