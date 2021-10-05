using AutoMapper;
using GameTracker.Domain.Entities;
using GameTracker.Common.Dtos.FeedBack;


namespace GameTracker.Services.Profiles
{
    public class FeedBackProfile: Profile
    {
        public FeedBackProfile()
        {
            CreateMap<FeedBack, FeedBackDto>().ForMember(x => x.UserName, y => y.MapFrom(z => z.User.UserName));
            CreateMap<FeedBackDto, FeedBack>();
            CreateMap<CreateFeedBackDto, FeedBack>();
        }
    }
}
