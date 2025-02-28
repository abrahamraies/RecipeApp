using RecipeApp.Domain.Entities;

namespace RecipeApp.Domain.Intefaces
{
    public interface IFavoriteRepository
    {
        Task<IEnumerable<Favorite>> GetByUserIdAsync(int userId);
        Task AddAsync(Favorite favorite);
        Task RemoveAsync(int userId, int recipeId);
        Task<bool> IsFavoriteAsync(int userId, int recipeId);
    }
}
