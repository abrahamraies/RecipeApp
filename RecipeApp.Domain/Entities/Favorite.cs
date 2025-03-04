using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeApp.Domain.Entities
{
    [Table("favorites")]
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