using AutoMapper;
using GameTracker.Common.Dtos.Company;
using GameTracker.Domain.Entities;
using GameTracker.Rep.Interfaces;
using GameTracker.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameTracker.Services.Services
{
    public class CompanyService: ICompanyService
    {
        private readonly IRepository<Company> _repository;
        private readonly IMapper _mapper;
        public CompanyService(IRepository<Company> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<CompanyDto>> GetAllCompanies()
        {
            var companies = await _repository.GetAllWithInclude(stat => stat.CompanyContract);

            List<CompanyDto> _list = new List<CompanyDto>();
            foreach (var company in companies)
            {
                var companyDto = _mapper.Map<CompanyDto>(company);
                _list.Add(companyDto);
            }
            return _list;
        }

        public async Task<CompanyDto> GetCompanyIdByUserName(string userName)
        {
            var companies = await _repository.GetAllWithInclude(stat => stat.CompanyContract);
      
            var company = companies.Where(e=>e.CompanyName==userName).FirstOrDefault();

            var companyDto = _mapper.Map<CompanyDto>(company);
            return companyDto;
        }
     
        public async Task<CompanyDto> CreateCompany(CompanyDto dto)
        {
            var company = _mapper.Map<Company>(dto);
            await _repository.Add(company);
            await _repository.SaveChangesAsync();
            var companyDto = _mapper.Map<CompanyDto>(company);
            return companyDto;
        }

    }
}
