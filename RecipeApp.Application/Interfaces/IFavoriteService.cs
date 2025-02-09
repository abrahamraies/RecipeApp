using RecipeApp.Domain.Entities;

namespace RecipeApp.Application.Interfaces;

public interface IFavoriteService
{
    Task<IEnumerable<Favorite>> GetUserFavoritesAsync(int userId);
    Task AddFavoriteAsync(int userId, int recipeId);
    Task RemoveFavoriteAsync(int favoriteId);
}
