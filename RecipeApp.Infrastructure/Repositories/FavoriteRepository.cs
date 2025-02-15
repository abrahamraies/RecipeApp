using Microsoft.EntityFrameworkCore;
using RecipeApp.Domain.Entities;
using RecipeApp.Domain.Intefaces;
using RecipeApp.Infrastructure.Data;

namespace RecipeApp.Infrastructure.Repositories;

public class FavoriteRepository(AppDbContext context) : IFavoriteRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Favorite>> GetByUserIdAsync(int userId)
        => await _context.Favorites.Where(f => f.UserId == userId).Include(f => f.Recipe).ToListAsync();

    public async Task AddAsync(Favorite favorite)
    {
        _context.Favorites.Add(favorite);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(int favoriteId)
    {
        var favorite = await _context.Favorites.FindAsync(favoriteId);
        if (favorite != null)
        {
            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> IsFavoriteAsync(int userId, int recipeId)
    {
        return await _context.Favorites
            .AnyAsync(f => f.UserId == userId && f.RecipeId == recipeId);
    }
}
