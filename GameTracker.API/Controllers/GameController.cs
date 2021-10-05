using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameTracker.Services.Interfaces;
using GameTracker.Common.Dtos.Game;
using GameTracker.Common.Models.PagedRequest;
using Microsoft.AspNetCore.Authorization;

namespace GameTracker.API.Controllers
{   
    [Route("api/games")]  
    public class GameController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IGameService _gameService;
        public GameController(IGameService gameService, IMapper mapper)
        {
            _gameService = gameService;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IList<GameDto>> Get()
        {
            var games = await _gameService.GetGames();

            return games;
        }

        [HttpPost("paginated-search")]
        public async Task<PaginatedResult<GameDto>> GetPagedGames(PagedRequest pagedRequest)
        {
            var games = await _gameService.GetPagedGames(pagedRequest);
            return games;
        }

        [AllowAnonymous]
        [HttpPost]      
        public async Task<IActionResult> AddGame(CreateGameDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _gameService.PostAGame(dto);

            return Ok(dto);
        }


        [HttpGet("{id}")]      
        public async Task<GameDto> GetGame(int id)
        {

            var gameDto = await _gameService.GetGame(id);
            return gameDto;
        }
        
        [HttpGet("Gamer/{phoneNumber}")]
        public async Task<IList<GameDto>> GetGameByGamerUserPhone(string phoneNumber)
        {
            var gameDto = await _gameService.GetGameByGamerUserPhone(phoneNumber);
            return gameDto;
        }

        [HttpGet("Company/{userName}")]
        public async Task<IList<GameDto>> GetGameByCompanyId(string userName)
        {
            var gameDto =await _gameService.GetGameByCompanyUsername(userName);
            return gameDto;
        }
       
    }
}
