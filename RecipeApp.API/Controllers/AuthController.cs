using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using RecipeApp.Application.Interfaces;
using RecipeApp.Application.Services;
using RecipeApp.Domain.Entities;

namespace RecipeApp.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IUserService userService, IAuthService authService) : ControllerBase
    {
        private readonly IUserService _userservice = userService;
        private readonly IAuthService _authService = authService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var existingUser = await _userservice.GetUserByEmailAsync(user.Email);
            if (existingUser != null)
                return BadRequest("User already exists");

            user.PasswordHash = _authService.HashPassword(user.PasswordHash);
            await _userservice.RegisterUserAsync(user);

            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.AuthenticateAsync(request.Email, request.Password);
            if (token == null)
            {
                return Unauthorized("Invalid credentials");
            }

            return Ok(new { Token = token });
        }
    }
}