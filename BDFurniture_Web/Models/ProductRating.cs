using System.ComponentModel.DataAnnotations;

namespace BDFurniture_Web.Models
{
    public class ProductRating
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        [Required, MaxLength(450)]
        public string UserId { get; set; } = default!;

        [Range(1, 5)]
        public int Rating { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public Product Product { get; set; } = default!;
    }
}
