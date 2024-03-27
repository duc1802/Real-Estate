using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.CustomActionFilters;
using RealEstate.Data;
using RealEstate.Models.Domain;
using RealEstate.Models.DTO;
using RealEstate.Repositories;

namespace RealEstate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreetsController : ControllerBase
    {
        private readonly RealEstateDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IStreetRepository streetRepository;

        public StreetsController(RealEstateDbContext dbContext, IMapper mapper, IStreetRepository streetRepository)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.streetRepository = streetRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool IsAscending)
        {
            var streetDomainModel = await streetRepository.GetAllAsync(filterOn, filterQuery, sortBy, IsAscending);
            return Ok(mapper.Map<List<StreetDto>>(streetDomainModel));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddStreetRequestDto addStreetRequestDto)
        {

            var streetDomainModel = mapper.Map<Street>(addStreetRequestDto);
            await streetRepository.CreateAsync(streetDomainModel);

            return Ok(mapper.Map<StreetDto>(streetDomainModel));

        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var streetDomainModel = await streetRepository.GetByIdAsync(id);
            if (streetDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<StreetDto>(streetDomainModel));
        }

        [HttpPut]
        [Route("{id:int}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateStreetRequestDto updateStreetRequestDto)
        {

            var streetDomainModel = mapper.Map<Street>(updateStreetRequestDto);
            streetDomainModel = await streetRepository.UpdateAsync(id, streetDomainModel);

            if (streetDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<StreetDto>(streetDomainModel));


        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deleteStreetDomainModel = await streetRepository.DeleteAsync(id);
            if (deleteStreetDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<StreetDto>(deleteStreetDomainModel));
        }
    }
}
