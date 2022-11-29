using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Lotus.Blog.TNT.Data.Entity;
using Lotus.Blog.TNT.Data.Ext;
using Lotus.Blog.TNT.Data.Repository;
using Lotus.Blog.TNT.Ext;
using Lotus.Blog.TNT.Web;
using Microsoft.EntityFrameworkCore;

namespace Lotus.Blog.TNT.Service
{
    public abstract class BaseService<T, TDbRepository> : IService<T>
        where T : BaseEntity
        where TDbRepository : IBaseDbRepository
    {
        protected BaseService(TDbRepository repo)
        {
            Repository = repo;
        }
        private TDbRepository Repository { get; }
        protected DbContext MasterDb => Repository.MasterDb;

        protected DbContext SlaveDb => Repository.SlaveDb;
        public virtual bool Delete(int id, bool save = true)
        {
            return DeleteAsync(id, save).Result;
        }

        public async Task<bool> DeleteAsync(int id, bool save = true)
        {
            var model = await FindAsync(id);
            if (model == null)
            {
                return false;
            }

            MasterDb.Remove(model);

            if (save)
            {
                await MasterDb.SaveChangesAsync();
            }
            return true;
        }

        public void Delete(Expression<Func<T, bool>> where, bool save = true)
        {
            DeleteAsync(where, save).Wait();
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> where, bool save = true)
        {
            var list = Find(where);
            foreach (var item in list)
            {
                MasterDb.Remove(item);
            }

            if (save)
            {
                await MasterDb.SaveChangesAsync();
            }
        }


        public bool SoftDelete(int id)
        {
            var entity = Find(id);
            if (entity == null)
            {
                return false;
            }

            return SoftDelete(entity);
        }

        public async Task<bool> SoftDeleteAsync(T entity)
        {
            entity.IsDeleted = true;
            entity.Deleted = DateTime.Now;
            entity.Version++;
            return await UpdateAsync(entity);
        }

        public bool SoftDelete(T entity)
        {
            if (entity == null)
            {
                return false;
            }
            entity.IsDeleted = true;
            entity.Deleted = DateTime.Now;
            entity.Version++;
            return Update(entity);
        }

        public void SoftDelete(Expression<Func<T, bool>> where)
        {
            foreach (var entity in Find(where))
            {
                SoftDelete(entity);
            }
        }

        public T Find(int id, string includePath = null)
        {
            return FindAsync(id, includePath).Result;
        }

        public async Task<T> FindAsync(int id, string includePath = "")
        {
            if (!includePath.IsNullOrEmpty())
            {
                return await FindOneAsync(i => i.Id == id, includePath);
            }

            return await SlaveDb.Set<T>().FindAsync(id);
        }


        public virtual IQueryable<T> Find(Expression<Func<T, bool>> where, string includePath = "")
        {
            where = AppendDeletedQuery(where);

            var query = SlaveDb.Set<T>().Where(where);

            if (!includePath.IsNullOrEmpty())
            {
                query = query.Include(includePath);
            }

            return query;
        }

        public virtual IQueryable<T> Find<TKey>(Expression<Func<T, bool>> where, Expression<Func<T, TKey>> sort, string includePath = "")
        {
            where = AppendDeletedQuery(where);

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

        public virtual IQueryable<T> FindAll()
        {
            return SlaveDb.Set<T>();
        }

        public virtual T FindOne(Expression<Func<T, bool>> where, string includePath = "")
        {
            return Find(where, includePath).SingleOrDefault();
        }

        public async virtual Task<T> FindOneAsync(Expression<Func<T, bool>> where, string includePath = "")
        {
            return await Find(where, includePath).SingleOrDefaultAsync();
        }

        public virtual async Task<PageList<T>> FindPage(PageObjectModel pageObject, Expression<Func<T, bool>> where)
        {
            return await FindPage(pageObject, where, o => o.Id);
        }

        public virtual async Task<PageList<T>> FindPage<TKey>(PageObjectModel pageObject, Expression<Func<T, bool>> where, Expression<Func<T, TKey>> sort)
        {
            return await FindPageAsync(pageObject, where, sort, "");
        }

        
        public virtual async Task<PageList<T>> FindPageAsync<TKey>(PageObjectModel pageObject,
            Expression<Func<T, bool>> where,
            Expression<Func<T, TKey>> sort,
            IList<string> includePaths)
        {
            where = AppendDeletedQuery(where);
            IQueryable<T> query = SlaveDb.Set<T>();
            if (where != null)
            {
                query = query.Where(where);
            }


            if (includePaths.Count > 0)
            {
                foreach (var i in includePaths)
                {
                    query = query.Include(i);
                }
            }

            if (sort != null)
            {
                query = query.OrderByDescending(sort);
            }

            var rows = new List<T>();
            int total = await query.CountAsync();
            if (total > 0)
            {
                int skipCount = (pageObject.Page - 1) * pageObject.Size;
                rows = await query.Skip(skipCount).Take(pageObject.Size).ToListAsync();
            }
            return new PageList<T>(rows, total, pageObject.Page, pageObject.Size);
        }
        public virtual async Task<PageList<T>> FindPageAsync<TKey>(PageObjectModel pageObject,
            Expression<Func<T, bool>> where,
            Expression<Func<T, TKey>> sort,
            string includePath)
        {
            where = AppendDeletedQuery(where);

            IQueryable<T> query = SlaveDb.Set<T>();
            if (where != null)
            {
                query = query.Where(where);
            }

            if (!string.IsNullOrEmpty(includePath))
            {
                query = query.Include(includePath);
            }

            if (sort != null)
            {
                query = query.OrderByDescending(sort);
            }

            var rows = new List<T>();
            int total = await query.CountAsync();
            if (total > 0)
            {
                int skipCount = (pageObject.Page - 1) * pageObject.Size;
                rows = await query.Skip(skipCount).Take(pageObject.Size).ToListAsync();
            }
            return new PageList<T>(rows, total, pageObject.Page, pageObject.Size);
        }

        public T Insert(T entity, bool save = true)
        {
            return InsertAsync(entity, save).Result;
        }

        public async Task<T> InsertAsync(T? entity, bool save = true)
        {
            if (entity == null)
            {
                return null;
            }

            if (entity.Created == null)
            {
                entity.Created = DateTime.Now;
            }
            if (entity.Modified == null)
            {
                entity.Modified = entity.Created;
            }

            entity.Version = 1;

            MasterDb.Add(entity);

            if (save)
            {
                await MasterDb.SaveChangesAsync();
            }

            return entity;
        }


        public virtual T MasterFind(int id)
        {
            Expression<Func<T, bool>> where = entity => entity.Id == id;

            where = AppendDeletedQuery(where);

            return MasterDb.Set<T>().Where(where).FirstOrDefault();
        }

        public bool Update(T entity, bool save = true)
        {
            return UpdateAsync(entity, save).Result;
        }

        public async Task<bool> UpdateAsync(T? entity, bool save = true)
        {
            if (entity == null)
            {
                return false;
            }

            entity.Modified = DateTime.Now;
            entity.Version += 1;

            MasterDb.Set<T>().Attach(entity);
            MasterDb.Entry(entity).State = EntityState.Modified;

            if (save)
            {
                await MasterDb.SaveChangesAsync();
            }

            return true;
        }

        private Expression<Func<T, bool>> AppendDeletedQuery(Expression<Func<T, bool>> where)
        {
            Expression<Func<T, bool>> w = entity => !entity.IsDeleted;

            if (where == null)
            {
                where = w;
            }
            else
            {
                where = where.And(w);
            }

            return where;
        }


        public async Task<bool> BatchInsertAsync(IEnumerable<T> entityList)
        {
            foreach (var entity in entityList)
            {
                await MasterDb.Set<T>().AddAsync(entity);
            }
            await MasterDb.SaveChangesAsync();
            return true;

        }

        public IQueryable<T> Query()
        {
            return SlaveDb.Set<T>().AsQueryable();
        }
        public void DeleteWhere(Expression<Func<T, bool>> predicate = null) 
        {
            var dbSet = MasterDb.Set<T>();
            if (predicate != null)
                dbSet.RemoveRange(dbSet.Where(predicate));
            else
                dbSet.RemoveRange(dbSet);

            MasterDb.SaveChanges();
        } 
        
        public async Task<bool> BatchUpdateAsync(IEnumerable<T> entityList)
        {
            foreach (var entity in entityList)
            {
                if (entity == null)
                {
                    return false;
                }

                entity.Modified = DateTime.Now;
                entity.Version += 1;

                MasterDb.Set<T>().Attach(entity);
                MasterDb.Entry(entity).State = EntityState.Modified;
            }

            await MasterDb.SaveChangesAsync();
            return true;
        }
    }
}
