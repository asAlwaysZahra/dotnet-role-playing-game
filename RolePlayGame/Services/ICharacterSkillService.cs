using RolePlayGame.Dtos.Character;
using RolePlayGame.Dtos.CharacterSkill;
using RolePlayGame.Models;

namespace RolePlayGame.Services;

public interface ICharacterSkillService
{
    Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill);
}