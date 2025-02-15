using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RecipeApp.Application.DTOs.Ingredients;
using RecipeApp.Application.Interfaces;
using RecipeApp.Domain.Entities;

namespace RecipeApp.Api.Controllers;

[ApiController]
[Route("api/ingredients")]
public class IngredientsController(IIngredientService ingredientService, IMemoryCache cache) : ControllerBase
{
    private readonly IIngredientService _ingredientService = ingredientService;
    private readonly IMemoryCache _cache = cache;

    [HttpGet]
    public async Task<IActionResult> GetIngredients([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var cacheKey = $"Ingredients_{pageNumber}_{pageSize}";
        if (!_cache.TryGetValue(cacheKey, out var ingredients))
        {
            ingredients = await _ingredientService.GetAllIngredientsAsync(pageNumber, pageSize);
            _cache.Set(cacheKey, ingredients, TimeSpan.FromDays(10));
        }

        return Ok(ingredients);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetIngredient(int id)
    {
        var ingredient = await _ingredientService.GetIngredientByIdAsync(id);
        if (ingredient == null) return NotFound("Ingredient not found");

        return Ok(ingredient);
    }

    [HttpPost]
    public async Task<IActionResult> AddIngredient([FromBody] Ingredient ingredient)
    {
        await _ingredientService.AddIngredientAsync(ingredient);
        return CreatedAtAction(nameof(GetIngredient), new { id = ingredient.Id }, ingredient);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateIngredient(int id, [FromBody] IngredientUpdateDto ingredientDto)
    {
        await _ingredientService.UpdateIngredientAsync(id, ingredientDto);
        return NoContent();
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchIngredients([FromQuery] string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return BadRequest("The name cannot be empty.");
        }

        var ingredients = await _ingredientService.SearchIngredients(name);
        return Ok(ingredients);
    }

    [HttpGet("autocomplete")]
    public async Task<IActionResult> AutocompleteIngredients([FromQuery] string name)
    {
        var ingredients = await _ingredientService.AutocompleteAsync(name);
        return Ok(ingredients);
    }
}
