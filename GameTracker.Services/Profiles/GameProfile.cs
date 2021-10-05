
using AutoMapper;
using GameTracker.Domain.Entities;
using GameTracker.Common.Dtos.Game;

namespace GameTracker.Services.Profiles
{
    public class GameProfile: Profile
    {
        public GameProfile()
        {
            CreateMap<Game, GameDto>()
                .ForMember(x => x.PhoneNumber, y => y.MapFrom(z => z.Stat.PhoneNumber))
                .ForMember(x => x.Prize, y => y.MapFrom(z => z.Stat.Prize))
                .ForMember(x => x.Try1, y => y.MapFrom(z => z.Stat.Try1))
                .ForMember(x => x.Try2, y => y.MapFrom(z => z.Stat.Try2))
                .ForMember(x => x.Scenario, y => y.MapFrom(z => z.Stat.Scenario));
            CreateMap<CreateGameDto, Game>()
                .ForMember(entity => entity.Stat, memberOptions => memberOptions.MapFrom(dto => dto));
            CreateMap<CreateGameDto, GameStat>();
              
        }
    }
}
