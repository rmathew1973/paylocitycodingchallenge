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
                .ReverseMap();

            CreateMap<DependentInputModel, Dependent>()
                .ReverseMap();

            CreateMap<Employee, EmployeeViewModel>()
                .ReverseMap();

            CreateMap<Dependent, DependentViewModel>()
                .ReverseMap();
        }
    }
}
