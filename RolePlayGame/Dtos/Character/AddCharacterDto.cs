using WebApplication4.Models;

namespace WebApplication4.Dtos.Character;

public class AddCharacterDto
{
    public string Name { get; set; } = "na";
    public int HitPoints { get; set; } = 100;
    public int Strength { get; set; } = 10;
    public int Defence { get; set; } = 10;
    public int Intelligence { get; set; } = 10;
    public RpgClass Class { get; set; } = RpgClass.Mage;
}