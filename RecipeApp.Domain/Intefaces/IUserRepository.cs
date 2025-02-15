using RecipeApp.Domain.Entities;

namespace RecipeApp.Domain.Intefaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task<User?> GetUserByVerificationTokenAsync(string token);
    }
}
