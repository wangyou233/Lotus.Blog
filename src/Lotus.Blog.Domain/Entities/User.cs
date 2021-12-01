using System.ComponentModel.DataAnnotations.Schema;
using Lotus.Blog.Domain.Shared.Users;
using Lotus.Blog.TNT.Data.Entity;

namespace Lotus.Blog.Domain.Entities
{
    public class User : BaseEntity
    {
        [Column(TypeName = "varchar(100)")]
        public string UserName { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string NiceName { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Password { get; set; }

        public int Phone { get; set; }

        public UserStatus UserStatus { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [Column(TypeName = "varchar(255)")]
        public string AvatarUrl { get; set; }
    }
}