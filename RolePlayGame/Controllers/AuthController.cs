using Microsoft.AspNetCore.Mvc;
using WebApplication4.Data;
using WebApplication4.Dtos.User;
using WebApplication4.Models;

namespace WebApplication4.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthRepository _authRepo;

    public AuthController(IAuthRepository authRepository)
    {
        _authRepo = authRepository;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(UserRegisterDto request)
    {
        User user = new User() { Username = request.Username };
        ServiceResponse<int> response = await _authRepo.Register(user, request.Password);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(UserLoginDto request)
    {
        ServiceResponse<string> response = await _authRepo.Login(request.Username, request.Password);
        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }
}