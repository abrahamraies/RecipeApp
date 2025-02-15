using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RecipeApp.Application.Interfaces;

namespace RecipeApp.Api.Controllers;

[ApiController]
[Route("api/recipes")]
public class RecipesController(IRecipeService recipeService, IMemoryCache cache) : ControllerBase
{
    private readonly IRecipeService _recipeService = recipeService;
    private readonly IMemoryCache _cache = cache;

    [HttpGet]
    public async Task<IActionResult> GetRecipes([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var cacheKey = $"Recipes_{pageNumber}_{pageSize}";
        if (!_cache.TryGetValue(cacheKey, out var recipes))
        {
            recipes = await _recipeService.GetAllRecipesAsync(pageNumber, pageSize);
            _cache.Set(cacheKey, recipes, TimeSpan.FromDays(10));
        }

        return Ok(recipes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRecipe(int id)
    {
        var recipe = await _recipeService.GetRecipeByIdAsync(id);
        if (recipe == null) return NotFound("Recipe not found");

        return Ok(recipe);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchRecipes([FromQuery] List<int> ingredients)
    {
        if (ingredients == null || ingredients.Count == 0)
        {
            return BadRequest("Debe proporcionar al menos un ingrediente.");
        }

        var recipes = await _recipeService.SearchRecipesByIngredientsAsync(ingredients);

        return Ok(recipes);
    }
}
