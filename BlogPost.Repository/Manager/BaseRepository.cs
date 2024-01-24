using BlogPost.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Repository.Manager
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected DbContext dbContext;
        private readonly DbSet<T> dbSet;
        public int changeCount;

        public BaseRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = this.dbContext.Set<T>();
        }

        public async Task<bool> AddAsync(T entity)
        {
            dbSet.Add(entity);
            dbContext.Entry(entity).State = EntityState.Added;
            return await SaveAsync();
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            dbSet.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            dbContext.Entry(entity).State = EntityState.Deleted;
            return await SaveAsync();
        }

        public T Get(Expression<Func<T, bool>> filter = null)
        {
            return dbSet.AsQueryable().FirstOrDefault(filter);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null)
        {
            return await Task.FromResult(dbSet.AsQueryable().FirstOrDefault(filter));
        }

        public List<T> GetList(Expression<Func<T, bool>> filter = null)
        {
            return filter == null ? dbSet.AsQueryable().ToList() : dbSet.AsQueryable().Where(filter).ToList();
        }


        public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            return await Task.FromResult(this.dbSet.Where(filter).AsQueryable<T>());
        }

        public async Task<bool> SaveAsync()
        {
            changeCount = await this.dbContext.SaveChangesAsync();
            return changeCount > 0;
        }
    }
}
