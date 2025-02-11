namespace RecipeApp.Application.DTOs.Favorite
{
    public class FavoriteDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public RecipeDto? Recipe { get; set; }
        public DateTime AddedAt { get; set; }
    }

    public class RecipeDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? RecipeUrl { get; set; }
    }
}
