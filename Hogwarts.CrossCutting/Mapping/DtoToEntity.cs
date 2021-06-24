using AutoMapper;
using Hogwarts.Domain.Dtos;
using Hogwarts.Domain.Entities;

namespace Hogwarts.CrossCutting.Mapping
{
    public class DtoToEntity : Profile
    {
        public DtoToEntity()
        {
            CreateMap<CharacterInsertDto, CharacterEntity>().ReverseMap();
            CreateMap<CharacterResultDto, CharacterEntity>().ReverseMap();
            CreateMap<CharacterUpdateDto, CharacterEntity>().ReverseMap();
        }
        
    }
}
