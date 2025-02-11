using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Application.DTOs.User
{
    public class UpdateEmailDto
    {
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string NewEmail { get; set; } = null!;
    }
}
