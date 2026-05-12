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
            CreateMap<RegisterDto, AppUser>();
            CreateMap<UploadDocumentDto, EmployeeDocument>()
                .ForMember(
                dest => dest.OriginalName,
                opt => opt.MapFrom(src => src.File.FileName))
            .ForMember(
                dest => dest.ContentType,
                opt => opt.MapFrom(src => src.File.ContentType))
            .ForMember(
                dest => dest.Size,
                opt => opt.MapFrom(src => src.File.Length))
            .ForMember(
                dest => dest.UploadDate,
                opt => opt.Ignore())
            .ForMember(
                dest => dest.FileName,
                opt => opt.Ignore());
        }
    }
}
