using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WorldCities.Implementations.Contracts;
using WorldCities.Models;

namespace WorldCities.Implementations.Repository
{
    internal class RepositoryManager : IRepositoryManager
    {
        private readonly WorldCitiesDbContext repositoryContext;
        private ICountryRepository? countryRepository;
        private ICityRepository? cityRepository;

        public RepositoryManager(WorldCitiesDbContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        public void ConfigureRepositoryManager(IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public ICountryRepository Country
        {
            get { return countryRepository ??= new CountryRepository(repositoryContext); }
        }

        public ICityRepository City
        {
            get { return cityRepository ??= new CityRepository(repositoryContext); }
        }

        public Task SaveAsync() => repositoryContext.SaveChangesAsync();
    }
}
