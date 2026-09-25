using System.ComponentModel.DataAnnotations;

namespace BDFurniture_Web.Models
{
    public class ProductInquiry
    {
        public int Id { get; set; }
        public int ProductId { get; set; }

        [MaxLength(200)]
        public string? ProductName { get; set; }

        [Required, MaxLength(100)]
        [Display(Name = "Your Name")]
        public string InquirerName { get; set; } = default!;

        [Required, MaxLength(200), EmailAddress]
        public string Email { get; set; } = default!;

        [MaxLength(20)]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [MaxLength(200)]
        public string? Subject { get; set; }

        [Required, MaxLength(1000)]
        public string Message { get; set; } = default!;

        public bool IsResolved { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        public Product Product { get; set; } = default!;
    }
}
