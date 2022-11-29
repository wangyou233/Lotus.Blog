using System.ComponentModel.DataAnnotations.Schema;
using Lotus.Blog.TNT.Data.Entity;

namespace Lotus.Blog.Domain.Entities
{
    public class PostTag : BaseEntity {
        /// <summary>
        /// 文章Id
        /// </summary>
        [ForeignKey("PostId")]
        public int PostId { get; set; }
        public Post Post { get; set; }
        /// <summary>
        /// 标签Id
        /// </summary>
        [ForeignKey("TagId")]
        public int TagId { get; set; }
        
        public Tag Tag { get; set; }
    }
}