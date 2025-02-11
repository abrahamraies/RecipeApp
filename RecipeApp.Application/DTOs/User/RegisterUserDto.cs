using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Application.DTOs.User
{
    public class RegisterUserDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string Password { get; set; } = null!;
    }

}
