using Lotus.Blog.TNT.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotus.Blog.Application.Contracts.Dto.Category
{
    /// <summary>
    /// 显示Dto
    /// </summary>
    public class CategoryDto : BaseEntityDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// 上级目录
        /// </summary>
        public int ParentId { get; set; }

        public CategoryDto Parent { get; set; }
        /// <summary>
        /// 加密密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 自定义描述
        /// </summary>
        public string CustomDescription { get; set; }
    }
}
