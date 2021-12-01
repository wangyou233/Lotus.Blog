using Autofac;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace Lotus.Blog.TNT.Autofac
{
    public static class AutofacExtensions
    {
        public static ILifetimeScope Container { get; set; }
        
        /// <summary>
        /// 获取全局服务
        /// 警告：此方法使用不当会造成内存溢出,一般开发请勿使用此方法,请使用GetScopeService
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <returns></returns>
        public static T GetService<T>() where T : class
        {
            return Container.Resolve<T>();
        }


        /// <summary>
        /// Autofac依赖注入
        /// </summary>
        public static void AddService()
        {
            var baseType = typeof(IDependency);
            List<string> _fxMobileAssemblies =
            new List<string> {
                "Lotus.Blog.Application",
                "Lotus.Blog.Application.Contracts"
            };
            var builder = new ContainerBuilder();
            var mobileAssemblys = _fxMobileAssemblies.Select(x => Assembly.Load(x)).ToList();
            List<Type> allMobileTypes = new List<Type>();
            mobileAssemblys.ForEach(aAssembly =>
            {
                allMobileTypes.AddRange(aAssembly.GetTypes());
            });
            var diTypes = allMobileTypes
               .Where(x => baseType.IsAssignableFrom(x) && x != baseType)
               .ToArray();
            builder.RegisterTypes(diTypes)
                .AsImplementedInterfaces()
                .PropertiesAutowired()
                .InstancePerDependency();


        }

        /// <summary>
        /// 获取当前请求为生命周期的服务
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <returns></returns>
        public static T GetScopeService<T>() where T : class
        {
            if (typeof(T).Equals(typeof(IHttpContextAccessor)))
            {
                return (T)GetService<IHttpContextAccessor>().HttpContext.RequestServices.GetService(typeof(T));
            }
            return GetService<T>();
        }
    }
}