using AutoMapper;
using GameTracker.Domain.Entities;
using GameTracker.Common.Dtos.News;


namespace GameTracker.Services.Profiles
{
    public class NewsProfile:Profile
    {
        public NewsProfile()
        {
            CreateMap<News, NewsDto>();
            CreateMap<CreateNewsDto, News>();

        }
    }
}
