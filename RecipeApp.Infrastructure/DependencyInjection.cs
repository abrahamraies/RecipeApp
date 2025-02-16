using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using RecipeApp.Application.Interfaces;
using RecipeApp.Application.Services;
using RecipeApp.Domain.Intefaces;
using RecipeApp.Infrastructure.Repositories;
using RecipeApp.Infrastructure.Security;

namespace RecipeApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Registrar Servicios
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRecipeService, RecipeService>();
        services.AddScoped<IShopListService, ShopListService>();
        services.AddScoped<IFavoriteService, FavoriteService>();
        services.AddScoped<IIngredientService, IngredientService>();

        services.AddScoped<IEmailService, EmailService>();
        services.AddSingleton<IMemoryCache, MemoryCache>();

        // Security
        services.AddSingleton<PasswordHasher>();

        // Registrar Repositorios
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRecipeRepository, RecipeRepository>();
        services.AddScoped<IShopListRepository, ShopListRepository>();
        services.AddScoped<IFavoriteRepository, FavoriteRepository>();
        services.AddScoped<IIngredientRepository, IngredientRepository>();

        return services;
    }
}
