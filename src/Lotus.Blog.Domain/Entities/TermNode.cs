using System.ComponentModel.DataAnnotations.Schema;
using Lotus.Blog.Domain.Shared.TermNode;
using Lotus.Blog.TNT;
using Lotus.Blog.TNT.Data.Entity;

namespace Lotus.Blog.Domain.Entities;

public class TermNode : BaseEntity
{
    [Column(TypeName = FieldTypes.VAR255)]
    public string Code { get; set; }

    [Column(TypeName = FieldTypes.VAR255)]
    public string Name { get; set; }
    
    [Column(TypeName = FieldTypes.ID)]
    public string ParentId { get; set; }

    [ForeignKey("ParentId")]
    public TermNode Parent { get; set; }
    
    [Column(TypeName = FieldTypes.ENUM)]
    public TermNodeType Type { get; set; }
    
    [Column(TypeName = FieldTypes.VAR255)]
    public string Description { get; set; }

    
    [Column(TypeName = FieldTypes.JSON)]
    public string ExtData { get; set; }

}