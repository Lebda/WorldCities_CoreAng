using WorldCities.Models.Models;

namespace WorldCities.Implementations.Contracts
{
    public interface ICountryRepository : IEntityRepository<Country, int>
    {
    }
}
