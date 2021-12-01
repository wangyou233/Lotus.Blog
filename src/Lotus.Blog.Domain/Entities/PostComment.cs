using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lotus.Blog.Domain.Shared.Posts;
using Lotus.Blog.TNT.Data.Entity;

namespace Lotus.Blog.Domain.Entities
{
    [Table("post_comment")]
    public class PostComment : BaseEntity
    {
        [Column(TypeName = "varchar(100)")]
        [Required]
        public string Author { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Required]
        public string Email { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string IpAddress { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string AuthorUrl { get; set; }

        
        public bool IsAdmin { get; set; }

        [ForeignKey("PostId")]
        public Post Post { get; set; }
        public int PostId { get; set; }

        public bool AllowNotification { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string UserAgent { get; set; }
        public PostCommentStatus PostCommentStatus { get; set; }

        public string Content { get; set; }

        [ForeignKey("ParentId")]
        public PostComment ParentComment { get; set; }
        public int ParentId { get; set; }
    }
}