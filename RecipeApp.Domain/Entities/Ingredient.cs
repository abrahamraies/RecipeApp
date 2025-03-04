using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeApp.Domain.Entities
{
    [Table("ingredients")]
    public class Ingredient
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Unit { get; set; } = null!; // Unidad de medida

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = [];
        public ICollection<ShopList> ShopLists { get; set; } = [];
    }
}