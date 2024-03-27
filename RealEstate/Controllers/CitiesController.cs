using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.CustomActionFilters;
using RealEstate.Data;
using RealEstate.Models.Domain;
using RealEstate.Models.DTO;
using RealEstate.Repositories;

namespace RealEstate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly RealEstateDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ICityRespository cityRespository;

        public CitiesController(RealEstateDbContext dbContext, IMapper mapper, ICityRespository cityRespository)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.cityRespository = cityRespository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool IsAscending)
        {
            var cityDomainModel =  await cityRespository.GetAllAsync(filterOn, filterQuery, sortBy, IsAscending);
            return Ok(mapper.Map<List<CityDto>>(cityDomainModel));
        }

        [HttpPost]
        [ValidateModel]
      
        public async Task<IActionResult> Create([FromBody] AddCityRequestDto addCityRequestDto)
        {
            
                var cityDomainmodel = mapper.Map<City>(addCityRequestDto);
                await cityRespository.CreateAsync(cityDomainmodel);

                return Ok(mapper.Map<CityDto>(cityDomainmodel));
            
            
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var cityDomainModel = await cityRespository.GetByIdAsync(id);
            if(cityDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<CityDto>(cityDomainModel));
        }

        [HttpPut]
        [Route("{id:int}")]
        [ValidateModel]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateCityRequestDto updateCityRequestDto)
        {
            
                var cityDomainModel = mapper.Map<City>(updateCityRequestDto);
                cityDomainModel = await cityRespository.UpdateAsync(id, cityDomainModel);

                if (cityDomainModel == null)
                {
                    return NotFound();
                }
                return Ok(mapper.Map<CityDto>(cityDomainModel));
            
            
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deleteCityDomainModel = await cityRespository.DeleteAsync(id);
            if(deleteCityDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<CityDto>(deleteCityDomainModel));
        }
    }
}
