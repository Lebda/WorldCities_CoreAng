using System.Threading.Tasks;
using WorldCities.Models.Dto;
using WorldCities.Models.Models;
using WorldCities.Models.RequestFeatures;

namespace WorldCities.Implementations.Contracts
{
    public interface ICityRepository : IEntityRepository<City, int>
    {
        Task<PagedList<CityDto>> GetAllParamsAsync(CityRequestParameters requestParameters, bool trackChanges);
        Task<bool> IsDupeCityAsync(City city);
    }
}
