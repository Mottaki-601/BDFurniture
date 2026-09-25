using System.ComponentModel.DataAnnotations;

namespace BDFurniture_Web.Models
{
    public class YouTubeVideo
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } = default!;

        [Required, MaxLength(50)]
        [Display(Name = "YouTube Video ID")]
        public string YouTubeId { get; set; } = default!;

        [MaxLength(300)]
        public string? ThumbnailUrl { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public int SortOrder { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
