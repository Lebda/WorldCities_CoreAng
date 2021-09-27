using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorldCities.Implementations.Contracts;
using WorldCities.Implementations.RequestFeatures;
using WorldCities.Models;
using WorldCities.Models.Dto;
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

        public async Task<PagedList<CityDto>> GetAllParamsAsync(
            CityRequestParameters requestParameters,
            bool trackChanges)
        {
            var pagedList = await FindAll(trackChanges)
                .OrderBy(e => e.Name)
                .Filter(requestParameters)
                //.Search(employeeParameters.SearchTerm)
                .Sort(requestParameters)
                .Select(c => new CityDto()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Lat = c.Lat,
                    Lon = c.Lon,
                    CountryId = c.Country.Id,
                    CountryName = c.Country.Name
                })
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

        public async Task<bool> IsDupeCityAsync(City city) =>
        await FindByCondition(e => 
        e.Name == city.Name
        && e.Lat == city.Lat
        && e.Lon == city.Lon
        && e.CountryId == city.CountryId
        && e.Id != city.Id, false)
        .CountAsync() > 0;
    }
}
