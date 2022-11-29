using Lotus.Blog.TNT.Data.Entity;

namespace Lotus.Blog.Domain.Entities
{
    public class PostTag : BaseEntity
    {
        /// <summary>
        /// 文章Id
        /// </summary>
        public int PostId { get; set; }

        /// <summary>
        /// 标签Id
        /// </summary>
        public int TagId { get; set; }
    }
}