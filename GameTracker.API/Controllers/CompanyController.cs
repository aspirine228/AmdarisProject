using AutoMapper;
using GameTracker.Common.Dtos.Company;
using GameTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameTracker.API.Controllers
{
    [Route("api/companies")]
    public class CompanyController : BaseController
    {

        private readonly IMapper _mapper;
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }

        [HttpGet]     
        public async Task<IList<CompanyDto>> Get()
        {
            var companies = await _companyService.GetAllCompanies();

            return companies;
        }

        [HttpGet("{userName}")]
        public async Task<CompanyDto> GetCompanyIdByCompanyUserName(string userName)
        {
            var company = await _companyService.GetCompanyIdByUserName(userName);

            return company;
        }
    }
}
