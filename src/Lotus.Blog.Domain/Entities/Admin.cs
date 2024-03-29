﻿using Lotus.Blog.TNT.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lotus.Blog.TNT;

namespace Lotus.Blog.Domain.Entities
{
    public class Admin : BaseEntity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Column(TypeName = FieldTypes.VAR255)]
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Column(TypeName = FieldTypes.VAR255)]
        public string NiceName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Column(TypeName = FieldTypes.VAR255)]
        public string Password { get; set; }


        /// <summary>
        /// 头像
        /// </summary>
        [Column(TypeName = FieldTypes.VAR255)]
        public string AvatarUrl { get; set; }

        /// <summary>
        /// 自定义描述
        /// </summary>
        public string CustomDescription { get; set; }
    }
}
