using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TheCompany.Domain.Shared.Common.Repository
{
    public interface IRepositoryBase<T> : IDisposable
    {
        Task<IReadOnlyList<T>> FindAllAsync();
        Task<IReadOnlyList<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
        Task<T> FindOneByConditionAsync(Expression<Func<T, bool>> expression);
        Task<int> CreateAsync(T entity);
        int Update(T entity);
        int Delete(T entity);

        Task<int> CreateWithSaveAsync(T entity);
        Task<int> UpdateWithSaveAsync(T entity);
        Task<int> DeleteWithSaveAsync(T entity);

        Task<int> SaveAsync();
    }
}
