using System.Threading.Tasks;

namespace WorldCities.Implementations.Contracts
{
    public interface IRepositoryManager
    {
        ICountryRepository Country { get; }
        ICityRepository City { get; }
        Task SaveAsync();
    }
}
