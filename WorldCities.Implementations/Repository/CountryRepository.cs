using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorldCities.Implementations.Contracts;
using WorldCities.Implementations.RequestFeatures;
using WorldCities.Models;
using WorldCities.Models.Models;
using WorldCities.Models.RequestFeatures;

namespace WorldCities.Implementations.Repository
{
    internal class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        public CountryRepository(WorldCitiesDbContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<PagedList<Country>> GetAllParamsAsync(
                CountryRequestParameters requestParameters,
                bool trackChanges)
        {
            var pagedList = await FindAll(trackChanges)
                .OrderBy(e => e.Name)
                .Filter(requestParameters)
                //.Search(employeeParameters.SearchTerm)
                .Sort(requestParameters)
                .ToPagedListAsync(requestParameters.QueryMetaData);
            return pagedList;
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
