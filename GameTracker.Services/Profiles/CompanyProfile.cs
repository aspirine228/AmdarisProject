
using AutoMapper;
using GameTracker.Domain.Entities;
using GameTracker.Common.Dtos.Company;

namespace GameTracker.Services.Profiles
{
    public class CompanyProfile:Profile
    {
        public CompanyProfile()
        {

            CreateMap<Company, CompanyDto>()
                .ForMember(x => x.ContractStart, y => y.MapFrom(z => z.CompanyContract.ContractStart))
                .ForMember(x => x.ContractEnd, y => y.MapFrom(z => z.CompanyContract.ContractEnd));
              
        }

    }
}
