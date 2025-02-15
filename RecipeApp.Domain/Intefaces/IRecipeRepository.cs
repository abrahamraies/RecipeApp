using RecipeApp.Domain.Entities;

namespace RecipeApp.Domain.Intefaces
{
    public interface IRecipeRepository
    {
        Task<int> CountAsync();
        Task<IEnumerable<Recipe>> GetAllAsync(int pageNumber, int pageSize);
        Task<Recipe?> GetByIdAsync(int id);
        Task<IEnumerable<Recipe>> GetRecipesWithIngredientsAsync(List<int> ingredientIds);
        Task AddAsync(Recipe recipe);
    }
}
