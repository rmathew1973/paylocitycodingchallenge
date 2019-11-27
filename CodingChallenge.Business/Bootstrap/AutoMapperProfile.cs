using AutoMapper;
using CodingChallenge.Business.Models;

namespace CodingChallenge.Business.Bootstrap
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Dependent, Repository.Models.Dependent>()
                .ForMember(dest => dest.Employee, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Employee, Repository.Models.Employee>()
                .ReverseMap();
        }
    }
}
