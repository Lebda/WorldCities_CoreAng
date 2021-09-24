using Microsoft.EntityFrameworkCore;
using WorldCities.Models.Models;

namespace WorldCities.Models
{
    public class WorldCitiesDbContext : DbContext
    {
        public WorldCitiesDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}
