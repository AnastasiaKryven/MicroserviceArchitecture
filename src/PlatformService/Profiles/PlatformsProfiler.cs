using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles
{
    public class PlatformsProfiler : Profile
    {
        public PlatformsProfiler()
        {
            // Source -> Target
            CreateMap<Platform, PlatformReadDto>();

            CreateMap<PlatformCreateDto, Platform>();

            CreateMap<PlatformReadDto, PlatformPublishedDto>();
        }
    }
}
