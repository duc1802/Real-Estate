using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.CustomActionFilters;
using RealEstate.Models.Domain;
using RealEstate.Models.DTO;
using RealEstate.Repositories;

namespace RealEstate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IDistrictRepository districtRepository;

        public DistrictsController(IMapper mapper, IDistrictRepository districtRepository)
        {
            this.mapper = mapper;
            this.districtRepository = districtRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool IsAscending)
        {
            var districtDomainModel = await districtRepository.GetAllAsync(filterOn, filterQuery, sortBy, IsAscending);
            return Ok(mapper.Map<List<DistrictDto>>(districtDomainModel));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddDistrictRequestDto addDistrictRequestDto)
        {

            var districtDomainModel = mapper.Map<District>(addDistrictRequestDto);
            await districtRepository.CreateAsync(districtDomainModel);

            return Ok(mapper.Map<DistrictDto>(districtDomainModel));


        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var districtDomainModel = await districtRepository.GetByIdAsync(id);
            if (districtDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<DistrictDto>(districtDomainModel));
        }

        [HttpPut]
        [Route("{id:int}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateDistrictRequestDto updateDistrictRequestDto)
        {

            var districtDomainModel = mapper.Map<District>(updateDistrictRequestDto);
            districtDomainModel = await districtRepository.UpdateAsync(id, districtDomainModel);

            if (districtDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<DistrictDto>(districtDomainModel));


        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deleteDistrictDomainModel = await districtRepository.DeleteAsync(id);
            if (deleteDistrictDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<DistrictDto>(deleteDistrictDomainModel));
        }
    }
}
