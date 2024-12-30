using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.Dtos;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
           _dbContext = dbContext;
            _regionRepository = regionRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
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

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
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
