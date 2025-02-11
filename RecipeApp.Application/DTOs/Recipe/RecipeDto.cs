namespace RecipeApp.Application.DTOs.Recipe
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Instructions { get; set; }
        public int PreparationTime { get; set; }
        public string? ImageUrl { get; set; }
        public string? RecipeUrl { get; set; }
        public ICollection<RecipeIngredientDto> Ingredients { get; set; } = [];
    }

    public class RecipeIngredientDto
    {
        public int IngredientId { get; set; }
        public IngredientDto? Ingredient { get; set; }
        public decimal Quantity { get; set; }
    }

    public class IngredientDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Unit { get; set; }
    }
}