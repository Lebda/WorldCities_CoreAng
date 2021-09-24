using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorldCities.Implementations.Contracts;
using WorldCities.Models;
using WorldCities.Models.Models;

namespace WorldCities.Implementations.Repository
{
    internal class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        public CountryRepository(WorldCitiesDbContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Country>> GetAllAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToListAsync();

        public async Task<Country?> GetAsync(int companyId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(companyId), trackChanges)
                .SingleOrDefaultAsync();

        public async Task<IEnumerable<Country>> GetByIdsAsync(IEnumerable<int> ids, bool
            trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges)
                .ToListAsync();

        public async Task CreateEntityAsync(Country company) => await CreateAsync(company);
        public void DeleteEntity(Country company) => Delete(company);
    }
}
