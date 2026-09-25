using System.ComponentModel.DataAnnotations;

namespace BDFurniture_Web.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        [Required, MaxLength(300)]
        public string ImagePath { get; set; } = default!;

        [MaxLength(200)]
        public string? AltText { get; set; }
        public bool IsMain { get; set; } = false;
        public int SortOrder { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        public Product Product { get; set; } = default!;
    }
}
