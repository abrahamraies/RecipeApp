using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeApp.Domain.Entities
{
    [Table("users")]
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; } = null!; // Para usuarios que se registran sin OAuth

        [MaxLength(100)]
        public string? GoogleId { get; set; } // Para usuarios que se registran con Google

        // Para enviar un correo de verificacion, y para resetear la contraseña.
        public string? VerificationToken { get; set; }
        public DateTime? VerificationTokenExpires { get; set; }
        public bool IsEmailVerified { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Favorite> Favorites { get; set; } = [];
        public ICollection<ShopList> ShopLists { get; set; } = [];
    }
}