using System.ComponentModel.DataAnnotations.Schema;
using Lotus.Blog.TNT;
using Lotus.Blog.TNT.Data.Entity;

namespace Lotus.Blog.Domain.Entities;

public class View : Entity
{
    
    [Column(TypeName = FieldTypes.VAR255)]
    public string ViewTitle { get; set; }
    
    [Column(TypeName = FieldTypes.VAR255)]
    public string PostTitle { get; set; }
    
    [Column(TypeName = FieldTypes.LONGTEXT)]
    public string PostDescription { get; set; }
    
    [Column(TypeName = FieldTypes.VAR255)]
    public string ViewAlias { get; set; }
    
    [Column(TypeName = FieldTypes.TEXT)]
    public string ViewSummary { get; set; }
    
    
    
    [Column(TypeName = FieldTypes.VAR255)]
    public string CoverPath { get; set; }
    
    public bool IsCommit { get; set; }
    
    public DateTime PublishDateTime { get; set; }
    
    [Column(TypeName = FieldTypes.VAR255)]
    public string SEOKeyword { get; set; }
    
    [Column(TypeName = FieldTypes.TEXT)]
    public string SEODescribe { get; set; }
    
    
    [Column(TypeName = FieldTypes.JSON)]
    public string ExtData { get; set; }
}