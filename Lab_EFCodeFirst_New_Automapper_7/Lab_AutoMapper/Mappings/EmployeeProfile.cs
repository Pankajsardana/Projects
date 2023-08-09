using AutoMapper;
using DAL;
using Lab_AutoMapper.Models;

namespace Lab_AutoMapper.Mappings
{
    public class EmployeeProfile: Profile
    {
        public EmployeeProfile()
        {            //src => dest
                 CreateMap<Employee, EmployeeViewModel>()    
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))    
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))  
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))  
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId)) 
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
                .ReverseMap() //dest => src
                              .ForMember(src => src.Department, opt => opt.Ignore());//ignore navigation prop
        } 
    }
}
