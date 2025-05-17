using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.Dtos.RegionDTO;
using NZWalks.API.Repositories;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionsController> _logger;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            _dbContext = dbContext;
            _regionRepository = regionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("GetAll Regions is called");
            var regions = await _regionRepository.GetAllAsync();

            //var regionDto = new List<RegionDto>();
            //foreach (var region in regionDto)
            //{
            //    //regionDto.Add(new RegionDto()
            //    //{
            //    //    Id = region.Id,
            //    //    Code = region.Code,
            //    //    RegionName = region.RegionName,
            //    //    RegionImageUrl = region.RegionImageUrl,
            //    //});
            //}

            var regionDto = _mapper.Map<List<RegionDto>>(regions);
            _logger.LogInformation($"GetAll Regions request with data: {JsonSerializer.Serialize(regionDto)}");

            return Ok(regionDto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var region = await _regionRepository.GetById(id);
            if (region == null)
            {
                return NotFound();
            }

            //var regionDto = new RegionDto()
            //{
            //    Id = region.Id,
            //    Code = region.Code,
            //    RegionName = region.RegionName,
            //    RegionImageUrl = region.RegionImageUrl,
            //};

            var regionDto = _mapper.Map<RegionDto>(region);
            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            if (ModelState.IsValid)
            {
                //var region = new Region
                //{
                //    Code = addRegionRequestDto.Code,
                //    RegionName = addRegionRequestDto.RegionName,
                //    RegionImageUrl = addRegionRequestDto.RegionImageUrl,
                //};

                var region = _mapper.Map<Region>(addRegionRequestDto);

                region = await _regionRepository.Create(region);

                //var regionDto = new RegionDto
                //{
                //    Id = region.Id,
                //    Code = region.Code,
                //    RegionName = region.RegionName,
                //    RegionImageUrl = region.RegionImageUrl,
                //};

                var regionDto = _mapper.Map<RegionDto>(region);

                return CreatedAtAction(nameof(GetById), new
                {
                    id = regionDto.Id,
                }, regionDto
                );
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            if (ModelState.IsValid)
            {
                //var region = new Region
                //{
                //    Code = updateRegionRequestDto.Code,
                //    RegionName = updateRegionRequestDto.RegionName,
                //    RegionImageUrl = updateRegionRequestDto.RegionImageUrl
                //};

                var region = _mapper.Map<Region>(updateRegionRequestDto);

                region = await _regionRepository.Update(id, region);

                if (region == null)
                {
                    return NotFound();
                }

                //var regionDto = new RegionDto
                //{
                //    Id = region.Id,
                //    Code = region.Code,
                //    RegionName = region.RegionName,
                //    RegionImageUrl = region.RegionImageUrl,
                //};

                var regionDto = _mapper.Map<RegionDto>(region);
                return Ok(regionDto);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var region = await _regionRepository.Delete(id);
            if (region == null)
            {
                return NotFound();
            }

            //var regionDto = new RegionDto
            //{
            //    Id = region.Id,
            //    Code = region.Code,
            //    RegionName = region.RegionName,
            //    RegionImageUrl = region.RegionImageUrl
            //};

            var regionDto = _mapper.Map<RegionDto>(region);
            return Ok();
        }
    }
}
