using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Application.DTOs.User
{
    public class UpdatePasswordDto
    {
        [Required]
        public string CurrentPassword { get; set; } = null!;

        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; } = null!;
    }
}
