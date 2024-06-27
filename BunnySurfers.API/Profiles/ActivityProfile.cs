using AutoMapper;
using BunnySurfers.API.DTOs;
using BunnySurfers.API.Entities;

namespace BunnySurfers.API.Profiles
{
    public class ActivityProfile : Profile
    {
        public ActivityProfile()
        {
            CreateMap<Activity, ActivityGetDTO>();
            CreateMap<ActivityEditDTO, Activity>();
        }
    }
}
