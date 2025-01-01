using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.Dtos.WalksDTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            _mapper = mapper;
            _walkRepository = walkRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            if (ModelState.IsValid)
            {
                var walk = _mapper.Map<Walk>(addWalkRequestDto);

                await _walkRepository.CreateAsync(walk);

                var walkDto = _mapper.Map<WalkDto>(walk);
                return Ok(walkDto);
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery)
        {
            var walks = await _walkRepository.GetAllAsync(filterOn, filterQuery);

            var walksDto = _mapper.Map<List<WalkDto>>(walks);
            return Ok(walksDto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walks = await _walkRepository.GetById(id);

            if (walks == null)
            {
                return NotFound();
            }

            var walkDto = _mapper.Map<WalkDto>(walks);
            return Ok(walkDto);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            if (ModelState.IsValid)
            {
                var walks = _mapper.Map<Walk>(updateWalkRequestDto);

                walks = await _walkRepository.Update(id, walks);

                if (walks == null)
                {
                    return NotFound();
                }

                var walkDto = _mapper.Map<WalkDto>(walks);

                return Ok(walkDto);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walks = await _walkRepository.Delete(id);

            if (walks == null)
            {
                return NotFound();
            }

            var walkDto = _mapper.Map<WalkDto>(walks);
            return Ok(walkDto);
        }
    }
}
