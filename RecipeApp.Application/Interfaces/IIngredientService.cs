using RecipeApp.Domain.Entities;

namespace RecipeApp.Application.Interfaces
{
    public interface IIngredientService
    {
        Task<PagedResponse<Ingredient>> GetAllIngredientsAsync(int pageNumber, int pageSize);
        Task<Ingredient?> GetIngredientByIdAsync(int id);
        Task AddIngredientAsync(Ingredient ingredient);
        Task UpdateIngredientAsync(Ingredient ingredient);
    }
}
