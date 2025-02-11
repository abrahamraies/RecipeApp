using RecipeApp.Application.DTOs.ShopList;
using RecipeApp.Domain.Entities;

namespace RecipeApp.Application.Interfaces;

public interface IShopListService
{
    Task<IEnumerable<ShopListDto>> GetUserShopListAsync(int userId);
    Task AddIngredientToShopListAsync(int userId, int ingredientId);
    Task RemoveIngredientFromShopListAsync(int shopItemId);
}
