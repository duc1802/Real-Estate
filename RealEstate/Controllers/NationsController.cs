using RealEstate.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Data;
using RealEstate.Models.Domain;
using Microsoft.EntityFrameworkCore;
using RealEstate.Repositories;
using AutoMapper;
using RealEstate.CustomActionFilters;
using Microsoft.AspNetCore.Authorization;

namespace RealEstate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationsController : ControllerBase
    {
        private readonly RealEstateDbContext dbContext;
        private readonly INationRepository nationRepository;
        private readonly IMapper mapper;

        public NationsController(RealEstateDbContext dbContext, INationRepository nationRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.nationRepository = nationRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var nationsDomain = await nationRepository.GetAllAsync();

            return Ok(mapper.Map<List<NationDto>>(nationsDomain));
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id) 
        {
            var nationDomain = await nationRepository.GetByIdAsync(id);
            if (nationDomain == null)
            {
                return NotFound();
            }  
             
            return Ok(mapper.Map<NationDto>(nationDomain));  
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddNationRequestDto addNationRequestDto) 
        {
            
                var nationDomainModel = mapper.Map<Nation>(addNationRequestDto);
                nationDomainModel = await nationRepository.CreateAsync(nationDomainModel);


                var nationDto = mapper.Map<NationDto>(nationDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = nationDto.Id }, nationDto);
            
        }

        [HttpPut]
        [Route("{id:int}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateNationRequestDto updateNationRequestDto)
        {
            
                var nationDomainModel = mapper.Map<Nation>(updateNationRequestDto);
                nationDomainModel = await nationRepository.UpdateAsync(id, nationDomainModel);
                if (nationDomainModel == null)
                {
                    return NotFound();
                }

                return Ok(mapper.Map<NationDto>(nationDomainModel));
            
            
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var nationDomainModel = await nationRepository.DeleteAsync(id);
            if (nationDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<NationDto>(nationDomainModel));

            
        }
    }
}
