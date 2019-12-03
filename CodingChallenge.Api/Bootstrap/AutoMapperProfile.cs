using AutoMapper;
using CodingChallenge.Api.Models;
using CodingChallenge.Business.Models;

namespace CodingChallenge.Api.Bootstrap
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EmployeeInputModel, Employee>()
                .ForMember(dest => dest.EmployeeTotalCostPerPayPeriod, opt => opt.Ignore())
                .ForMember(dest => dest.EmployeeTotalCostPerYear, opt => opt.Ignore())
                .ForMember(dest => dest.EmployeeAndDependentsTotalCostPerPayPeriod, opt => opt.Ignore())
                .ForMember(dest => dest.EmployeeAndDependentsTotalCostPerYear, opt => opt.Ignore())
                .ForMember(dest => dest.PayPerYear, opt => opt.Ignore())
                .ForMember(dest => dest.NetPayPerPeriod, opt => opt.Ignore())
                .ForMember(dest => dest.NetPayPerYear, opt => opt.Ignore())
                .ForMember(dest => dest.LessCostForLastPayPeriod, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<DependentInputModel, Dependent>()
                .ForMember(dest => dest.DependentTotalCostPerPayPeriod, opt => opt.Ignore())
                .ForMember(dest => dest.DependentTotalCostPerYear, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Employee, EmployeeViewModel>()
                .ReverseMap();

            CreateMap<Dependent, DependentViewModel>()
                .ReverseMap();
        }
    }
}
