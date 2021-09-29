using System.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using WorldCities.Implementations.Contracts;
using WorldCities.Models.Models;

namespace WorldCities.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly IDbSeeder seeder;
        private readonly IWebHostEnvironment env;

        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public SeedController(
            IWebHostEnvironment env, 
            IDbSeeder seeder, 
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            this.env = env;
            this.seeder = seeder;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult> CreateDefaultUsers()
        {
            return Ok(await seeder.CreateDefaultUsersAsync());
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
