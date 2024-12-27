namespace NZWalks.API.Models.Dtos
{
    public class AddRegionRequestDto
    {
        public string Code { get; set; }
        public string RegionName { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
