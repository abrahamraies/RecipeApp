using RecipeApp.Domain.Entities;

namespace RecipeApp.Application.Interfaces;

public interface IShopListService
{
    Task<IEnumerable<ShopList>> GetUserShopListAsync(int userId);
    Task AddIngredientToShopListAsync(int userId, int ingredientId);
    Task RemoveIngredientFromShopListAsync(int shopItemId);
}
