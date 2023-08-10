﻿using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RolePlayGame.Data;
using RolePlayGame.Dtos.Character;
using RolePlayGame.Dtos.CharacterSkill;
using RolePlayGame.Models;

namespace RolePlayGame.Services;

public class CharacterSkillService : ICharacterSkillService
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public CharacterSkillService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
    {
        ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();

        try
        {
            Character character =
                await _context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.CharacterSkills).ThenInclude(cs => cs.Skill)
                    .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId &&
                                              c.User.Id == int.Parse(_httpContextAccessor.HttpContext.User
                                                  .FindFirstValue(ClaimTypes.NameIdentifier)));
            if (character == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Character not found.";
                return serviceResponse;
            }

            Skill skill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);

            if (skill == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Skill not found.";
                return serviceResponse;
            }

            CharacterSkill characterSkill = new CharacterSkill
            {
                Character = character,
                Skill = skill
            };

            await _context.CharacterSkills.AddAsync(characterSkill);
            await _context.SaveChangesAsync();

            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }
}