using RecipeApp.Application.Interfaces;
using RecipeApp.Domain.Entities;
using RecipeApp.Domain.Intefaces;

namespace RecipeApp.Application.Services;

public class RecipeService(IRecipeRepository recipeRepository) : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository = recipeRepository;

    public async Task<PagedResponse<Recipe>> GetAllRecipesAsync(int pageNumber, int pageSize)
    {
        var totalRecords = await _recipeRepository.CountAsync();
        var recipes = await _recipeRepository.GetAllAsync(pageNumber, pageSize);
        return new PagedResponse<Recipe>(recipes, pageNumber, pageSize, totalRecords);
    }

    public async Task<Recipe?> GetRecipeByIdAsync(int id)
        => await _recipeRepository.GetByIdAsync(id);

    public async Task<IEnumerable<Recipe>> SearchRecipesByIngredientsAsync(List<int> ingredientIds)
        => await _recipeRepository.SearchByIngredientsAsync(ingredientIds);
}
