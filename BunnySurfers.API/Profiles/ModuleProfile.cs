using AutoMapper;
using BunnySurfers.API.DTOs;
using BunnySurfers.API.Entities;

namespace BunnySurfers.API.Profiles
{
    public class ModuleProfile : Profile
    {
        public ModuleProfile()
        {
            CreateMap<Module, ModuleGetDTO>();
            CreateMap<ModuleEditDTO, Module>();
        }
    }
}
