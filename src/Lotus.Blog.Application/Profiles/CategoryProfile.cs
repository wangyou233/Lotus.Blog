using AutoMapper;
using Lotus.Blog.Application.Contracts.Dto.Category;
using Lotus.Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotus.Blog.Application.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateOrUpdateCategoryDto, Category>();
        }
    }
}
