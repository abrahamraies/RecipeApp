using Microsoft.AspNetCore.Mvc;
using RecipeApp.Application.DTOs.User;
using RecipeApp.Application.Interfaces;
using RecipeApp.Infrastructure.Security;

namespace RecipeApp.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly PasswordHasher _passwordHasher;

    public UsersController(IUserService userService, PasswordHasher passwordHasher)
    {
        _userService = userService;
        _passwordHasher = passwordHasher;
    }

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
}
