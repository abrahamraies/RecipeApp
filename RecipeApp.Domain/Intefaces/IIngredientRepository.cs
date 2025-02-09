using RecipeApp.Domain.Entities;

namespace RecipeApp.Domain.Intefaces
{
    public interface IIngredientRepository
    {
        Task<int> CountAsync();
        Task<IEnumerable<Ingredient>> GetAllAsync(int pageNumber, int pageSize);
        Task<Ingredient?> GetByIdAsync(int id);
        Task<Ingredient?> GetByNameAsync(string name);
        Task AddAsync(Ingredient ingredient);
        Task UpdateAsync(Ingredient ingredient);
    }
}
