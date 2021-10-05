
using System.Collections.Generic;
using GameTracker.Services.Interfaces;
using GameTracker.Rep.Interfaces;
using GameTracker.Domain.Entities;
using AutoMapper;
using System.Threading.Tasks;
using GameTracker.Common.Dtos.Gamer;
using System.Linq;


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

        public IList<GamerDto> GetGamers()
        {
            List<GamerDto> gamersDto = new List<GamerDto>();

            var gamers = _repository.GetAll();
            foreach (var gamer in gamers)
            {
                var gamerDto = _mapper.Map<GamerDto>(gamer);
                gamersDto.Add(gamerDto);
            }

            return gamersDto;
        }

        public Gamer GetGamerByPhone(string phoneNumber)
        {
            var gamer = _repository.GetAll().Where(e => e.PhoneNumber == phoneNumber).FirstOrDefault();

            return gamer;
        }

        public async Task<GamerDto> CreateGamer(GamerCreateDto dto)
        {
            var gamer = _mapper.Map<Gamer>(dto);
            await _repository.Add(gamer);
            await _repository.SaveChangesAsync();
            var gamerDto = _mapper.Map<GamerDto>(gamer);
            return gamerDto;
        }

        public async Task<IList<GamerDto>> GetGamersByCompanyId(int id)
        {
            var gamers = await _repository.GetAllWithInclude(gamers=>gamers.Games);

            var gomers = gamers.Where(g => g.Games.Any() == g.Games.Where(h => h.CompanyId == id).Any()).ToList();
            List<GamerDto> gamersDto = new List<GamerDto>();

          
            foreach (var gamer in gomers)
            {
                var gamerDto = _mapper.Map<GamerDto>(gamer);
                gamersDto.Add(gamerDto);
            }

            return gamersDto;
            
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
