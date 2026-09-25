using System.ComponentModel.DataAnnotations;

namespace BDFurniture_Web.Models
{
    public class ProductReview
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        [MaxLength(450)]
        public string? UserId { get; set; }

        [MaxLength(100)]
        public string? ReviewerName { get; set; }

        [Required, MaxLength(1000)]
        public string ReviewText { get; set; } = default!;

        public bool IsApproved { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        public Product Product { get; set; } = default!;
    }
}
