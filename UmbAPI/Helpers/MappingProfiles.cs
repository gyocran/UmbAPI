using AutoMapper;
using UmbAPI.DTO;
using UmbAPI.Models;

namespace UmbAPI.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(d => d.DepartmentName, opt => opt.MapFrom(src => src.Department.DepartmentName))
                .ReverseMap();
            CreateMap<Department, DepartmentDTO>().ReverseMap();
        }
    }
}
