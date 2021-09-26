using System.Threading.Tasks;
using WorldCities.Models.Models;
using WorldCities.Models.RequestFeatures;

namespace WorldCities.Implementations.Contracts
{
    public interface ICountryRepository : IEntityRepository<Country, int>
    {
        Task<PagedList<Country>> GetAllParamsAsync(CountryRequestParameters requestParameters, bool trackChanges);
        Task<bool> IsDupeFieldAsync(int countryId, string fieldName, string fieldValue);
    }
}
