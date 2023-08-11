using AutoMapper;
using RolePlayGame.Dtos.Character;
using RolePlayGame.Dtos.Fight;
using RolePlayGame.Dtos.Skill;
using RolePlayGame.Dtos.Weapon;
using RolePlayGame.Models;

namespace RolePlayGame;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Character, GetCharacterDto>()
            .ForMember(dto => dto.Skills,
                c => c.MapFrom(ch => ch.CharacterSkills.Select(cs => cs.Skill)));
        CreateMap<AddCharacterDto, Character>();
        CreateMap<Weapon, GetWeaponDto>();
        CreateMap<Skill, GetSkillDto>();
        CreateMap<Character, HighScoreDto>();
    }
}