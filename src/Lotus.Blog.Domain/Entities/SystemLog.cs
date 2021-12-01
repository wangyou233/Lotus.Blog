using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lotus.Blog.TNT.Data.Entity;

namespace Lotus.Blog.Domain.Entities
{
    public class SystemLog : Entity
    {
        /// <summary>
        /// 关联Id
        /// </summary>
        [Column(TypeName = "varchar(100)")]
        public string EventId { get; set; }

        /// <summary>
        /// 事件
        /// </summary>
        [Column(TypeName = "varchar(255)")]
        [Required]
        public string EventTitle { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}