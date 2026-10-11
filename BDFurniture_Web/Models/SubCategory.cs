using System.ComponentModel.DataAnnotations;

namespace BDFurniture_Web.Models
{
    public class SubCategory
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }

        [Required, MaxLength(100)]
        [Display(Name = "Sub Category Name")]
        public string Name { get; set; } = default!;

        [Required, MaxLength(100)]
        public string Slug { get; set; } = default!;

        [MaxLength(500)]
        public string? Description { get; set; }

        [MaxLength(300)]
        public string? ImagePath { get; set; }

        [MaxLength(300)]
        [Display(Name = "Hero Cover Image")]
        public string? CoverImagePath { get; set; }

        public int SortOrder { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        public Category Category { get; set; } = default!;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
