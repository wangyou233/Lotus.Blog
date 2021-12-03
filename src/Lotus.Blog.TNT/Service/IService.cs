using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Lotus.Blog.TNT.Data.Entity;
using Lotus.Blog.TNT.Web;

namespace Lotus.Blog.TNT.Service
{
    public interface IService<T> where T : BaseEntity
    {
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        IQueryable<T> FindAll();

        /// <summary>
        /// 插入一个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="save"></param>
        /// <returns></returns>
        T Insert(T entity, bool save = true);


        /// <summary>
        /// 异步插入
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="save"></param>
        /// <returns></returns>
        Task<T> InsertAsync(T entity, bool save = true);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="save"></param>
        /// <returns></returns>
        bool Update(T entity, bool save = true);
        /// <summary>
        /// 异步更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="save"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T entity, bool save = true);


        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="save"></param>
        /// <returns></returns>
        bool Delete(int id, bool save = true);
        /// <summary>
        /// 异步删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="save"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(int id, bool save = true);

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> SoftDeleteAsync(T entity);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="where"></param>
        /// <param name="save"></param>
        void Delete(Expression<Func<T, bool>> where, bool save = true);
        /// <summary>
        /// 异步删除实体
        /// </summary>
        /// <param name="where"></param>
        /// <param name="save"></param>
        /// <returns></returns>
        Task DeleteAsync(Expression<Func<T, bool>> where, bool save = true);

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool SoftDelete(int id);
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool SoftDelete(T entity);
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void SoftDelete(Expression<Func<T, bool>> where);

        /// <summary>
        /// 查找一个对象
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includePath"></param>
        /// <returns></returns>
        T Find(int id, string includePath = "");
        /// <summary>
        /// 异步查找对象
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includePath"></param>
        /// <returns></returns>
        Task<T> FindAsync(int id, string includePath = "");

        /// <summary>
        /// 主库查询实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T MasterFind(int id);

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="where"></param>
        /// <param name="includePath"></param>
        /// <returns></returns>
        IQueryable<T> Find(Expression<Func<T, bool>> where, string includePath = "");
        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="where"></param>
        /// <param name="includePath"></param>
        /// <returns></returns>
        IQueryable<T> Find<TKey>(Expression<Func<T, bool>> where, Expression<Func<T, TKey>> sort,
            string includePath = "");

        /// <summary>
        /// 查询一个实体
        /// </summary>
        /// <param name="where"></param>
        /// <param name="includePath"></param>
        /// <returns></returns>
        T FindOne(Expression<Func<T, bool>> where, string includePath = "");
        /// <summary>
        /// 异步查询
        /// </summary>
        /// <param name="where"></param>
        /// <param name="includePath"></param>
        /// <returns></returns>
        Task<T> FindOneAsync(Expression<Func<T, bool>> where, string includePath = "");

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageObject"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<PageList<T>> FindPage(PageObjectModel pageObject, Expression<Func<T, bool>> where);

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageObject"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<PageList<T>> FindPage<TKey>(PageObjectModel pageObject, Expression<Func<T, bool>> where,
            Expression<Func<T, TKey>> sort);


        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        Task<bool> BatchInsertAsync(IEnumerable<T> entityList);
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        Task<bool> BatchUpdateAsync(IEnumerable<T> entityList);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageObject"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="includePath"></param>
        /// <returns></returns>
        Task<PageList<T>> FindPageAsync<TKey>(PageObjectModel pageObject,
            Expression<Func<T, bool>> where,
            Expression<Func<T, TKey>> sort,
            string includePath);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageObject"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="includePaths"></param>
        /// <returns></returns>
        Task<PageList<T>> FindPageAsync<TKey>(PageObjectModel pageObject,
            Expression<Func<T, bool>> where,
            Expression<Func<T, TKey>> sort,
            IList<string> includePaths);
    }
}
