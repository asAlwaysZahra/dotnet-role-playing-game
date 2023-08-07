using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using RolePlayGame.Models;

namespace RolePlayGame.Data;

public class AuthRepository : IAuthRepository
{
    private readonly DataContext _context;
    private readonly IConfiguration _configuration;

    public AuthRepository(DataContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<ServiceResponse<int>> Register(User user, string password)
    {
        ServiceResponse<int> response = new ServiceResponse<int>();

        if (await UserExists(user.Username))
        {
            response.Success = false;
            response.Message = "User already exists.";
            return response;
        }

        CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

        user.PasswordSalt = passwordSalt;
        user.PasswordHash = passwordHash;

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        response.Data = user.Id;
        return response;
    }

    public async Task<ServiceResponse<string>> Login(string username, string password)
    {
        ServiceResponse<string> response = new ServiceResponse<string>();
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username.ToLower().Equals(username.ToLower()));

        if (user == null)
        {
            response.Success = false;
            response.Message = "User not found.";
        }
        else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        {
            response.Success = false;
            response.Message = "Wrong password.";
        }
        else
        {
            response.Data = CreateToken(user);
        }

        return response;
    }

    public async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(
            u => u.Username.ToLower().Equals(username.ToLower())
        );
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != passwordHash[i])
                    return false;
            }

            return true;
        }
    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };
        
        SymmetricSecurityKey key =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = credentials
        };

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}