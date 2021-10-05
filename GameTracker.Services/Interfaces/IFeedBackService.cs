using GameTracker.Common.Dtos.FeedBack;
using GameTracker.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameTracker.Services.Interfaces
{
    public interface IFeedBackService
    {
        Task<FeedBackDto> GetCurrentFeedBack(int id);
        Task<IList<FeedBackDto>> GetAllFeedBacks();
        Task<IList<FeedBackDto>> GetFeedBacksByUserId(int id);
        Task<FeedBackDto> CreateFeedBack(FeedBack feedBack);
        Task UpdateFeedBack(int id, FeedBackDto dto);
        Task DeleteFeedBack(int id);
    }
}
