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

    public async Task UpdateIngredientAsync(Ingredient ingredient)
    {
        var existingIngredient = await _ingredientRepository.GetByIdAsync(ingredient.Id);
        if (existingIngredient == null)
        {
            throw new InvalidOperationException("Ingredient not found.");
        }
        await _ingredientRepository.UpdateAsync(ingredient);
    }
}
