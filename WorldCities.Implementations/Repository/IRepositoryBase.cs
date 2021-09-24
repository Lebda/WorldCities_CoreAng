using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WorldCities.Implementations.Repository
{
    internal interface IRepositoryBase<T>
    where T : class
    {
        Task<int> CountAsync();
        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
