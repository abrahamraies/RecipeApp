using RecipeApp.Application.DTOs.Recipe;
using RecipeApp.Application.Interfaces;
using RecipeApp.Domain.Intefaces;

namespace RecipeApp.Application.Services;

public class RecipeService(IRecipeRepository recipeRepository) : IRecipeService
{
    private readonly IRecipeRepository _recipeRepository = recipeRepository;

    public async Task<PagedResponse<RecipeDto>> GetAllRecipesAsync(int pageNumber, int pageSize)
    {
        var totalRecords = await _recipeRepository.CountAsync();
        var recipes = await _recipeRepository.GetAllAsync(pageNumber, pageSize);

        var recipeDtos = recipes.Select(r => new RecipeDto
        {
            Id = r.Id,
            Title = r.Title,
            Description = r.Description,
            Instructions = r.Instructions,
            PreparationTime = r.PreparationTime,
            ImageUrl = r.ImageUrl,
            RecipeUrl = r.RecipeUrl,
            Ingredients = r.RecipeIngredients.Select(ri => new RecipeIngredientDto
            {
                IngredientId = ri.IngredientId,
                Ingredient = new IngredientDto
                {
                    Id = ri.Ingredient.Id,
                    Name = ri.Ingredient.Name,
                    Unit = ri.Ingredient.Unit
                },
                Quantity = ri.Quantity
            }).ToList()
        }).ToList();

        return new PagedResponse<RecipeDto>(recipeDtos, pageNumber, pageSize, totalRecords);
    }

    public async Task<RecipeDto?> GetRecipeByIdAsync(int id)
    {
        var recipe = await _recipeRepository.GetByIdAsync(id);
        if (recipe == null) return null;

        return new RecipeDto
        {
            Id = recipe.Id,
            Title = recipe.Title,
            Description = recipe.Description,
            Instructions = recipe.Instructions,
            PreparationTime = recipe.PreparationTime,
            ImageUrl = recipe.ImageUrl,
            RecipeUrl = recipe.RecipeUrl,
            Ingredients = recipe.RecipeIngredients.Select(ri => new RecipeIngredientDto
            {
                IngredientId = ri.IngredientId,
                Ingredient = new IngredientDto
                {
                    Id = ri.Ingredient.Id,
                    Name = ri.Ingredient.Name,
                    Unit = ri.Ingredient.Unit
                },
                Quantity = ri.Quantity
            }).ToList()
        };
    }

    public async Task<IEnumerable<RecipeResponse>> SearchRecipesByIngredientsAsync(List<int> ingredientIds)
    {
        // Buscar recetas que contengan todos los ingredientes seleccionados
        var recipes = await _recipeRepository.GetRecipesWithIngredientsAsync(ingredientIds);

        return recipes.Select(r => new RecipeResponse
        {
            Id = r.Id,
            Title = r.Title,
            Description = r.Description,
            ImageUrl = r.ImageUrl,
            RecipeUrl = r.RecipeUrl
        }).ToList();
    }
}
