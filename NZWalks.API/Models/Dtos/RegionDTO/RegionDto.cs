﻿namespace NZWalks.API.Models.Dtos.RegionDTO
{
    public class RegionDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string RegionName { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
