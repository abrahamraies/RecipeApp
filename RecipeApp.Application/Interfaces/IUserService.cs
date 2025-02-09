using RecipeApp.Domain.Entities;

namespace RecipeApp.Application.Interfaces;

public interface IUserService
{
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> GetUserByEmailAsync(string email);
    Task RegisterUserAsync(User user);
    Task UpdateUserAsync(User user);
}
