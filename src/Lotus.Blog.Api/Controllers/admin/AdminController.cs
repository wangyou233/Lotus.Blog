using System.Threading.Tasks;
using AutoMapper;
using Lotus.Blog.Application.Contracts;
using Lotus.Blog.Application.Contracts.Dto.Admin;
using Lotus.Blog.Application.Contracts.Models;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Attribute;
using Lotus.Blog.TNT.Service;
using Lotus.Blog.TNT.Swagger;
using Lotus.Blog.TNT.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lotus.Blog.Api.Controllers.admin
{
    /// <summary>
    /// 管理员
    /// </summary>
    [ApiExplorerSettings(GroupName = SwaggerExtensions.Grouping.GroupName_v2)]
    public class AdminController : BackGroupEntityController<Admin,AdminDto,CreateOrUpdateAdminDto>
    {
        private readonly IAdminService _adminService;


        /// <summary>
        /// 管理员管理
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="adminService"></param>
        public AdminController( IMapper mapper, IAdminService adminService) : base(adminService, mapper)
        {
            _adminService = adminService;
        }

        /// <summary>
        /// 创建管理员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public override async Task<AdminDto> InsertAsync([FromBody]CreateOrUpdateAdminDto input)
        {
            return await _adminService.InsertAsync(input);
        }
        
        /// <summary>
        /// 管理端登录 无需授权
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<string> LoginAsync([FromBody] LoginInput input)
        {
            return await _adminService.LoginAsync(input);
        }

      

    }
}
