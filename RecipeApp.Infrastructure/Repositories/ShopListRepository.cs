using Microsoft.EntityFrameworkCore;
using RecipeApp.Domain.Entities;
using RecipeApp.Domain.Intefaces;
using RecipeApp.Infrastructure.Data;

namespace RecipeApp.Infrastructure.Repositories;

public class ShopListRepository(AppDbContext context) : IShopListRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<ShopList>> GetByUserIdAsync(int userId)
        => await _context.ShopLists.Where(s => s.UserId == userId).Include(s => s.Ingredient).ToListAsync();

    public async Task AddAsync(ShopList shopItem)
    {
        _context.ShopLists.Add(shopItem);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(int shopItemId)
    {
        var shopItem = await _context.ShopLists.FindAsync(shopItemId);
        if (shopItem != null)
        {
            _context.ShopLists.Remove(shopItem);
            await _context.SaveChangesAsync();
        }
    }
}
