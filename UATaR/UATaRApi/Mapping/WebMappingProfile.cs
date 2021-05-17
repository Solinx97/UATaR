using AutoMapper;
using BusinessLayer.Entities;
using UATaRApi.ViewModels;

namespace UATaRApi.Mapping
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<ExecuteLoadViewModel, ExecuteLoad>().ReverseMap();
            CreateMap<LoadTypeViewModel, LoadType>().ReverseMap();
            CreateMap<LoadViewModel, Load>().ReverseMap();
            CreateMap<SpecialityViewModel, Speciality>().ReverseMap();
            CreateMap<SubjectViewModel, Subject>().ReverseMap();
            CreateMap<TeacherViewModel, Teacher>().ReverseMap();
            CreateMap<GroupViewModel, Group>().ReverseMap();
        }
    }
}