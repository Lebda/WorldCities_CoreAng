using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorldCities.Models;

namespace WorldCities.Implementations.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected WorldCitiesDbContext RepositoryContext;

        protected RepositoryBase(WorldCitiesDbContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public Task<int> CountAsync()
        {
            return RepositoryContext.Set<T>().CountAsync();
        }

        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ?
                RepositoryContext.Set<T>().AsNoTracking() :
                RepositoryContext.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
            bool trackChanges) =>
            !trackChanges ?
                RepositoryContext.Set<T>()
                    .Where(expression)
                    .AsNoTracking() :
                RepositoryContext.Set<T>()
                    .Where(expression);

        public async Task CreateAsync(T entity) => await RepositoryContext.Set<T>().AddAsync(entity);

        public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);

        public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
    }
}
