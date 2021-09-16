using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using GameTracker.Domain.Entities;
using GameTracker.Common.Dtos.Game;

namespace GameTracker.Services.Profiles
{
    public class GameProfile: Profile
    {
        public GameProfile()
        {

            CreateMap<Game, GameDto>();
        }
    }
}
