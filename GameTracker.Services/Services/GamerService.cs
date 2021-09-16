using System;
using System.Collections.Generic;
using System.Text;
using GameTracker.Services.Interfaces;
using GameTracker.Rep.Interfaces;
using GameTracker.Domain.Entities;
using AutoMapper;
using System.Threading.Tasks;
using GameTracker.Common.Dtos.Gamer;
using GameTracker.Common.Models;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace GameTracker.Services.Services
{
    public class GamerService : IGamerService
    {
        private readonly IRepository<Gamer> _repository;
        private readonly IMapper _mapper;

        public GamerService(IRepository<Gamer> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<GamerDto> GetGamerById(int id)
        {
            var gamer = await _repository.GetById(id);
            var gamerDto = _mapper.Map<GamerDto>(gamer);
            return gamerDto;
        }
        public IList<Gamer> GetGamers(FilterOptions filterOptions)
        {
            IQueryable<Gamer> gamers;

            if (!string.IsNullOrWhiteSpace(filterOptions.SearchTerm))
            {
                gamers = _repository.FindAll(e => e.Name.Contains(filterOptions.SearchTerm));
            }
            else
            {
                gamers = null;
            }

            switch (filterOptions.Order)
            {
                case SortOrder.Ascending:
                    gamers = gamers.OrderBy(e => e.Name);
                    break;
                case SortOrder.Descending:
                    gamers = gamers.OrderByDescending(e => e.Name);
                    break;
            }

            return gamers.ToList();
        }
        public async Task<GamerDto> CreateGamer(GamerCreateDto dto)
        {
            var gamer = _mapper.Map<Gamer>(dto);
            await _repository.Add(gamer);
            await _repository.SaveChangesAsync();
            var gamerDto = _mapper.Map<GamerDto>(gamer);
            return gamerDto;
        }
        public async Task UpdateGamer(int id,GamerCreateDto dto)
        {
            var gamer = await _repository.GetById(id);
            _mapper.Map(dto, gamer);
            await _repository.SaveChangesAsync();
        }
        public async Task DeleteGamer(int id)
        {
            await _repository.Remove(id);
            await _repository.SaveChangesAsync();
        }
    }
}
