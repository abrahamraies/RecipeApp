using RecipeApp.Application.DTOs.Ingredients;
using RecipeApp.Application.Interfaces;
using RecipeApp.Domain.Entities;
using RecipeApp.Domain.Intefaces;

namespace RecipeApp.Application.Services;

public class IngredientService(IIngredientRepository ingredientRepository) : IIngredientService
{
    private readonly IIngredientRepository _ingredientRepository = ingredientRepository;

    public async Task<PagedResponse<Ingredient>> GetAllIngredientsAsync(int pageNumber, int pageSize)
    {
        var totalRecords = await _ingredientRepository.CountAsync();
        var ingredients = await _ingredientRepository.GetAllAsync(pageNumber, pageSize);
        return new PagedResponse<Ingredient>(ingredients, pageNumber, pageSize, totalRecords);
    }

    public async Task<Ingredient?> GetIngredientByIdAsync(int id)
        => await _ingredientRepository.GetByIdAsync(id);

    public async Task AddIngredientAsync(Ingredient ingredient)
    {
        var existingIngredient = await _ingredientRepository.GetByNameAsync(ingredient.Name);
        if (existingIngredient != null)
        {
            throw new InvalidOperationException("Ingredient already exists.");
        }
        await _ingredientRepository.AddAsync(ingredient);
    }

    public async Task UpdateIngredientAsync(int id, IngredientUpdateDto ingredientDto)
    {
        var existingIngredient = await _ingredientRepository.GetByIdAsync(id);
        if (existingIngredient == null)
        {
            throw new InvalidOperationException("Ingredient not found.");
        }

        existingIngredient.Name = ingredientDto.Name ?? existingIngredient.Name;
        existingIngredient.Unit = ingredientDto.Unit ?? existingIngredient.Unit;

        await _ingredientRepository.UpdateAsync(existingIngredient);
    }

    public async Task<List<IngredientResponse>> SearchIngredients(string query)
    {
        var ingredients = await _ingredientRepository.SearchIngredientsAsync(query);

        return ingredients.Select(i => new IngredientResponse
        {
            Id = i.Id,
            Name = i.Name
        }).ToList();
    }

    public async Task<IEnumerable<string>> AutocompleteAsync(string query)
        => await _ingredientRepository.AutocompleteAsync(query);
}
