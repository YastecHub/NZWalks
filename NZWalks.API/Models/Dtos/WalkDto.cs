﻿using NZWalks.API.Models.Domain;

namespace NZWalks.API.Models.Dtos
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public string WalkName { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Difficulty Difficulty { get; set; }
        public Guid RegionId { get; set; }
        public Region Region { get; set; }
    }
}