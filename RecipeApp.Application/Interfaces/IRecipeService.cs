using RecipeApp.Domain.Entities;

namespace RecipeApp.Application.Interfaces;

public interface IRecipeService
{
    Task<PagedResponse<Recipe>> GetAllRecipesAsync(int pageNumber, int pageSize);
    Task<Recipe?> GetRecipeByIdAsync(int id);
    Task<IEnumerable<Recipe>> SearchRecipesByIngredientsAsync(List<int> ingredientIds);
}
