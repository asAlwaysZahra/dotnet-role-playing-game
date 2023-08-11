using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RolePlayGame.Dtos.Character;
using RolePlayGame.Models;
using RolePlayGame.Services;

namespace RolePlayGame.Controllers;

[Authorize(Roles = "Player,Admin")]
[ApiController]
[Route("[controller]")]
public class CharacterController : ControllerBase
{
    private readonly ICharacterService _characterService;

    public CharacterController(ICharacterService characterService)
    {
        _characterService = characterService;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _characterService.GetAllCharacters());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingle(int id)
    {
        return Ok(await _characterService.GetCharacterById(id));
    }

    [HttpPost]
    public async Task<IActionResult> AddCharacter(AddCharacterDto newCharacter)
    {
        return Ok(await _characterService.AddCharacter(newCharacter));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCharacter(UpdateCharacterDto updateCharacter)
    {
        ServiceResponse<GetCharacterDto> response = await _characterService.UpdateCharacter(updateCharacter);

        if (response.Data == null)
        {
            return NotFound(response);
        }

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCharacter(int id)
    {
        ServiceResponse<List<GetCharacterDto>> response = await _characterService.DeleteCharacter(id);

        if (response.Data == null)
        {
            return NotFound(response);
        }

        return Ok(response);
    }
}