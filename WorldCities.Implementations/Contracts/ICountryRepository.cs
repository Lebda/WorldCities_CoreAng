using System;
using System.Threading.Tasks;
using WorldCities.Models.Dto;
using WorldCities.Models.Models;
using WorldCities.Models.RequestFeatures;

namespace WorldCities.Implementations.Contracts
{
    public interface ICountryRepository : IEntityRepository<Country, int>
    {
        Task<PagedList<CountryDto>> GetAllParamsAsync(CountryRequestParameters requestParameters, bool trackChanges);
        Task<bool> IsDupeFieldAsync(int countryId, string fieldName, string fieldValue);
    }
}
