using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeApp.Domain.Entities
{
    [Table("shop_list")]
    public class ShopList
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required]
        public int IngredientId { get; set; }
        public Ingredient? Ingredient { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    }
}