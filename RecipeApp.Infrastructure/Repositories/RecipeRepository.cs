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

    public async Task<IEnumerable<Recipe>> GetRecipesWithIngredientsAsync(List<int> ingredientIds)
    {
        int ingredientCount = ingredientIds.Count;

        var recipeIds = await _context.RecipeIngredient
        .Where(ri => ingredientIds.Contains(ri.IngredientId))
        .GroupBy(ri => ri.RecipeId)
        .Where(g => g.Select(ri => ri.IngredientId).Distinct().Count() == ingredientCount)
        .Select(g => g.Key)
        .ToListAsync();

        return await _context.Recipes
            .Where(r => recipeIds.Contains(r.Id))
            .ToListAsync();
    }

    public async Task AddAsync(Recipe recipe)
    {
        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();
    }
}
