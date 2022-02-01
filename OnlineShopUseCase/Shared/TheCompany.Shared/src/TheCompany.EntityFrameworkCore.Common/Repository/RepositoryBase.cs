using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheCompany.Domain.Shared.Common.Repository;

namespace TheCompany.EntityFrameworkCore.Common.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DbContext RepositoryContext { get; set; }
        protected RepositoryBase(DbContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }

        public async Task<int> CreateAsync(T entity)
        {
            var r = await this.RepositoryContext.Set<T>().AddAsync(entity);
            return r != null ? 1 : 0;
        }

        public async Task<int> CreateWithSaveAsync(T entity)
        {
            var r = await CreateAsync(entity);
            if (r > 0)
                r = await SaveAsync();

            return r;
        }

        public int Update(T entity)
        {
            var r = this.RepositoryContext.Set<T>().Update(entity);
            return r != null ? 1 : 0;
        }

        public async Task<int> UpdateWithSaveAsync(T entity)
        {
            var r = Update(entity);
            if (r > 0)
                r = await SaveAsync();

            return r;
        }

        public int Delete(T entity)
        {
            var r = this.RepositoryContext.Set<T>().Remove(entity);
            return r != null ? 1 : 0;
        }

        public async Task<int> DeleteWithSaveAsync(T entity)
        {
            var r = Delete(entity);
            if (r > 0)
                r = await SaveAsync();

            return r;
        }

        public async Task<IReadOnlyList<T>> FindAllAsync()
        {
            return await GetAllItemsAsIQuerable().ToListAsync();
        }

        public async Task<T> FindOneByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await GetAllItemsAsIQuerable().Where(expression).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await GetAllItemsAsIQuerable().Where(expression).ToListAsync<T>();
        }

        protected IQueryable<T> GetAllItemsAsIQuerable()
        {
            return this.RepositoryContext.Set<T>().AsQueryable().AsNoTracking();
        }

        public async Task<int> SaveAsync()
        {
            return await this.RepositoryContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.RepositoryContext != null)
            {
                this.RepositoryContext.Dispose();
                this.RepositoryContext = null;
            } 
        }
    }
}
