namespace RecipeApp.Application.DTOs.ShopList
{
    public class ShopListDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public IngredientDto? Ingredient { get; set; }
    }

    public class IngredientDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Unit { get; set; }
    }
}
