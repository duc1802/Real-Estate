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
    public class PackagesController : ControllerBase
    {
        private readonly RealEstateDbContext dbContext;
        private readonly IPackageRepository packageRepository;
        private readonly IMapper mapper;

        public PackagesController(RealEstateDbContext dbContext, IPackageRepository packageRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.packageRepository = packageRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var packagesDomain = await packageRepository.GetAllAsync();

            return Ok(mapper.Map<List<PackageDto>>(packagesDomain));
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var packageDomain = await packageRepository.GetByIdAsync(id);
            if (packageDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<PackageDto>(packageDomain));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddPackageRequestDto addPackageRequestDto)
        {

            var packageDomainModel = mapper.Map<Package>(addPackageRequestDto);
            packageDomainModel = await packageRepository.CreateAsync(packageDomainModel);


            var packageDto = mapper.Map<PackageDto>(packageDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = packageDto.Id }, packageDto);

        }

        [HttpPut]
        [Route("{id:int}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePackageRequestDto updatePackageRequestDto)
        {

            var packageDomainModel = mapper.Map<Package>(updatePackageRequestDto);
            packageDomainModel = await packageRepository.UpdateAsync(id, packageDomainModel);
            if (packageDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<PackageDto>(packageDomainModel));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var packageDomainModel = await packageRepository.DeleteAsync(id);
            if (packageDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<PackageDto>(packageDomainModel));


        }
    }
}
