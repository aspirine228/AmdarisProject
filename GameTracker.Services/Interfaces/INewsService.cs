using GameTracker.Common.Dtos.News;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace GameTracker.Services.Interfaces
{
    public interface INewsService
    {
        Task<NewsDto> GetCurrentNews(int id);
        Task<IList<NewsDto>> GetNews();
        Task<NewsDto> CreateNews(CreateNewsDto dto);
        Task UpdateNews(int id, CreateNewsDto dto);
        Task DeleteNews(int id);
    }
}
