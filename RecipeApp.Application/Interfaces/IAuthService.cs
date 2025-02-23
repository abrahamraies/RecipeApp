using RecipeApp.Domain.Entities;

namespace RecipeApp.Application.Interfaces
{
    public interface IAuthService
    {
        Task<(string? Token, User? User)> AuthenticateAsync(string email, string password);
        string HashPassword(string password);
    }
}
