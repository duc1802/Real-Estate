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
    public class WardsController : ControllerBase
    {
        private readonly RealEstateDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IWardRepository wardRepository;

        public WardsController(RealEstateDbContext dbContext, IMapper mapper, IWardRepository wardRepository)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.wardRepository = wardRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool IsAscending)
        {
            var wardDomainModel = await wardRepository.GetAllAsync(filterOn, filterQuery, sortBy, IsAscending);
            return Ok(mapper.Map<List<WardDto>>(wardDomainModel));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddDistrictRequestDto addDistrictRequestDto)
        {

            var wardDomainModel = mapper.Map<Ward>(addDistrictRequestDto);
            await wardRepository.CreateAsync(wardDomainModel);

            return Ok(mapper.Map<WardDto>(wardDomainModel));

        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var wardDomainModel = await wardRepository.GetByIdAsync(id);
            if (wardDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WardDto>(wardDomainModel));
        }

        [HttpPut]
        [Route("{id:int}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateWardRequestDto updateWardRequestDto)
        {

            var wardDomainModel = mapper.Map<Ward>(updateWardRequestDto);
            wardDomainModel = await wardRepository.UpdateAsync(id, wardDomainModel);

            if (wardDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WardDto>(wardDomainModel));


        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deleteWardDomainModel = await wardRepository.DeleteAsync(id);
            if (deleteWardDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WardDto>(deleteWardDomainModel));
        }
    }
}
