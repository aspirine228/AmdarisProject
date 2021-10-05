using System.Collections.Generic;
using System.Threading.Tasks;
using GameTracker.Common.Dtos.Company;


namespace GameTracker.Services.Interfaces
{
    public interface ICompanyService
    {       
        Task<IList<CompanyDto>> GetAllCompanies();
      
        Task<CompanyDto> CreateCompany(CompanyDto dto);

        Task<CompanyDto> GetCompanyIdByUserName(string userName);

    }
}
