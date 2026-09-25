using System.ComponentModel.DataAnnotations;

namespace BDFurniture_Web.Models
{
    public class NewsletterSubscriber
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string? Name { get; set; }

        [Required, MaxLength(200), EmailAddress]
        public string Email { get; set; } = default!;

        public bool IsActive { get; set; } = true;
        public DateTime SubscribedAt { get; set; } = DateTime.Now;
    }
}
