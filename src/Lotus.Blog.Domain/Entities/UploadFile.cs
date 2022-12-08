using System.ComponentModel.DataAnnotations.Schema;
using Lotus.Blog.TNT;
using Lotus.Blog.TNT.Data.Entity;

namespace Lotus.Blog.Domain.Entities
{
    public class UploadFile : BaseEntity
    {
        
        [Column(TypeName = FieldTypes.VAR255)]
        public string Name { get; set; }
        [Column(TypeName = FieldTypes.VAR255)]
        public string Path { get; set; }

        [Column(TypeName = FieldTypes.VAR255)]
        public string RootPath { get; set; }
        [Column(TypeName = FieldTypes.INT)]
        public int Size { get; set; }
        [Column(TypeName = FieldTypes.VAR255)]
        public string FileName { get; set; }

        [Column(TypeName = FieldTypes.VAR50)]
        public string Type { get; set; }
    }
}