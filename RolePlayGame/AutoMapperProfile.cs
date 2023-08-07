using AutoMapper;
using RolePlayGame.Dtos.Character;
using RolePlayGame.Models;

namespace RolePlayGame;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Character, GetCharacterDto>();
        CreateMap<AddCharacterDto, Character>();
    }
}