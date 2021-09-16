using AutoMapper;
using GameTracker.Domain.Entities;
using GameTracker.Common.Dtos.Gamer;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameTracker.Services.Profiles
{
    public class GamerProfile : Profile
    {
        public GamerProfile()
        {
            CreateMap<Gamer, GamerDto>();
            CreateMap<GamerCreateDto, Gamer>();

            // CreateMap<Gamer, GamerCreateDto>();
        }
    }
}
