using AutoMapper;
using BunnySurfers.API.DTOs;
using BunnySurfers.API.Entities;

namespace BunnySurfers.API.Profiles
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<Document, DocumentGetDTO>();
            CreateMap<DocumentEditDTO, Document>();
        }
    }
}
