using AutoMapper;
using GameTracker.Domain.Entities;
using GameTracker.Common.Dtos.Gamer;

namespace GameTracker.Services.Profiles
{
    public class GamerProfile : Profile
    {
        public GamerProfile()
        {
            CreateMap<Gamer, GamerDto>();
            CreateMap<GamerCreateDto, Gamer>();

            
        }
    }
}
