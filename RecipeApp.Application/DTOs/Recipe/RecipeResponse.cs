namespace RecipeApp.Application.DTOs.Recipe
{
    public class RecipeResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string? RecipeUrl { get; set; }
    }
}
