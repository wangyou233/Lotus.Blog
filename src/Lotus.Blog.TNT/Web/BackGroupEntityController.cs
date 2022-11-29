using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Lotus.Blog.TNT.Attribute;
using Lotus.Blog.TNT.Data.Dto;
using Lotus.Blog.TNT.Data.Entity;
using Lotus.Blog.TNT.Ext;
using Lotus.Blog.TNT.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lotus.Blog.TNT.Web
{
    /// <summary>
    /// CURD基础
    /// </summary>
    [Route("/admin/[controller]")]
    [Authorize]
    public class BackGroupEntityController<TEntity, TDto, TCreateOrUpdateDto> : BaseController
        where TEntity : BaseEntity
        where TDto : BaseEntityDto
    {
        private readonly IService<TEntity> _entityService;
        private readonly IMapper _mapper;

        protected BackGroupEntityController(IService<TEntity> entityService,IMapper mapper)
        {
            _entityService = entityService;
            _mapper = mapper;
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<IList<TDto>> IndexAsync()
        {
            var list = await _entityService.FindAll().ToListAsync();
            return _mapper.Map<IList<TEntity>,IList< TDto>>(list);
        }

        /// <summary>
        /// 获取分页  page,size
        /// </summary>
        /// <returns></returns>
        [HttpGet("page")]
        public virtual async Task<PageList<TDto>> PageListAsync()
        {
            var pageResult = await _entityService.FindPageAsync(Request.GetPageObject(), null, z => z.Created,"");
            var result = pageResult.ConvertView<TDto>(x=>_mapper.Map<TDto>(x));
            return result;
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        [HttpGet("{entityId:int}")]
        public virtual async Task<TDto> GetAsync(int entityId)
        {
            var entity = await _entityService.FindAsync(entityId);
            if (entity == null)
            {
                throw new EventException("实体不存在");
            }

            return _mapper.Map<TDto>(entity);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<TDto> InsertAsync([FromBody] TCreateOrUpdateDto input)
        {
            if (!ModelState.IsValid)
            {
                throw new EventException(ModelState.Values.ToJson());
            }

            var entity = await _entityService.InsertAsync(_mapper.Map<TEntity>(input));
            return _mapper.Map<TDto>(entity);
        }

        /// <summary>
        /// 修改一个实体
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="EventException"></exception>
        [HttpPut("{entityId:int}")]
        public virtual async Task<TDto> UpdateAsync(int entityId, [FromBody] TCreateOrUpdateDto input)
        {
            if (!ModelState.IsValid)
            {
                throw new EventException(ModelState.Values.ToJson());
            }
            var entity = await _entityService.FindAsync(entityId);
            if (entity == null)
            {
                throw new EventException("实体不存在");
            }

            _mapper.Map(input, entity);
            await _entityService.UpdateAsync(entity);
            return _mapper.Map<TDto>(entity);
        }

        /// <summary>
        /// 删除实体 数据库中删除 建议使用软删除
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        [HttpDelete("{entityId:int}")]
        public virtual async void DeleteAsync(int entityId)
        {
            var entity = await _entityService.FindAsync(entityId);
            if (entity == null)
            {
                throw new EventException("实体不存在");
            }

            await _entityService.DeleteAsync(entityId);
        }

        /// <summary>
        /// 软删除实体
        /// </summary>
        /// <param name="entityId"></param>
        /// <exception cref="EventException"></exception>
        [HttpDelete("{entityId:int}/soft/delete")]
        public virtual async void SoftDeleteAsync(int entityId)
        {
            var entity = await _entityService.FindAsync(entityId);
            if (entity == null)
            {
                throw new EventException("实体不存在");
            }

            await _entityService.SoftDeleteAsync(entity);
        }
    }
}