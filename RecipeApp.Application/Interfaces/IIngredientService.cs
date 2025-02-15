using RecipeApp.Application.DTOs.Ingredients;
using RecipeApp.Domain.Entities;

namespace RecipeApp.Application.Interfaces
{
    public interface IIngredientService
    {
        Task<PagedResponse<Ingredient>> GetAllIngredientsAsync(int pageNumber, int pageSize);
        Task<Ingredient?> GetIngredientByIdAsync(int id);
        Task AddIngredientAsync(Ingredient ingredient);
        Task UpdateIngredientAsync(int id, IngredientUpdateDto ingredientDto);
        Task<List<IngredientResponse>> SearchIngredients(string query);
        Task<IEnumerable<string>> AutocompleteAsync(string query);
    }
}
