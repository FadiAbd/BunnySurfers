using AutoMapper;
using BunnySurfers.API.DTOs;
using BunnySurfers.API.Entities;

namespace BunnySurfers.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserForGetDTO>();
        }
    }
}
