using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Application.DTOs.Ingredients
{
    public class IngredientUpdateDto
    {
        [MaxLength(100)]
        public string? Name { get; set; } = null!;

        [MaxLength(50)]
        public string? Unit { get; set; } = null!;
    }
}
