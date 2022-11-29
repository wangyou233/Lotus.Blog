﻿using System.ComponentModel.DataAnnotations.Schema;
using Lotus.Blog.Domain.Shared.Posts;
using Lotus.Blog.TNT;
using Lotus.Blog.TNT.Data.Entity;

namespace Lotus.Blog.Domain.Entities
{
    /// <summary>
    /// 文章
    /// </summary>
    public class Post : BaseEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Column(TypeName = FieldTypes.VAR255)]
        public string Title { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        [Column(TypeName = FieldTypes.VAR255)]
        public string Alias { get; set; }

        /// <summary>
        /// 封面图
        /// </summary>
        [Column(TypeName = FieldTypes.VAR255)]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 加密密码
        /// </summary>
        [Column(TypeName = FieldTypes.VAR255)]
        public string PassWord { get; set; }

        /// <summary>
        /// Seo优化
        /// </summary>
        [Column(TypeName =  FieldTypes.TEXT)]
        public string SeoSetting { get; set; }

        /// <summary>
        /// 自定义描述
        /// </summary>
        [Column(TypeName = FieldTypes.TEXT)]
        public string CustomDescription { get; set; }

        /// <summary>
        /// 是否顶置
        /// </summary>
        public bool IsTop { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        [Column(TypeName = FieldTypes.VAR255)]
        public string Author { get; set; }

        /// <summary>
        /// 转载链接
        /// </summary>
        [Column(TypeName = FieldTypes.VAR255)]
        public string Url { get; set; }

        /// <summary>
        /// Markdown
        /// </summary>
        [Column(TypeName = FieldTypes.LONGTEXT)]
        public string Markdown { get; set; }

        /// <summary>
        /// 分类Id
        /// </summary>
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        
        /// <summary>
        /// 状态
        /// </summary>
        [Column(TypeName = FieldTypes.ENUM)]
        public PostStatus Status { get; set; }


        public int ReadCount { get; set; }
        [Column(TypeName = FieldTypes.JSON)]
        public string ExtData { get; set; }
        public IQueryable<Comment> Comments { get; set; }
        
        
        public IQueryable<Tag> Tags { get; set; }
    }
}