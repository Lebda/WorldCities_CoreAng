using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorldCities.Implementations.Contracts;
using WorldCities.Models;
using WorldCities.Models.Models;
using WorldCities.Models.RequestFeatures;
using WorldCities.Models.ResponseFeatures;

namespace WorldCities.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly IRepositoryManager repository;

        public CitiesController(
            WorldCitiesDbContext context, 
            IRepositoryManager repository)
        {
            this.repository = repository;
        }

        // GET: api/Cities
        [HttpGet(Name = "GetCities")]
        public async Task<ActionResult<ApiResult<City>>> GetCities(
            int? pageIndex,
            int? pageSize)
        {
            CityRequestParameters requestParameters = new(new QueryMetaData()
            {
                IsZeroBase = true,
                PageSize = pageSize ?? 10,
                PageIndex = pageIndex ?? 0   
            });
            var pagedList = await repository.City.GetAllParamsAsync(requestParameters, false);

            return Ok(new ApiResult<City>(pagedList));
        }

        // GET: api/Cities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {
            var city = await repository.City.GetAsync(id, false);

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        // PUT: api/Cities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCity(int id, City city)
        //{
        //    if (id != city.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(city).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CityExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Cities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<City>> PostCity(City city)
        //{
        //    _context.Cities.Add(city);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetCity", new { id = city.Id }, city);
        //}

        // DELETE: api/Cities/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCity(int id)
        //{
        //    var city = await _context.Cities.FindAsync(id);
        //    if (city == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Cities.Remove(city);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool CityExists(int id)
        //{
        //    return _context.Cities.Any(e => e.Id == id);
        //}
    }
}
