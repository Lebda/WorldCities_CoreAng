using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorldCities.Implementations.Contracts;
using WorldCities.Models.Models;
using WorldCities.Models.RequestFeatures;
using WorldCities.Models.ResponseFeatures;

namespace WorldCities.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IRepositoryManager repository;

        public CountriesController(IRepositoryManager repository)
        {
            this.repository = repository;
        }

        // GET: api/Countries GetCountries
        [HttpGet]
        public async Task<ActionResult<ApiResult<Country>>> GetCountries(
            int? pageIndex,
            int? pageSize,
            string sortColumn,
            string sortOrder,
            string filterColumn,
            string filterQuery)
        {
            CountryRequestParameters requestParameters = new(new QueryMetaData(500)
            {
                IsZeroBase = true,
                PageSize = pageSize ?? 10,
                PageIndex = pageIndex ?? 0,
                SortColumn = sortColumn,
                SortOrder = sortOrder,
                FilterColumn = filterColumn,
                FilterQuery = filterQuery
            });
            return await GetCountryInternal(requestParameters);
        }

        private async Task<ActionResult<ApiResult<Country>>> GetCountryInternal(CountryRequestParameters requestParameters)
        {
            var pagedList = await repository.Country.GetAllParamsAsync(requestParameters, false);

            return Ok(new ApiResult<Country>(pagedList));
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            var item = await repository.Country.GetAsync(id, false);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCountry(int id, Country country)
        //{
        //    if (id != country.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(country).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CountryExists(id))
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

        //// POST: api/Countries
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Country>> PostCountry(Country country)
        //{
        //    _context.Countries.Add(country);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        //}

        //// DELETE: api/Countries/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCountry(int id)
        //{
        //    var country = await _context.Countries.FindAsync(id);
        //    if (country == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Countries.Remove(country);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool CountryExists(int id)
        //{
        //    return _context.Countries.Any(e => e.Id == id);
        //}
    }
}
