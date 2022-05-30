using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameTracker.Services.Interfaces;
using GameTracker.Common.Dtos.Gamer;
using GameTracker.API.Exeptions;
using Microsoft.AspNetCore.Authorization;

namespace GameTracker.API.Controllers
{
     [Route("api/gamers")]
     
    public class GamersController:BaseController
    {
        private readonly IMapper _mapper;
        private readonly IGamerService _gamerService;
        private readonly ICompanyService _companyService;
        public GamersController(IGamerService gamerService, IMapper mapper, ICompanyService companyService)
        {
            _gamerService = gamerService;
            _mapper = mapper;
            _companyService = companyService;
        }

        [HttpGet]
        public IList<GamerDto> Get()
        {
            var gamers = _gamerService.GetGamers();
            return gamers;
        }

        [HttpGet("{id}")]
        public async Task<GamerDto> GetGamerById(int id)
        {
            var gamerDto = await _gamerService.GetGamerById(id);
            return gamerDto;
        }

        [AllowAnonymous]
        [HttpGet("gamer/{phoneNumber}")]
        public async Task<GamerDto> GetGamerByPhoneNumber(string phoneNumber)
        {
            var gamer = _gamerService.GetGamerByPhone(phoneNumber);
            var gamerDto = _mapper.Map<GamerDto>(gamer);
            return gamerDto;
        }

   
        [HttpGet("company/{companyName}")]
        public async Task<IList<GamerDto>> GetGamersByCompanyName(string companyName)
        {
            var companyId= await _companyService.GetCompanyIdByUserName(companyName);
            var gameDto = await _gamerService.GetGamersByCompanyId(companyId.Id);
            return gameDto;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateGamer(GamerCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var gamerDto = await _gamerService.CreateGamer(dto);
            
            return CreatedAtAction(nameof(GetGamerById), new { id = gamerDto.Id }, gamerDto);
        }

        [HttpPut("{id}")]
        [ApiExceptionFilter]
        public async Task<IActionResult> UpdateGamer(int id, GamerCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _gamerService.UpdateGamer(id, dto);
            return Ok();
        }

        [HttpPatch("{id}")]
        [ApiExceptionFilter]
        public IActionResult Patch(int id, GamerCreateDto dto)
        {
            var gamer = _gamerService.UpdateGamer(id, dto);

            if (gamer == null)
                return NotFound();

            //var result = _mapper.Map<GamerDto>(gamer);
            return Ok(gamer);
        }

        [HttpDelete("{id}")]
        public async Task RemoveGamer(int id)
        {
            await _gamerService.DeleteGamer(id);

        }
    }
}
