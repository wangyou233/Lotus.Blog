﻿using System.Threading.Tasks;
using Lotus.Blog.Application.Contracts;
using Lotus.Blog.Application.Contracts.Dto.Admin;
using Lotus.Blog.TNT.Web;
using Microsoft.AspNetCore.Mvc;

namespace Lotus.Blog.Api.Controllers.admin
{
    /// <summary>
    /// 管理员
    /// </summary>
    public class AdminController : AdminApiController
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        /// <summary>
        /// 创建管理员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<AdminDto>> InsertAsync(CreateOrUpdateAdmiDto input)
        {
            return await _adminService.InsertAsync(input);
        }

    }
}
