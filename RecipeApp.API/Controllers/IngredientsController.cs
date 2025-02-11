using Microsoft.AspNetCore.Mvc;
using RecipeApp.Application.DTOs.Ingredients;
using RecipeApp.Application.Interfaces;
using RecipeApp.Domain.Entities;

namespace RecipeApp.Api.Controllers;

[ApiController]
[Route("api/ingredients")]
public class IngredientsController(IIngredientService ingredientService) : ControllerBase
{
    private readonly IIngredientService _ingredientService = ingredientService;

    [HttpGet]
    public async Task<IActionResult> GetIngredients([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var pagedResponse = await _ingredientService.GetAllIngredientsAsync(pageNumber, pageSize);
        return Ok(pagedResponse);
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
}
