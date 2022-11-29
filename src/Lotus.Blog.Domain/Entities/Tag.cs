using System.ComponentModel.DataAnnotations.Schema;
using Lotus.Blog.TNT;
using Lotus.Blog.TNT.Data.Entity;

namespace Lotus.Blog.Domain.Entities
{
    public class Tag:BaseEntity
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        [Column(TypeName =FieldTypes.VAR255)]
        public string TagName { get; set; }
        /// <summary>
        /// 展示名称
        /// </summary>
        [Column(TypeName =FieldTypes.VAR255)]
        public string DisplayName { get; set; }
        
        [Column(TypeName = FieldTypes.INT)]
        public int TagPostCount { get; set; }
        
        
    }
}