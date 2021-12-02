using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using Lotus.Blog.Application.Contracts;
using Lotus.Blog.Application.Contracts.Dto.Admin;
using Lotus.Blog.Application.Contracts.Models;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Attribute;
using Lotus.Blog.TNT.Autofac;
using Lotus.Blog.TNT.Data.Entity;
using Lotus.Blog.TNT.Data.Repository;
using Lotus.Blog.TNT.Jwt;
using Lotus.Blog.TNT.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NETCore.Encrypt;

namespace Lotus.Blog.Application
{
    public class AdminService : BaseService<Admin, IBaseDbRepository>, IDependency, IAdminService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AdminService(IBaseDbRepository repo, IMapper mapper,IConfiguration configuration) : base(repo)
        {
            _mapper = mapper;
            _configuration = configuration;
        }


        public async Task<AdminDto> InsertAsync(CreateOrUpdateAdmiDto input)
        {
            var entity = _mapper.Map<Admin>(input);

            entity.Password = EncryptProvider.Md5(entity.Password);
            
            entity = await base.InsertAsync(entity);
            return _mapper.Map<AdminDto>(entity);
        }

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="EventException"></exception>
        public async Task<string> LoginAsync(LoginInput input)
        {
            input.Password = EncryptProvider.Md5(input.Password);
            var entity = await base.FindOneAsync(z => z.UserName == input.UserName && z.Password == input.Password);
            if (entity == null)
            {
                throw new EventException("账号不存在！");
            }

            var jwtConfig = _configuration.GetSection("jwtconfig").Get<JwtConfig>();
            IList<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier,entity.Id.ToString()));
            claims.Add(new Claim("auth","admin"));
            claims.Add(new Claim("name",entity.UserName));
            claims.Add(new Claim("email",entity.Password));

            var token = JwtUtils.GenerateToken(jwtConfig, claims);
            return token;

        }
    }
}