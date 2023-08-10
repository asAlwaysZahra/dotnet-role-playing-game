using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RolePlayGame.Dtos.CharacterSkill;
using RolePlayGame.Services;

namespace RolePlayGame.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CharacterSkillController : ControllerBase
{
    private readonly ICharacterSkillService _characterSkillService;

    public CharacterSkillController(ICharacterSkillService characterSkillService)
    {
        _characterSkillService = characterSkillService;
    }

    [HttpPost]
    public async Task<IActionResult> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
    {
        return Ok(await _characterSkillService.AddCharacterSkill(newCharacterSkill));
    }
}