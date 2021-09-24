using System.Threading.Tasks;

namespace WorldCities.Implementations.Contracts
{
    public interface IDbSeeder
    {
        Task<string> SeedAsync();
    }
}
