using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotus.Blog.TNT.Data.Entity
{
    public class BaseEntity : Entity
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// 版本号
        /// </summary>
        [Column("version", Order = 99)]
        public int Version { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Column("created", TypeName = "datetime", Order = 100)]
        [JsonProperty(Order = 100)]
        public DateTime Created { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Column("modified", TypeName = "datetime", Order = 101)]
        [JsonProperty(Order = 101)]
        public DateTime Modified { get; set; }
        /// <summary>
        /// 软删除时间
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        [Column("deleted", TypeName = "datetime", Order = 101)]
        public DateTime? Deleted { get; set; }
    }
}
