using GameTracker.Common.Dtos.Game;
using GameTracker.Common.Models.PagedRequest;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameTracker.Services.Interfaces
{
    public interface IGameService
    {
        Task<GameDto> GetGame(int id);

        Task PostAGame(CreateGameDto dto);
        Task<IList<GameDto>> GetGames();
        Task<PaginatedResult<GameDto>> GetPagedGames(PagedRequest pagedRequest);
        Task<IList<GameDto>> GetGameByGamerUserPhone(string phoneNumber);
        Task<IList<GameDto>> GetGameByCompanyUsername(string userName);

    }
}
