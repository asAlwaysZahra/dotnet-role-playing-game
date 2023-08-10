using RolePlayGame.Dtos.Fight;
using RolePlayGame.Models;

namespace RolePlayGame.Services.FightService;

public interface IFightService
{
    Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request);
}