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
        public int Version { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Created { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? Modified { get; set; }
        /// <summary>
        /// 软删除时间
        /// </summary>
        public DateTime? Deleted { get; set; }
    }
}
