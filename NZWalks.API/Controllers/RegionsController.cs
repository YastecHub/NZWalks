using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.Dtos;
using System.Reflection.Metadata.Ecma335;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
           _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = _dbContext.Regions.ToList();   

            var regionDto = new List<RegionDto>();
            foreach (var region in regionDto)
            {
                regionDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Code = region.Code,
                    RegionName = region.RegionName,
                    RegionImageUrl = region.RegionImageUrl,
                });
            }

            return Ok(regionDto);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var region = _dbContext.Regions.Find(id);
            if (region == null)
            {
                return NotFound();
            }

            var regionDto = new RegionDto()
            {
                Id = region.Id,
                Code = region.Code,
                RegionName = region.RegionName,
                RegionImageUrl = region.RegionImageUrl,
            };
            return Ok(regionDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var region = new Region
            {
                Code = addRegionRequestDto.Code,
                RegionName = addRegionRequestDto.RegionName,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl,
            };

            _dbContext.Regions.Add(region);
            _dbContext.SaveChanges();

            var regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                RegionName = region.RegionName,
                RegionImageUrl = region.RegionImageUrl,
            };

            return CreatedAtAction(nameof(GetById), new
            {
                id = regionDto.Id,
            }, regionDto
            );
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var region = _dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (region == null)
            {
                return NotFound();
            }

            region.Code = updateRegionRequestDto.Code;
            region.RegionName = updateRegionRequestDto.RegionName;
            region.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            _dbContext.SaveChanges( );

            var regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                RegionName = region.RegionName,
                RegionImageUrl = region.RegionImageUrl,
            };
            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var region = _dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (region == null)
            {
                return NotFound();
            }

            _dbContext.Regions.Remove(region);
            _dbContext.SaveChanges();

            var regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                RegionName = region.RegionName,
                RegionImageUrl = region.RegionImageUrl
            };
            return Ok();
        }
    }
}
