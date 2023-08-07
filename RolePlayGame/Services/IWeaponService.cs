using RolePlayGame.Dtos.Character;
using RolePlayGame.Dtos.Weapon;
using RolePlayGame.Models;

namespace RolePlayGame.Services;

public interface IWeaponService
{
    Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeponDto newWeapon);
}