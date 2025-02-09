using Microsoft.AspNetCore.Mvc;
using RecipeApp.Application.Interfaces;

namespace RecipeApp.Api.Controllers;

[ApiController]
[Route("api/recipes")]
public class RecipesController(IRecipeService recipeService) : ControllerBase
{
    private readonly IRecipeService _recipeService = recipeService;

    [HttpGet]
    public async Task<IActionResult> GetRecipes([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var pagedResponse = await _recipeService.GetAllRecipesAsync(pageNumber, pageSize);
        return Ok(pagedResponse);
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
        var recipes = await _recipeService.SearchRecipesByIngredientsAsync(ingredients);

        return Ok(recipes);
    }
}
