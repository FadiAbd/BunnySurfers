using AutoMapper;
using BunnySurfers.API.DTOs;
using BunnySurfers.API.Entities;

namespace BunnySurfers.API.Profiles
{
    public class StudentViewProfile : Profile
    {
        public StudentViewProfile()
        {
            CreateMap<User, UserForStudentViewDTO>();
            CreateMap<Document, DocumentForStudentViewDTO>();
            CreateMap<Activity, ActivityForStudentViewDTO>();
            CreateMap<Module, ModuleForStudentViewDTO>();
            CreateMap<Course, CourseForStudentViewDTO>();
        }
    }
}
