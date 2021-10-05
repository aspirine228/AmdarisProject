using System.Collections.Generic;
using GameTracker.Domain.Entities;
using System.Threading.Tasks;
using GameTracker.Common.Dtos.Gamer;

namespace GameTracker.Services.Interfaces
{
    public interface IGamerService
    {
        IList<GamerDto> GetGamers();
        Task<GamerDto> GetGamerById(int id);
        Task<GamerDto> CreateGamer(GamerCreateDto dto);
        Task UpdateGamer(int id, GamerCreateDto dto);
        Task DeleteGamer(int id);
        Task<IList<GamerDto>> GetGamersByCompanyId(int id);
        Gamer GetGamerByPhone(string phoneNumber);
    }
}
