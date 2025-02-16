using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using RecipeApp.Application.DTOs.User;
using RecipeApp.Application.Interfaces;
using RecipeApp.Infrastructure.Security;

namespace RecipeApp.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(IUserService userService, PasswordHasher passwordHasher, IEmailService emailService) : ControllerBase
{
    private readonly IUserService _userService = userService;
    private readonly PasswordHasher _passwordHasher = passwordHasher;
    private readonly IEmailService _emailService = emailService;

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound("User not found");

        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto userDto)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound("User not found");

        user.Name = userDto.Name;
        user.UpdatedAt = DateTime.UtcNow;

        await _userService.UpdateUserAsync(user);
        return NoContent();
    }

    [HttpPut("{id}/email")]
    public async Task<IActionResult> UpdateEmail(int id, [FromBody] UpdateEmailDto emailDto)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound("User not found");

        user.Email = emailDto.NewEmail;
        user.UpdatedAt = DateTime.UtcNow;

        await _userService.UpdateUserAsync(user);
        return NoContent();
    }

    [HttpPut("{id}/password")]
    public async Task<IActionResult> UpdatePassword(int id, [FromBody] UpdatePasswordDto passwordDto)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound("User not found");

        if (!_passwordHasher.Verify(user.PasswordHash, passwordDto.CurrentPassword))
        {
            return BadRequest("Incorrect current password.");
        }

        user.PasswordHash = _passwordHasher.Hash(passwordDto.NewPassword);
        user.UpdatedAt = DateTime.UtcNow;

        await _userService.UpdateUserAsync(user);
        return NoContent();
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

        var resetLink = $"https://yourfrontend.com/reset-password?token={token}";
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
