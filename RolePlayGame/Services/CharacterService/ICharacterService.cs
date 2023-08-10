using RolePlayGame.Dtos.Character;
using RolePlayGame.Models;

namespace RolePlayGame.Services;

public interface ICharacterService
{
    Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();
    Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
    Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter);
    Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter);
    Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);
}