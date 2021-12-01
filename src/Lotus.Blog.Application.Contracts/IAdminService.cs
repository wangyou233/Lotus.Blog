using System.Threading.Tasks;
using Lotus.Blog.Application.Contracts.Dto.Admin;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Autofac;
using Lotus.Blog.TNT.Service;

namespace Lotus.Blog.Application.Contracts
{
    public interface IAdminService : IService<Admin>,IDependency
    {


        /// <summary>
        /// 插入管理员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<AdminDto> InsertAsync(CreateOrUpdateAdmiDto input);
    }
}