using RecipeApp.Application.Interfaces;
using RecipeApp.Domain.Entities;
using RecipeApp.Domain.Intefaces;

namespace RecipeApp.Application.Services;

public class FavoriteService(IFavoriteRepository favoriteRepository) : IFavoriteService
{
    private readonly IFavoriteRepository _favoriteRepository = favoriteRepository;

    public async Task<IEnumerable<Favorite>> GetUserFavoritesAsync(int userId)
        => await _favoriteRepository.GetByUserIdAsync(userId);

    public async Task AddFavoriteAsync(int userId, int recipeId)
    {
        var favorite = new Favorite { UserId = userId, RecipeId = recipeId };
        await _favoriteRepository.AddAsync(favorite);
    }

    public async Task RemoveFavoriteAsync(int favoriteId)
        => await _favoriteRepository.RemoveAsync(favoriteId);
}
