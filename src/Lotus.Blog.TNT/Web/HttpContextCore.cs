using Lotus.Blog.TNT.Autofac;
using Microsoft.AspNetCore.Http;

namespace Lotus.Blog.TNT.Web
{
    public static class HttpContextCore
    {
        public static HttpContext Current { get => AutofacExtensions.GetService<IHttpContextAccessor>().HttpContext; }
    }
}