using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BDFurniture_Web.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        [Display(Name = "Category Name")]
        public string Name { get; set; } = default!;

        [Required, MaxLength(100)]
        public string Slug { get; set; } = default!;

        [MaxLength(500)]
        public string? Description { get; set; }

        [MaxLength(300)]
        public string? ImagePath { get; set; }

        [MaxLength(100)]
        public string? IconClass { get; set; }

        public int SortOrder { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
    }
}
