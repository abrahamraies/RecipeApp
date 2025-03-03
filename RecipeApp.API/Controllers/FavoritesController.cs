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

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddFavorite([FromBody] FavoriteRequest request)
    {
        await _favoriteService.AddFavoriteAsync(request.UserId, request.RecipeId);
        var updatedFavorites = await _favoriteService.GetUserFavoritesAsync(request.UserId);
        return Ok(updatedFavorites);
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> RemoveFavorite([FromQuery] int userId, [FromQuery] int recipeId)
    {
        await _favoriteService.RemoveFavoriteAsync(userId, recipeId);
        var updatedFavorites = await _favoriteService.GetUserFavoritesAsync(userId);
        return Ok(updatedFavorites);
    }

    [Authorize]
    [HttpGet("check")]
    public async Task<IActionResult> CheckFavorite([FromQuery] int userId, [FromQuery] int recipeId)
    {
        var isFavorite = await _favoriteService.IsFavoriteAsync(userId, recipeId);
        return Ok(new { IsFavorite = isFavorite });
    }
}
