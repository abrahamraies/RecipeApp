using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using RecipeApp.Application.Interfaces;
using RecipeApp.Application.Services;
using RecipeApp.Domain.Entities;

namespace RecipeApp.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IUserService userService, IAuthService authService, IEmailService emailService) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly IAuthService _authService = authService;
        private readonly IEmailService _emailService = emailService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var existingUser = await _userService.GetUserByEmailAsync(user.Email);
            if (existingUser != null)
                return BadRequest("User already exists");

            user.PasswordHash = _authService.HashPassword(user.PasswordHash);
            await _userService.RegisterUserAsync(user);

            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.AuthenticateAsync(request.Email, request.Password);
            if (result.Token == null || result.User == null)
            {
                return Unauthorized("Invalid credentials");
            }

            return Ok(new
            {
                result.Token,
                User = new
                {
                    result.User.Id,
                    result.User.Name,
                    result.User.Email
                }
            });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var user = await _userService.GetUserByEmailAsync(request.Email);
            if (user == null) return NotFound("User not found");

            var token = Guid.NewGuid().ToString();
            user.VerificationToken = token;
            user.VerificationTokenExpires = DateTime.UtcNow.AddHours(1); // Token válido por 1 hora
            await _userService.UpdateUserAsync(user);

            var resetLink = $"https://recipes-app01.netlify.app//reset-password?token={token}";
            await _emailService.SendEmailAsync(user.Email, "Password Reset", $"Click here to reset your password: {resetLink}");

            return Ok("Password reset email sent");
        }

        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string token)
        {
            var user = await _userService.GetUserByVerificationTokenAsync(token);
            if (user == null || user.VerificationTokenExpires < DateTime.UtcNow) return BadRequest("Invalid or expired token");

            user.IsEmailVerified = true;
            user.VerificationToken = null;
            user.VerificationTokenExpires = null;
            await _userService.UpdateUserAsync(user);

            return Ok("Email verified successfully");
        }
    }
}