using Microsoft.EntityFrameworkCore;
using RecipeApp.Domain.Entities;
using RecipeApp.Domain.Intefaces;
using RecipeApp.Infrastructure.Data;

namespace RecipeApp.Infrastructure.Repositories;

public class IngredientRepository(AppDbContext context) : IIngredientRepository
{
    private readonly AppDbContext _context = context;

    public async Task<int> CountAsync()
    {
        return await _context.Ingredients.CountAsync();
    }

    public async Task<IEnumerable<Ingredient>> GetAllAsync(int pageNumber, int pageSize)
    {
        return await _context.Ingredients
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Ingredient?> GetByIdAsync(int id)
        => await _context.Ingredients.FindAsync(id);

    public async Task<Ingredient?> GetByNameAsync(string name)
    => await _context.Ingredients.FirstOrDefaultAsync(i => i.Name == name);

    public async Task AddAsync(Ingredient ingredient)
    {
        _context.Ingredients.Add(ingredient);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Ingredient ingredient)
    {
        _context.Ingredients.Update(ingredient);
        await _context.SaveChangesAsync();
    }
}
