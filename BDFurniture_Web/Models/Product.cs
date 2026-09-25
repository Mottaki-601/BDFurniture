using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BDFurniture_Web.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please select a subcategory")]
        [Display(Name = "Subcategory")]
        public int SubCategoryId { get; set; }

        [Required(ErrorMessage = "Product name is required"), MaxLength(200)]
        [Display(Name = "Product Name")]
        public string Name { get; set; } = default!;

        [ValidateNever]
        [MaxLength(200)]
        public string Slug { get; set; } = string.Empty;

        [MaxLength(500)]
        [Display(Name = "Short Description")]
        public string? ShortDescription { get; set; }

        public string? Description { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Price")]
        public decimal? Price { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Discount Price")]
        public decimal? DiscountPrice { get; set; }

        [Required(ErrorMessage = "WhatsApp number is required"), MaxLength(20)]
        [Display(Name = "WhatsApp Number")]
        public string WhatsAppNumber { get; set; } = "+8801XXXXXXXXX";

        public bool IsFeatured { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public int ViewCount { get; set; } = 0;

        [Column(TypeName = "decimal(3,2)")]
        public decimal AverageRating { get; set; } = 0;
        public int TotalRatings { get; set; } = 0;

        [ValidateNever]
        public string? CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        [ValidateNever]
        public SubCategory? SubCategory { get; set; }

        [ValidateNever]
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();

        [ValidateNever]
        public ICollection<ProductRating> Ratings { get; set; } = new List<ProductRating>();

        [ValidateNever]
        public ICollection<ProductReview> Reviews { get; set; } = new List<ProductReview>();

        [ValidateNever]
        public ICollection<ProductInquiry> Inquiries { get; set; } = new List<ProductInquiry>();
    }
}

