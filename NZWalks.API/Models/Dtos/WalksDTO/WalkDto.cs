using NZWalks.API.Models.Domain;
using NZWalks.API.Models.Dtos.RegionDTO;

namespace NZWalks.API.Models.Dtos.WalksDTO
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public string WalkName { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public DifficultyDto Difficulty { get; set; }
        public RegionDto Region { get; set; }
    }
}
