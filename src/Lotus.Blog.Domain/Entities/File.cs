using System.ComponentModel.DataAnnotations.Schema;
using Lotus.Blog.TNT.Data.Entity;

namespace Lotus.Blog.Domain.Entities
{
    public class File : BaseEntity
    {
        [Column(TypeName = "varchar(100)")]
        public string Path { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string RootPath { get; set; }

        public int Size { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string FileName { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Type { get; set; }
    }
}