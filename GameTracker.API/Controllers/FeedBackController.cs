using AutoMapper;
using GameTracker.API.Exeptions;
using GameTracker.Common.Dtos.FeedBack;
using GameTracker.Domain.Auth;
using GameTracker.Domain.Entities;
using GameTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameTracker.API.Controllers
{
    [Route("api/feedback")]
    public class FeedBackController :BaseController
    {
        private readonly IMapper _mapper;
        private readonly IFeedBackService _feedBackService;
        private readonly UserManager<User> _userManager;
        public FeedBackController(IFeedBackService feedBackService, IMapper mapper, UserManager<User> userManager)
        {
            _feedBackService = feedBackService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IList<FeedBackDto>> GetFeedBacks()
        {
            var news = await _feedBackService.GetAllFeedBacks();

            return news;
        }

        [HttpGet("user/{id}")]
        public async Task<IList<FeedBackDto>> GetFeedBacksByUserId(int id)
        {
            var news = await _feedBackService.GetFeedBacksByUserId(id);

            return news;
        }

        [HttpGet("{id}")]
        public async Task<FeedBackDto> GetCurrentFeedBack(int id)
        {
            var newsDto = await _feedBackService.GetCurrentFeedBack(id);
            return newsDto;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateFeedBack(CreateFeedBackDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(dto.UserName);
            var feedBack = _mapper.Map<FeedBack>(dto);
            feedBack.UserId = user.Id;
            var feedBackDto = await _feedBackService.CreateFeedBack(feedBack);

            return CreatedAtAction(nameof(GetCurrentFeedBack), new { id = feedBackDto.Id }, feedBackDto);
        }

        [HttpPatch("Update")]
        [ApiExceptionFilter]
        public async Task<IActionResult> UpdateFeedBack(FeedBackDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _feedBackService.UpdateFeedBack(dto.Id, dto);
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task RemoveFeedBack(int id)
        {
            await _feedBackService.DeleteFeedBack(id);

        }

    }
}
