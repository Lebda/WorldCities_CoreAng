using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldCities.Implementations.Contracts
{
    public interface IEntityRepository<TEntity, in TId>
     where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges);
        Task<TEntity?> GetAsync(TId companyId, bool trackChanges);
        Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<TId> ids, bool trackChanges);
        Task CreateEntityAsync(TEntity company);
        void DeleteEntity(TEntity company);
        Task<int> CountAsync();
    }
}
