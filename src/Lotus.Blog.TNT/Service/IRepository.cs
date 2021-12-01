using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Lotus.Blog.TNT.Data.Entity;

namespace Lotus.Blog.TNT.Service
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> InsertAsync(T entity);

        Task<bool> UpdateAsync(T entity);

        Task<bool> DeleteAsync(int id);

        Task DeleteAsync(Expression<Func<T, bool>> where);

        Task<T> FindAsync(int id, string includePath = "");

        T MasterFind(int id);

        IQueryable<T> Find(Expression<Func<T, bool>> where, string includePath = "");

        IQueryable<T> Find<TKey>(Expression<Func<T, bool>> where, Expression<Func<T, TKey>> sort,
            string includePath = "");

        T FindOne(Expression<Func<T, bool>> where, string includePath = "");

        Task<T> FindOneAsync(Expression<Func<T, bool>> where, string includePath = "");
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
    }
}
