using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lotus.Blog.Domain.Shared.SystemLog;
using Lotus.Blog.TNT;
using Lotus.Blog.TNT.Data.Entity;

namespace Lotus.Blog.Domain.Entities
{
    public class SystemLog : BaseEntity
    {
        /// <summary>
        /// 关联Id
        /// </summary>
        [Column(TypeName = "varchar(100)")]
        public string EventId { get; set; }

        [Column(TypeName = FieldTypes.ENUM)]

        public SystemLogType SystemLogType { get; set; }
        /// <summary>
        /// 事件
        /// </summary>
        [Column(TypeName = "varchar(255)")]
        public string EventTitle { get; set; }

        
        [Column(TypeName = FieldTypes.VAR50)]
        public string IP { get; set; }
        
        [Column(TypeName = FieldTypes.VAR255)]
        public string Description { get; set; }

        

        
        
    }
}