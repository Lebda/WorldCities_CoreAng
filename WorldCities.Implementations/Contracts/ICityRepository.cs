using System.Threading.Tasks;
using WorldCities.Models.Models;
using WorldCities.Models.RequestFeatures;

namespace WorldCities.Implementations.Contracts
{
    public interface ICityRepository : IEntityRepository<City, int>
    {
        Task<PagedList<City>> GetAllParamsAsync(CityRequestParameters requestParameters, bool trackChanges);
        Task<bool> IsDupeCityAsync(City city);
    }
}
