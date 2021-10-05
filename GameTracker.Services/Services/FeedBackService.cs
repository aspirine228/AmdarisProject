using AutoMapper;
using GameTracker.Common.Dtos.FeedBack;
using GameTracker.Domain.Entities;
using GameTracker.Rep.Interfaces;
using GameTracker.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameTracker.Services.Services
{
    public class FeedBackService:IFeedBackService
    {

        private readonly IRepository<FeedBack> _repository;
        private readonly IMapper _mapper;

        public FeedBackService(IRepository<FeedBack> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<FeedBackDto>> GetAllFeedBacks()
        {
            List<FeedBackDto> feedBacksDto = new List<FeedBackDto>();
            var feedbacks =  _repository.GetAll();
            foreach (var feedBack in feedbacks)
            {
                var feedBackDto = _mapper.Map<FeedBackDto>(feedBack);
                feedBacksDto.Add(feedBackDto);
            }

            return feedBacksDto;
        }

        public async Task<FeedBackDto> GetCurrentFeedBack(int id)
        {
            var feedBack = await _repository.GetById(id);
            var feedBackDto = _mapper.Map<FeedBackDto>(feedBack);
            return feedBackDto;
        }

        public async Task<IList<FeedBackDto>> GetFeedBacksByUserId(int id)
        {
            List<FeedBackDto> feedBacksDto = new List<FeedBackDto>();

            var feedBacks = _repository.GetAll();
            foreach (var feedBack in feedBacks.Where(x=>x.UserId==id))
            {
                var feedBackDto = _mapper.Map<FeedBackDto>(feedBack);
                feedBacksDto.Add(feedBackDto);
            }

            return feedBacksDto;
        }

        public async Task<FeedBackDto> CreateFeedBack(FeedBack feedBack)
        {           
            await _repository.Add(feedBack);
            await _repository.SaveChangesAsync();
            var feedBackDto = _mapper.Map<FeedBackDto>(feedBack);
            return feedBackDto;
        }

        public async Task UpdateFeedBack(int id, FeedBackDto dto)
        {
            var feedBack = await _repository.GetById(id);
          
            _mapper.Map(dto, feedBack);

            await _repository.SaveChangesAsync();
        }

        public async Task DeleteFeedBack(int id)
        {
            await _repository.Remove(id);
            await _repository.SaveChangesAsync();
        }
    }
}
