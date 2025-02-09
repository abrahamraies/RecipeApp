using Microsoft.AspNetCore.Mvc;
using RecipeApp.Application.Interfaces;
using RecipeApp.Domain.Entities;

namespace RecipeApp.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound("User not found");

        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
    {
        if (id != user.Id) return BadRequest("User ID mismatch");

        await _userService.UpdateUserAsync(user);
        return NoContent();
    }
}
