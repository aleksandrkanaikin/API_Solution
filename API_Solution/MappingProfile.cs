using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace API_Solution
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>().ForMember(c => c.FullAddress, opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Car, CarDto>();
            CreateMap<Driver, DriverDto>();
            CreateMap<CompanyForCreationDto, Company>();
            CreateMap<EmployeeForCreationDto, Employee>();
            CreateMap<DriverForCreatonDto, Driver>();
            CreateMap<CarForCreationDto, Car>();
            CreateMap<EmployeeForUpdateDto, Employee>();
            CreateMap<CompanyForUpdateDto, Company>();
            CreateMap<CarForUpdateDto, Car>();
            CreateMap<DriverForUpdateDto, Driver>();
            CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
            CreateMap<CarForUpdateDto, Car>().ReverseMap();
        }
    }
}
