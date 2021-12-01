using Lotus.Blog.TNT.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotus.Blog.Domain.Entities
{
    [Table("chat_histories")]
    public class ChatHistory :BaseEntity
    {
     [ForeignKey("SendUserId")]
            public User User { get; set; }
            /// <summary>
            /// 发送人Id
            /// </summary>
            public int SendUserId { get; set; }
    
            /// <summary>
            /// 头像
            /// </summary>
            [Column(TypeName = "varchar(255)")]
            public string AvatarUrl { get; set; }
    
    
            /// <summary>
            /// 聊天文本
            /// </summary>
            public string Comment { get; set; }
    
            /// <summary>
            /// Ip
            /// </summary>
            [Column(TypeName = "varchar(50)")]
            public string IpAddress { get; set; }
    }
}
