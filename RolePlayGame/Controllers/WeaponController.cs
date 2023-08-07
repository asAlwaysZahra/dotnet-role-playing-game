using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RolePlayGame.Dtos.Weapon;
using RolePlayGame.Services;

namespace RolePlayGame.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class WeaponController : ControllerBase
{
    private readonly IWeaponService _weaponService;

    public WeaponController(IWeaponService weaponService)
    {
        _weaponService = weaponService;
    }

    [HttpPost]
    public async Task<IActionResult> AddWeapon(AddWeponDto newWeapon)
    {
        return Ok(await _weaponService.AddWeapon(newWeapon));
    }
}