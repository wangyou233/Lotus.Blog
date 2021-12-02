using Lotus.Blog.TNT.Data.Dto;

namespace Lotus.Blog.Application.Contracts.Dto.Admin
{
    public class AdminDto : BaseEntityDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// 昵称
        /// </summary>
        public string NiceName { get; set; }



        /// <summary>
        /// 头像
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// 自定义描述
        /// </summary>
        public string CustomDescription { get; set; }
    }
}