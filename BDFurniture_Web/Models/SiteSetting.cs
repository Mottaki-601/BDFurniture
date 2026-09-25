using System.ComponentModel.DataAnnotations;

namespace BDFurniture_Web.Models
{
    public class SiteSetting
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string SettingKey { get; set; } = default!;

        public string? SettingValue { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
