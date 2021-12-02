using Lotus.Blog.TNT.Attribute;
using Lotus.Blog.TNT.Ext;
using Lotus.Blog.TNT.Jwt;
using Microsoft.AspNetCore.Mvc;

namespace Lotus.Blog.TNT.Web
{
    [Route("[controller]")]
    [ApiController]
    [ApiLog]
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