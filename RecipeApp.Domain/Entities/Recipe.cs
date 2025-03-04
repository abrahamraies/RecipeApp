using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeApp.Domain.Entities
{
    [Table("recipes")]
    public class Recipe
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = null!;

        [Required]
        public string Instructions { get; set; } = null!;

        [Required]
        [Range(1, 1440)] // Tiempo en minutos (1 minuto a 24 horas)
        public int PreparationTime { get; set; }

        public string? ImageUrl { get; set; }
        public string? RecipeUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = [];
        public ICollection<Favorite> Favorites { get; set; } = [];
    }
}