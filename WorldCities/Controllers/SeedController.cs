using System.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using WorldCities.Implementations.Contracts;

namespace WorldCities.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly IDbSeeder seeder;
        private readonly IWebHostEnvironment env;

        public SeedController(
            IWebHostEnvironment env, IDbSeeder seeder)
        {
            this.env = env;
            this.seeder = seeder;
        }

        [HttpGet]
        public async Task<ActionResult> Import()
        {
            if (!env.IsDevelopment())
            {
                throw new SecurityException("Not allowed");
            }

            return Ok(await seeder.SeedAsync());
        }
    }
}
