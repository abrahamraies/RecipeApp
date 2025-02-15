using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeApp.API.Models.Favorite;
using RecipeApp.Application.Interfaces;

namespace RecipeApp.Api.Controllers;

[ApiController]
[Route("api/favorites")]
public class FavoritesController(IFavoriteService favoriteService) : ControllerBase
{
    private readonly IFavoriteService _favoriteService = favoriteService;

    [Authorize]
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetFavorites(int userId)
    {
        var favorites = await _favoriteService.GetUserFavoritesAsync(userId);
        return Ok(favorites);
    }

    [HttpPost]
    public async Task<IActionResult> AddFavorite([FromBody] FavoriteRequest request)
    {
        await _favoriteService.AddFavoriteAsync(request.UserId, request.RecipeId);
        return Ok("Favorite added");
    }

    [HttpDelete("{favoriteId}")]
    public async Task<IActionResult> RemoveFavorite(int favoriteId)
    {
        await _favoriteService.RemoveFavoriteAsync(favoriteId);
        return Ok("Favorite removed");
    }

    [HttpGet("check")]
    public async Task<IActionResult> CheckFavorite([FromQuery] int userId, [FromQuery] int recipeId)
    {
        var isFavorite = await _favoriteService.IsFavoriteAsync(userId, recipeId);
        return Ok(new { IsFavorite = isFavorite });
    }
}
