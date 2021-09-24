using Microsoft.Extensions.DependencyInjection;
using WorldCities.Implementations.Repository;

namespace WorldCities.Implementations.Contracts
{
    public static class Configuration
    {
        public static void ConfigureImplementations(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IDbSeeder, DbSeeder>();
        }
    }
}
