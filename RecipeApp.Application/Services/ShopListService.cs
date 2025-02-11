using RecipeApp.Application.DTOs;
using RecipeApp.Application.DTOs.ShopList;
using RecipeApp.Application.Interfaces;
using RecipeApp.Domain.Entities;
using RecipeApp.Domain.Intefaces;

namespace RecipeApp.Application.Services;

public class ShopListService(IShopListRepository shopListRepository) : IShopListService
{
    private readonly IShopListRepository _shopListRepository = shopListRepository;

    public async Task<IEnumerable<ShopListDto>> GetUserShopListAsync(int userId)
    {
        var shopList = await _shopListRepository.GetByUserIdAsync(userId);
        return shopList.Select(s => new ShopListDto
        {
            Id = s.Id,
            UserId = s.UserId,
            Ingredient = new IngredientDto
            {
                Id = s.Ingredient.Id,
                Name = s.Ingredient.Name,
                Unit = s.Ingredient.Unit
            },
        }).ToList();
    }

    public async Task AddIngredientToShopListAsync(int userId, int ingredientId)
    {
        var existingItem = await _shopListRepository.GetByUserIdAsync(userId);
        if (existingItem.FirstOrDefault(s => s.IngredientId == ingredientId) != null)
        {
            throw new InvalidOperationException("Ingredient already in shop list.");
        }

        var shopItem = new ShopList { UserId = userId, IngredientId = ingredientId };
        await _shopListRepository.AddAsync(shopItem);
    }

    public async Task RemoveIngredientFromShopListAsync(int shopItemId)
        => await _shopListRepository.RemoveAsync(shopItemId);
}
