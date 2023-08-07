﻿using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RolePlayGame.Data;
using RolePlayGame.Dtos.Character;
using RolePlayGame.Dtos.Weapon;
using RolePlayGame.Models;

namespace RolePlayGame.Services;

public class WeaponService : IWeaponService
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public WeaponService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeponDto newWeapon)
    {
        ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();

        try
        {
            Character character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId &&
                c.User.Id ==
                int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
            
            if (character == null)
            {
                response.Success = false;
                response.Message = "Character not found.";
                return response;
            }

            Weapon weapon = new Weapon
            {
                Name = newWeapon.Name,
                Damage = newWeapon.Damage,
                Character = character
            };

            await _context.Weapons.AddAsync(weapon);
            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<GetCharacterDto>(character);
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }
}