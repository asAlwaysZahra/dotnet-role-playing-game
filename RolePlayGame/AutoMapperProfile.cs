using AutoMapper;
using WebApplication4.Dtos.Character;
using WebApplication4.Models;

namespace WebApplication4;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Character, GetCharacterDto>();
        CreateMap<AddCharacterDto, Character>();
    }
}