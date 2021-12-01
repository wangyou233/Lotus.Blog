using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Lotus.Blog.TNT.Data.Entity;
using Lotus.Blog.TNT.Data.Ext;
using Lotus.Blog.TNT.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Lotus.Blog.TNT.Service
{
    public class BaseRepository<T, TDbRepository> : IRepository<T>
        where T : Entity
        where TDbRepository : IBaseDbRepository
    {

        protected BaseRepository(TDbRepository repo)
        {
            Repository = repo;
        }
        private TDbRepository Repository { get; }
        protected DbContext MasterDb => Repository.MasterDb;

        protected DbContext SlaveDb => Repository.SlaveDb;

        public async Task<T> InsertAsync(T entity)
        {
            if (entity == null)
            {
                return null;
            }
            await MasterDb.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                return false;
            }
            MasterDb.Set<T>().Attach(entity);

            await MasterDb.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var model = await FindAsync(id);
            if (model == null)
            {
                return false;
            }

            MasterDb.Remove(model);


            await MasterDb.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> @where)
        {
            var list = Find(where);
            foreach (var item in list)
            {
                MasterDb.Remove(item);
            }

            await MasterDb.SaveChangesAsync();

        }

        public async Task<T> FindAsync(int id, string includePath = "")
        {
            if (!includePath.IsNullOrEmpty())
            {
                return await FindOneAsync(i => i.Id == id, includePath);
            }

            return await SlaveDb.Set<T>().FindAsync(id);
        }

        public T MasterFind(int id)
        {
            Expression<Func<T, bool>> where = entity => entity.Id == id;


            return MasterDb.Set<T>().Where(where).FirstOrDefault();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> @where, string includePath = "")
        {
            var query = SlaveDb.Set<T>().Where(where);

            if (!includePath.IsNullOrEmpty())
            {
                query = query.Include(includePath);
            }

            return query;
        }

        public IQueryable<T> Find<TKey>(Expression<Func<T, bool>> @where, Expression<Func<T, TKey>> sort, string includePath = "")
        {
            var query = SlaveDb.Set<T>().Where(where);

            if (sort != null)
            {
                query = query.OrderByDescending(sort);
            }

            if (!includePath.IsNullOrEmpty())
            {
                query = query.Include(includePath);
            }

            return query;
        }

        public T FindOne(Expression<Func<T, bool>> @where, string includePath = "")
        {
            return Find(where, includePath).SingleOrDefault();
        }

        public async Task<T> FindOneAsync(Expression<Func<T, bool>> @where, string includePath = "")
        {
            return await Find(where, includePath).SingleOrDefaultAsync();
        }

        public async Task<bool> BatchInsertAsync(IEnumerable<T> entityList)
        {
            await MasterDb.Set<T>().AddRangeAsync(entityList);
            await MasterDb.SaveChangesAsync();
            return true;

        }

        public async Task<bool> BatchUpdateAsync(IEnumerable<T> entityList)
        {

            MasterDb.Set<T>().AttachRange(entityList);

            await MasterDb.SaveChangesAsync();
            return true;
        }
    }
}
