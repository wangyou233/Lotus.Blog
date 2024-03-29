﻿using Lotus.Blog.TNT.Attribute;
using Lotus.Blog.TNT.Attributes;
using Lotus.Blog.TNT.Ext;
using Lotus.Blog.TNT.Jwt;
using Microsoft.AspNetCore.Mvc;

namespace Lotus.Blog.TNT.Web
{
    [ApiController]
    [ApiLog]
    [FormatResponse]
    public class BaseController : ControllerBase
    {
        
        /// <summary>
        /// 获取IP地址
        /// </summary>
        public string IpAddress
        {
            get
            {
                return Request.HttpContext.Connection.RemoteIpAddress.ToString();
            }
        }
        public string Token
        {
            get
            {
                return Request.Headers.ContainsKey("Authorization") ? Request.Headers["Authorization"]: Request.Query["Authorization"];
            }
        }
        public JwtUser CurrentUser
        {
            get { return Request.GetJwtUserFromClaim(); }
        }
    }
}