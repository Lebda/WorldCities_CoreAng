using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorldCities.Implementations.Contracts;
using WorldCities.Models;

namespace WorldCities.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

        public static void ConfigureSqlContext(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<WorldCitiesDbContext>(opts =>
             {
                 opts.EnableDetailedErrors(true);
                 opts.EnableSensitiveDataLogging(true);
                 opts.UseNpgsql(configuration.GetConnectionString("PostgreSqlConnection"), b => b.MigrationsAssembly("WorldCities"));
             });
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.ConfigureImplementations();
    }
}
