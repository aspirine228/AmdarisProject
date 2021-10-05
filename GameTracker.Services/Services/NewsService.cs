using AutoMapper;
using GameTracker.Common.Dtos.News;
using GameTracker.Domain.Entities;
using GameTracker.Rep.Interfaces;
using GameTracker.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameTracker.Services.Services
{
    public class NewsService: INewsService
    {
        private readonly IRepository<News> _repository;
        private readonly IMapper _mapper;

        public NewsService(IRepository<News> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<NewsDto> GetCurrentNews(int id)
        {
            var news = await _repository.GetById(id);
            var newsDto = _mapper.Map<NewsDto>(news);
            return newsDto;
        }

        public async Task<IList<NewsDto>> GetNews()
        {
            List<NewsDto> newssDto = new List<NewsDto>();

            var newss = _repository.GetAll();
            foreach (var news in newss)
            {
                var newsDto = _mapper.Map<NewsDto>(news);
                newssDto.Add(newsDto);
            }

            return newssDto;
        }

        public async Task<NewsDto> CreateNews(CreateNewsDto dto)
        {
            var news = _mapper.Map<News>(dto);
            await _repository.Add(news);
            await _repository.SaveChangesAsync();
            var newsDto = _mapper.Map<NewsDto>(news);
            return newsDto;
        }

        public async Task UpdateNews(int id, CreateNewsDto dto)
        {
            var news = await _repository.GetById(id);
            _mapper.Map(dto, news);
            await _repository.SaveChangesAsync();

        }

        public async Task DeleteNews(int id)
        {
            await _repository.Remove(id);
            await _repository.SaveChangesAsync();
        }
    }
}
