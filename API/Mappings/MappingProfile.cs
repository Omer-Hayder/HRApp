using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateEmployeeDto, Employee>()
                .ForMember(dest => dest.EmployeeProjects,
                    opt => opt.MapFrom(src => src.ProjectIds
                        .Select(id => new EmployeeProject
                        {
                            ProjectId = id
                        }
                        )));

            CreateMap<Employee, EmployeeResponseDto>();
        }
    }
}
