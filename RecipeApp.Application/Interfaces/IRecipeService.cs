using RecipeApp.Application.DTOs.Recipe;
using RecipeApp.Domain.Entities;

namespace RecipeApp.Application.Interfaces;

public interface IRecipeService
{
    Task<PagedResponse<RecipeDto>> GetAllRecipesAsync(int pageNumber, int pageSize);
    Task<RecipeDto?> GetRecipeByIdAsync(int id);
    Task<IEnumerable<Recipe>> SearchRecipesByIngredientsAsync(List<int> ingredientIds);
}
