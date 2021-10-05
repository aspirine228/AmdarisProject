using AutoMapper;
using GameTracker.API.Exeptions;
using GameTracker.Common.Dtos.News;
using GameTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameTracker.API.Controllers
{
    [Route("api/news")]
    public class NewsController :BaseController
    {
       
        private readonly IMapper _mapper;
        private readonly INewsService _newsService;
        public NewsController(INewsService newsService, IMapper mapper)
        {
            _newsService = newsService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IList<NewsDto>> GetNews()
        {
            var news = await _newsService.GetNews();
            
            return news;
        }

        [HttpGet("{id}")]
        public async Task<NewsDto> GetCurrentNews(int id)
        {
            var newsDto = await _newsService.GetCurrentNews(id);
            return newsDto;
        }

       
        [HttpPost("create")]
        public async Task<IActionResult> CreateNews(CreateNewsDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newsDto = await _newsService.CreateNews(dto);

            return CreatedAtAction(nameof(GetCurrentNews), new { id = newsDto.Id }, newsDto);
        }

        [HttpPut("{id}")]
        [ApiExceptionFilter]
        public async Task<IActionResult> UpdateNews(int id, CreateNewsDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _newsService.UpdateNews(id, dto);
            return Ok();
        }

  
        [HttpDelete("{id}")]
        public async Task RemoveNews(int id)
        {
            await _newsService.DeleteNews(id);

        }
    }
}
