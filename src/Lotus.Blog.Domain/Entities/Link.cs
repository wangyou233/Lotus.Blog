using System.ComponentModel.DataAnnotations.Schema;
using Lotus.Blog.TNT;
using Lotus.Blog.TNT.Data.Entity;

namespace Lotus.Blog.Domain.Entities
{
    public class Link : BaseEntity
    {
        [Column(TypeName = FieldTypes.VAR255)]
        public string Name { get; set; }
        [Column(TypeName = FieldTypes.VAR255)]
        public string Logo { get; set; }
        [Column(TypeName = FieldTypes.TEXT)]
        public string Description { get; set; }
        [Column(TypeName = FieldTypes.VAR255)]
        public string Team { get; set; }
        [Column(TypeName = FieldTypes.INT)]
        public int Sort { get; set; }
    }
}