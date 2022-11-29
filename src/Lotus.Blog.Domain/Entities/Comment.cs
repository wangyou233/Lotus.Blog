using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lotus.Blog.Domain.Shared.Posts;
using Lotus.Blog.TNT.Data.Entity;

namespace Lotus.Blog.Domain.Entities
{
    [Table("comment")]
    public class Comment : BaseEntity
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
        public int? PostId { get; set; }
        
        
        [ForeignKey("LogId")]
        public Log Log { get; set; }
        
        public int? LogId { get; set; }

        public bool AllowNotification { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string UserAgent { get; set; }
        public PostCommentStatus PostCommentStatus { get; set; }

        public string Content { get; set; }

        [ForeignKey("ParentId")]
        public Comment ParentComment { get; set; }
        public int ParentId { get; set; }
    }
}