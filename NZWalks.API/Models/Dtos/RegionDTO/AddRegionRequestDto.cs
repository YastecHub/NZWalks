using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.Dtos.RegionDTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to ba a minimum of three characters")]
        [MaxLength(3, ErrorMessage = "Code has to ba a maximum of three characters")]
        public string Code { get; set; }

        [Required]
        public string RegionName { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
