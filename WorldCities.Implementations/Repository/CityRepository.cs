using System;
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
    internal class CityRepository : RepositoryBase<City>, ICityRepository
    {
        public CityRepository(WorldCitiesDbContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<PagedList<City>> GetAllParamsAsync(
            CityRequestParameters requestParameters,
            bool trackChanges)
        {
            var pagedList = await FindAll(trackChanges)
                .OrderBy(e => e.Name)
                //.FilterEmployees(employeeParameters.MinAge, employeeParameters.MaxAge)
                //.Search(employeeParameters.SearchTerm)
                //.Sort(employeeParameters.OrderBy)
                .ToPagedListAsync(requestParameters.QueryMetaData);
            return pagedList;
        }

        public async Task<IEnumerable<City>> GetAllAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToListAsync();

        public async Task<City?> GetAsync(int companyId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(companyId), trackChanges)
                .SingleOrDefaultAsync();

        public async Task<IEnumerable<City>> GetByIdsAsync(IEnumerable<int> ids, bool
            trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges)
                .ToListAsync();

        public async Task CreateEntityAsync(City company) => await CreateAsync(company);
        public void DeleteEntity(City company) => Delete(company);
    }
}
