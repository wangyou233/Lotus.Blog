using Lotus.Blog.TNT.Data.Ext;
using Lotus.Blog.TNT.Jwt;
using Lotus.Blog.TNT.Web;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Lotus.Blog.TNT.Ext
{
    /// <summary>
    /// Http相关工具类
    /// </summary>
    public static class HttpUtilityExtensions
    {
        /// <summary>
        /// 判断是否是Ajax请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            var h = request.Headers["X-Requested-With"].FirstOrDefault();

            if ("XMLHttpRequest".Equals(h, StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="request"></param>
        /// <param name="defaultPageSize"></param>
        /// <returns></returns>
        public static PageObjectModel GetPageObject(this HttpRequest request, int defaultPageSize = 10)
        {
            string strPage = request.Query["page"];
            string strSize = request.Query["size"];

            return new PageObjectModel
            {
                Page = strPage.ToInt(1).Value,
                Size = strSize.ToInt(defaultPageSize).Value
            };
        }

        /// <summary>
        /// 获取JwtUser
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static JwtUser GetJwtUserFromClaim(this HttpRequest request)
        {
            var user = new JwtUser();

            if (!request.HttpContext.User.Identity.IsAuthenticated)
            {
                return user;
            }
            var claims = request.HttpContext.User.Claims;
            user.Id = claims?.GetClaimValue(ClaimTypes.NameIdentifier).ToInt() ?? 0;
            user.Name = claims.GetClaimValue("name");
            user.Email = claims.GetClaimValue("email");
            return user;
        }

        /// <summary>
        /// 查询一个Jwt项
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetClaimValue(this IEnumerable<Claim> claims, string name)
        {
            return claims?.FirstOrDefault(claim => claim.Type.Equals(name, StringComparison.OrdinalIgnoreCase))?.Value;
        }

        /// <summary>
        /// 获取JsonBody
        /// </summary>
        /// <param name="request"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<string> GetRawBodyAsync(this HttpRequest request, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            using StreamReader reader = new StreamReader(request.Body, encoding);

            return await reader.ReadToEndAsync();
        }
    }
}
