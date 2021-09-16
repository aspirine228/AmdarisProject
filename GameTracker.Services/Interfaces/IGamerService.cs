using System;
using System.Collections.Generic;
using System.Text;
using GameTracker.Domain.Entities;
using System.Threading.Tasks;
using GameTracker.Common.Dtos.Gamer;
using GameTracker.Common.Models;

namespace GameTracker.Services.Interfaces
{
    public interface IGamerService
    {
        IList<Gamer> GetGamers(FilterOptions options);
        Task<GamerDto> GetGamerById(int id);
        Task<GamerDto> CreateGamer(GamerCreateDto dto);
        Task UpdateGamer(int id, GamerCreateDto dto);
        Task DeleteGamer(int id);
    }
}
