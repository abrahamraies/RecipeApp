using RecipeApp.Domain.Entities;

namespace RecipeApp.Domain.Intefaces
{
    public interface IShopListRepository
    {
        Task<IEnumerable<ShopList>> GetByUserIdAsync(int userId);
        Task AddAsync(ShopList shopItem);
        Task RemoveAsync(int shopItemId);
    }

}
