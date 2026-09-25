using System.ComponentModel.DataAnnotations;

namespace BDFurniture_Web.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = default!;

        [Required, MaxLength(200), EmailAddress]
        public string Email { get; set; } = default!;

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [MaxLength(200)]
        public string? Subject { get; set; }

        [Required, MaxLength(2000)]
        public string Message { get; set; } = default!;

        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
