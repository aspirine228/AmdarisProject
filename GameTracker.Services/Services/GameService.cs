using AutoMapper;
using GameTracker.Common.Dtos.Game;
using GameTracker.Common.Models.PagedRequest;
using GameTracker.Domain.Entities;
using GameTracker.Rep.Interfaces;
using GameTracker.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameTracker.Services.Services
{
    public class GameService : IGameService
    {
        private readonly IRepository<Game> _repository;
      
        private readonly IMapper _mapper;
      
        public GameService(IRepository<Game> repository,  IMapper mapper)
        {
            _repository = repository;
            
            _mapper = mapper;
            
        }

        public async Task<GameDto> GetGame(int id)
        {
            var game = await _repository.GetByIdWithInclude<Game>(id, stat => stat.Stat);
            var gameDto = _mapper.Map<GameDto>(game);
            return gameDto;
        }

        public async Task PostAGame(CreateGameDto dto)
        {
            var game = _mapper.Map<Game>(dto);
       
            await _repository.Add(game);
    
            await _repository.SaveChangesAsync();
        }

        public async Task<IList<GameDto>> GetGames()
        {
            var games = await _repository.GetAllWithInclude(stat => stat.Stat);

            List<GameDto> _list= new List<GameDto>();
            foreach(var game in games)
            {
                var gameDto = _mapper.Map<GameDto>(game);
                _list.Add(gameDto);
            }
            return _list;
        }

        public async Task<PaginatedResult<GameListDto>> GetPagedGames(PagedRequest pagedRequest)
        {
            var pagedGamesDto = await _repository.GetPagedData<Game, GameListDto>(pagedRequest);
            return pagedGamesDto;
        }

        public async Task<IList<GameDto>> GetGameByGamerUserPhone(string phoneNumber)
        {
            var games = await _repository.GetAllWithInclude(gamer=>gamer.Gamer , gamer=>gamer.Stat);
          
            List<GameDto> _list = new List<GameDto>();
            foreach (var game in games.Where(e=>e.Gamer.PhoneNumber==phoneNumber))
            {           
                
                    var gameDto = _mapper.Map<GameDto>(game);
                    _list.Add(gameDto);          
            }
            return _list;
        }

        public async Task<IList<GameDto>> GetGameByCompanyUsername(string userName)
        {
            var games = await _repository.GetAllWithInclude(gamer=>gamer.Company, gamer=>gamer.Stat);
            List<GameDto> _list = new List<GameDto>();
            foreach (var game in games.Where(e => e.Company.CompanyName==userName))
            {
                var gameDto = _mapper.Map<GameDto>(game);
                _list.Add(gameDto);
            }
            return _list;
        }

     
    }
}
