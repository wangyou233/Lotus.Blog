using AutoMapper;
using Lotus.Blog.Application.Contracts.Dto.Admin;
using Lotus.Blog.Domain.Entities;

namespace Lotus.Blog.Application.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<Admin, AdminDto>();
            CreateMap<CreateOrUpdateAdmiDto, Admin>();
        }
    }
}