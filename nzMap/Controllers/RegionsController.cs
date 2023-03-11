using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using nzMap.Model.Domain;
using nzMap.Model.DTO;
using nzMap.Repositories;

namespace nzMap.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {

            var regions = await regionRepository.GetAll();
            if (regions == null)
            {
                return NotFound();
            }
            // return DTO region
            List<Model.DTO.Region> regionsDto = new List<Model.DTO.Region>();
            regions.ToList().ForEach(region =>
            {
                var regionDto = new Model.DTO.Region()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    Area = region.Area,
                    Lat = region.Lat,
                    Long = region.Long,
                    Population = region.Population

                };
                regionsDto.Add(regionDto);
            });
            return Ok(regionsDto);
        }


        [HttpGet]
        [Route("{id:guid}")]
        public async Task< IActionResult> GetRegion(Guid id)
        {
            var region = await regionRepository.GetRegion(id);
            if(region == null)
            {
                return NotFound();
            }
            // return Dto's By Using AutoMapper
           
            var DtoRegion = mapper.Map<Model.DTO.Region>(region);
            return Ok(DtoRegion);
        }


        [HttpPut]
        [Route("[action]")]
        public async Task<bool> CreateRecords([FromBody] AddRegion region)
        {
            // request dto to domain model
            var recordDomain = new Model.Domain.Region() {
               Code = region.Code,
               Name=region.Name,
               Area = region.Area,
               Lat = region.Lat,
               Long = region.Long,
               Population = region.Population
            };
            // add record
           var result = await regionRepository.AddRegion(recordDomain);
            //
            if (result == true) return true;
            else throw new InvalidDataException(); 
        }


        [HttpPost]
        [Route("[Action]")]
        public async Task<IActionResult> CreateResource(Model.DTO.AddRegion region)
        {
            // request for Domain from DTO Obj 
            var recieveDomain = new Model.Domain.Region()
            {
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population
            };
            // Send DTO to Repository
            var addedRecord  = await regionRepository.AddWithGetAddedRegion(recieveDomain);

            //Get Back Dto from Domain Obj  
            var dtoObj = mapper.Map<Model.DTO.Region>(addedRecord);

            return CreatedAtAction(nameof(GetRegion), new { Id = addedRecord.Id }, dtoObj);

            //return Ok(dtoObj);
        }



        [HttpDelete]
        [Route("[Action]/{id:guid}")]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
           var deletedregion = await regionRepository.DeleteRegion(id);
            if (deletedregion == null) return NotFound();
            // Get Dto From Domain
           var deletedDto = mapper.Map<Model.DTO.Region>(deletedregion);
            return Ok(deletedDto);

        }
    }
}
