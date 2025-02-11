using Microsoft.EntityFrameworkCore;
using RecipeApp.Domain.Entities;
using RecipeApp.Domain.Intefaces;
using RecipeApp.Infrastructure.Data;

namespace RecipeApp.Infrastructure.Repositories;

public class RecipeRepository(AppDbContext context) : IRecipeRepository
{
    private readonly AppDbContext _context = context;

    public async Task<int> CountAsync()
    {
        return await _context.Recipes.CountAsync();
    }

    public async Task<IEnumerable<Recipe>> GetAllAsync(int pageNumber, int pageSize)
    {
        return await _context.Recipes
            .Include(r => r.RecipeIngredients)
            .ThenInclude(ri => ri.Ingredient)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Recipe?> GetByIdAsync(int id)
        => await _context.Recipes.Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient).FirstOrDefaultAsync(r => r.Id == id);

    public async Task<IEnumerable<Recipe>> SearchByIngredientsAsync(List<int> ingredientIds)
    {
        return await _context.Recipes
            .Include(r => r.RecipeIngredients)
            .ThenInclude(ri => ri.Ingredient)
            .Where(r => r.RecipeIngredients
                .Join(
                    ingredientIds,
                    ri => ri.IngredientId,
                    id => id,
                    (ri, id) => ri
                ).Any())
            .ToListAsync();
    }

    public async Task AddAsync(Recipe recipe)
    {
        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();
    }
}
