using Lotus.Blog.TNT.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lotus.Blog.TNT;

namespace Lotus.Blog.Domain.Entities
{
    public class Category : BaseEntity
    {/// <summary>
     /// 标题
     /// </summary>
        [Column(TypeName = FieldTypes.VAR100)]
        public string Title { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        [Column(TypeName = FieldTypes.VAR100)]
        public string Alias { get; set; }

        /// <summary>
        /// 上级目录
        /// </summary>
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public Category Parent { get; set; }
        /// <summary>
        /// 加密密码
        /// </summary>
        [Column(TypeName = FieldTypes.VAR100)]
        public string PassWord { get; set; }
        /// <summary>
        /// 自定义描述
        /// </summary>
        [Column(TypeName = FieldTypes.TEXT)]
        public string CustomDescription { get; set; }

        /// <summary>
        /// 文章数
        /// </summary>
        [Column(TypeName = FieldTypes.INT)]
        public int PostCount { get; set; }
    }
}
