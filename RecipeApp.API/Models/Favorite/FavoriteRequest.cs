namespace RecipeApp.API.Models.Favorite
{
    public class FavoriteRequest
    {
        public int UserId { get; set; }
        public int RecipeId { get; set; }
    }
}
