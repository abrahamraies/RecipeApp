using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Application.DTOs.User
{
    public class UpdateUserDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
    }
}
