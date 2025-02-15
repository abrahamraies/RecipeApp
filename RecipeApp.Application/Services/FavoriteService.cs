using RecipeApp.Application.DTOs.Favorite;
using RecipeApp.Application.Interfaces;
using RecipeApp.Domain.Entities;
using RecipeApp.Domain.Intefaces;

namespace RecipeApp.Application.Services;

public class FavoriteService(IFavoriteRepository favoriteRepository) : IFavoriteService
{
    private readonly IFavoriteRepository _favoriteRepository = favoriteRepository;

    public async Task<IEnumerable<FavoriteDto>> GetUserFavoritesAsync(int userId)
    {
        var favorites = await _favoriteRepository.GetByUserIdAsync(userId);
        return favorites.Select(f => new FavoriteDto
        {
            Id = f.Id,
            UserId = f.UserId,
            Recipe = new RecipeDto
            {
                Id = f.Recipe.Id,
                Title = f.Recipe.Title,
                Description = f.Recipe.Description,
                ImageUrl = f.Recipe.ImageUrl,
                RecipeUrl = f.Recipe.RecipeUrl
            },
            AddedAt = f.AddedAt
        }).ToList();
    }

    public async Task AddFavoriteAsync(int userId, int recipeId)
    {
        var favorite = new Favorite { UserId = userId, RecipeId = recipeId };
        await _favoriteRepository.AddAsync(favorite);
    }

    public async Task RemoveFavoriteAsync(int favoriteId)
        => await _favoriteRepository.RemoveAsync(favoriteId);

    public async Task<bool> IsFavoriteAsync(int userId, int recipeId)
        => await _favoriteRepository.IsFavoriteAsync(userId, recipeId);
}
