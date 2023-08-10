﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RolePlayGame.Dtos.Fight;
using RolePlayGame.Services.FightService;

namespace RolePlayGame.Controllers;

[ApiController]
[Route("[controller]")]
public class FightController : ControllerBase
{
    private readonly IFightService _fightService;

    public FightController(IFightService fightService)
    {
        _fightService = fightService;
    }

    [HttpPost("Weapon")]
    public async Task<IActionResult> WeaponAttack(WeaponAttackDto request)
    {
        return Ok(await _fightService.WeaponAttack(request));
    }
}