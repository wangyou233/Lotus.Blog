using System.ComponentModel.DataAnnotations.Schema;
using Lotus.Blog.TNT;
using Lotus.Blog.TNT.Data.Entity;

namespace Lotus.Blog.Domain.Entities;

public class Menu : BaseEntity
{
    
    /// <summary>
    /// 名称
    /// </summary>
    [Column(TypeName = FieldTypes.VAR100)]
    public string Title { get; set; }
    
    /// <summary>
    /// 路径
    /// </summary>
    [Column(TypeName = FieldTypes.VAR255)]
    public string Path { get; set; }

    /// <summary>
    /// 时候弹窗页面
    /// </summary>
    public bool IsBlank { get; set; } = false;

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; } = 0;
}