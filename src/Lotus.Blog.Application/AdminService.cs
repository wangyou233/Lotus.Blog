using System.Threading.Tasks;
using AutoMapper;
using Lotus.Blog.Application.Contracts;
using Lotus.Blog.Application.Contracts.Dto.Admin;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Autofac;
using Lotus.Blog.TNT.Data.Repository;
using Lotus.Blog.TNT.Service;

namespace Lotus.Blog.Application
{
    public class AdminService : BaseService<Admin,IBaseDbRepository>,IDependency,IAdminService
    {
        private readonly IMapper _mapper;

        public AdminService(IBaseDbRepository repo,IMapper mapper): base(repo)
        {
            _mapper = mapper;
        }


        public async Task<AdminDto> InsertAsync(CreateOrUpdateAdmiDto input)
        {
            var entity = _mapper.Map<Admin>(input);
            entity = await base.InsertAsync(entity);
            return _mapper.Map<AdminDto>(entity);
        }
    }
}