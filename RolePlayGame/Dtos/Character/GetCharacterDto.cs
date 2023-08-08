using RolePlayGame.Dtos.Skill;
using RolePlayGame.Dtos.Weapon;
using RolePlayGame.Models;

namespace RolePlayGame.Dtos.Character;

public class GetCharacterDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "Fara";
    public int HitPoints { get; set; } = 100;
    public int Strength { get; set; } = 10;
    public int Defence { get; set; } = 10;
    public int Intelligence { get; set; } = 10;
    public RpgClass Class { get; set; } = RpgClass.Cleric;
    public GetWeaponDto Weapon { get; set; }
    public List<GetSkillDto> Skills { get; set; }
}