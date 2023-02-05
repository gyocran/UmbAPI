using AutoMapper;
using UmbAPI.DTO;
using UmbAPI.Models;

namespace UmbAPI.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Department, DepartmentDto>().ReverseMap();
            //CreateMap<EmployeeType, EmployeeTypeDto>().ReverseMap();
            //CreateMap<Employee, EmployeeDto>().ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.DepartmentName)).ReverseMap();
            //CreateMap<EmployeeDto, Employee>().ForMember(dest => dest.Department.DepartmentName, opt => opt.MapFrom(src => src.DepartmentName)).ReverseMap();
        }
    }
}
