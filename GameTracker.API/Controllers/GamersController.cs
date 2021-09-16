using AutoMapper;
using GameTracker.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GameTracker.Services.Interfaces;
using GameTracker.Common.Dtos.Gamer;
using GameTracker.Common.Models;
using GameTracker.API.Exeptions;

namespace GameTracker.API.Controllers
{
     [Route("api/gamers")]
     [ApiController]
    public class GamersController:BaseController
    {
        private readonly IMapper _mapper;
        private readonly IGamerService _gamerService;
        public GamersController(IGamerService gamerService, IMapper mapper)
        {
            _gamerService = gamerService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Get([FromQuery] FilterOptions filterOptions)
        {
            var gamers = _gamerService.GetGamers(filterOptions);
            var result = gamers.Select(e => _mapper.Map<GamerDto>(e));

            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<GamerDto> GetGamerById(int id)
        {
            var gamerDto = await _gamerService.GetGamerById(id);
            return gamerDto;
        }
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
       // [ApiExceptionFilter]
        public IActionResult Patch(int id, [FromBody] GamerCreateDto dto)
        {
            var gamer = _gamerService.UpdateGamer(id, dto);

            if (gamer == null)
                return NotFound();

            var result = _mapper.Map<GamerDto>(gamer);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task RemoveGamer(int id)
        {
            await _gamerService.DeleteGamer(id);
        }
    }
}
