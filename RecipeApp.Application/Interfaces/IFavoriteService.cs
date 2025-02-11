using RecipeApp.Application.DTOs.Favorite;
using RecipeApp.Domain.Entities;

namespace RecipeApp.Application.Interfaces;

public interface IFavoriteService
{
    Task<IEnumerable<FavoriteDto>> GetUserFavoritesAsync(int userId);
    Task AddFavoriteAsync(int userId, int recipeId);
    Task RemoveFavoriteAsync(int favoriteId);
}
