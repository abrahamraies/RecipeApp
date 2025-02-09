using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Domain.Entities
{
    public class Favorite
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required]
        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}