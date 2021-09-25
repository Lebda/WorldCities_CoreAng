using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorldCities.Implementations.Contracts;
using WorldCities.Models.Dto;
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
        private readonly IMapper mapper;

        public CitiesController(
            IRepositoryManager repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        // GET: api/Cities
        [HttpGet(Name = "GetCities")]
        public async Task<ActionResult<ApiResult<City>>> GetCities(
            int? pageIndex,
            int? pageSize,
            string sortColumn,
            string sortOrder,
            string filterColumn,
            string filterQuery)
        {
            CityRequestParameters requestParameters = new(new QueryMetaData()
            {
                IsZeroBase = true,
                PageSize = pageSize ?? 10,
                PageIndex = pageIndex ?? 0 ,
                SortColumn = sortColumn,
                SortOrder = sortOrder,
                FilterColumn = filterColumn,
                FilterQuery = filterQuery
            });
            return await GetCitiesInternal(requestParameters);
        }

        private async Task<ActionResult<ApiResult<City>>> GetCitiesInternal(CityRequestParameters requestParameters)
        {
            var pagedList = await repository.City.GetAllParamsAsync(requestParameters, false);

            return Ok(new ApiResult<City>(pagedList));
        }

        // GET: api/Cities/5
        [HttpGet("{id:int}", Name = "CityById")]
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
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCity(int id, CityForUpdateDto company)
        {
            if (company == null)
            {
                //logger.LogError("CompanyForUpdateDto object sent from client is null.");
                return BadRequest("CompanyForUpdateDto object is null");
            }

            var companyEntity = await repository.City.GetAsync(id, trackChanges: true);
            if (companyEntity == null)
            {
                //logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            mapper.Map(company, companyEntity);
            await repository.SaveAsync();

            return NoContent();
        }

        //POST: api/Cities
        [HttpPost(Name = "CreateCity")]
        public async Task<IActionResult> PostCity(CityForCreateDto dto)
        {
            if (dto == null)
            {
                //logger.LogError("CompanyForCreationDto object sent from client is null.");
                return BadRequest("dto object is null");
            }

            var entity = mapper.Map<City>(dto);

            await repository.City.CreateEntityAsync(entity);
            await repository.SaveAsync();

            var entityToReturn = mapper.Map<CityDto>(entity);

            return CreatedAtRoute("CityById", new { id = entityToReturn.Id },
                entityToReturn);
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
