using AutoMapper;
using BusinessLayer.Entities;
using DataAccessLayer.DTO;

namespace BusinessLayer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ExecuteLoad, ExecuteLoadDto>().ReverseMap();
            CreateMap<Load, LoadDto>().ReverseMap();
            CreateMap<LoadType, LoadTypeDto>().ReverseMap();
            CreateMap<Speciality, SpecialityDto>().ReverseMap();
            CreateMap<Subject, SubjectDto>().ReverseMap();
            CreateMap<Teacher, TeacherDto>().ReverseMap();
            CreateMap<Group, GroupDto>().ReverseMap();
        }
    }
}