﻿using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.Dtos;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, RegionDto>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, RegionDto>().ReverseMap();
            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
        }
    }
}
