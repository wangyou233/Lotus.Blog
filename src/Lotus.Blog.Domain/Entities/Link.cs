using System.ComponentModel.DataAnnotations.Schema;
using Lotus.Blog.TNT.Data.Entity;

namespace Lotus.Blog.Domain.Entities
{
    public class Link : BaseEntity
    {
        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Logo { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Team { get; set; }

        public int Sort { get; set; }
    }
}