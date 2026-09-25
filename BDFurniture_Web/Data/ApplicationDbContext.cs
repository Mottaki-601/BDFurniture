using BDFurniture_Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BDFurniture_Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductRating> ProductRatings { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<ProductInquiry> ProductInquiries { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<NewsletterSubscriber> NewsletterSubscribers { get; set; }
        public DbSet<YouTubeVideo> YouTubeVideos { get; set; }
        public DbSet<SiteSetting> SiteSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Unique constraint: one rating per user per product
            builder.Entity<ProductRating>()
                .HasIndex(r => new { r.ProductId, r.UserId })
                .IsUnique();

            // Unique constraint: slug
            builder.Entity<Category>()
                .HasIndex(c => c.Slug).IsUnique();

            builder.Entity<SubCategory>()
                .HasIndex(s => s.Slug).IsUnique();

            builder.Entity<Product>()
                .HasIndex(p => p.Slug).IsUnique();

            // Unique: newsletter email
            builder.Entity<NewsletterSubscriber>()
                .HasIndex(n => n.Email).IsUnique();

            // Unique: SiteSetting key
            builder.Entity<SiteSetting>()
                .HasIndex(s => s.SettingKey).IsUnique();

            // Seed Categories
            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Bedroom", Slug = "bedroom", Description = "Complete bedroom furniture solutions", IconClass = "fas fa-bed", SortOrder = 1 },
                new Category { Id = 2, Name = "Living Room", Slug = "living-room", Description = "Elegant living room furniture", IconClass = "fas fa-couch", SortOrder = 2 },
                new Category { Id = 3, Name = "Dining Room", Slug = "dining-room", Description = "Stylish dining room furniture", IconClass = "fas fa-utensils", SortOrder = 3 },
                new Category { Id = 4, Name = "Home Decor", Slug = "home-decor", Description = "Beautiful home decoration items", IconClass = "fas fa-paint-brush", SortOrder = 4 },
                new Category { Id = 5, Name = "Interior Design", Slug = "interior-design", Description = "Professional interior design services", IconClass = "fas fa-drafting-compass", SortOrder = 5 },
                new Category { Id = 6, Name = "Others", Slug = "others", Description = "Other furniture and accessories", IconClass = "fas fa-box", SortOrder = 6 }
            );

            // Seed SubCategories
            builder.Entity<SubCategory>().HasData(
                // Bedroom
                new SubCategory { Id = 1, CategoryId = 1, Name = "Almirah", Slug = "almirah", SortOrder = 1 },
                new SubCategory { Id = 2, CategoryId = 1, Name = "Bed", Slug = "bed", SortOrder = 2 },
                new SubCategory { Id = 3, CategoryId = 1, Name = "Bedroom Set", Slug = "bedroom-set", SortOrder = 3 },
                new SubCategory { Id = 4, CategoryId = 1, Name = "Bedside Table", Slug = "bedside-table", SortOrder = 4 },
                new SubCategory { Id = 5, CategoryId = 1, Name = "Dressing Table", Slug = "dressing-table", SortOrder = 5 },
                new SubCategory { Id = 6, CategoryId = 1, Name = "King Chair", Slug = "king-chair", SortOrder = 6 },
                // Living Room
                new SubCategory { Id = 7, CategoryId = 2, Name = "2+2+1 Sofa", Slug = "2-2-1-sofa", SortOrder = 1 },
                new SubCategory { Id = 8, CategoryId = 2, Name = "Center Table", Slug = "center-table", SortOrder = 2 },
                new SubCategory { Id = 9, CategoryId = 2, Name = "Corner Sofa", Slug = "corner-sofa", SortOrder = 3 },
                new SubCategory { Id = 10, CategoryId = 2, Name = "Single Sofa", Slug = "single-sofa", SortOrder = 4 },
                new SubCategory { Id = 11, CategoryId = 2, Name = "Sofa Set", Slug = "sofa-set", SortOrder = 5 },
                // Dining Room
                new SubCategory { Id = 12, CategoryId = 3, Name = "All Dining Chair", Slug = "dining-chair", SortOrder = 1 },
                new SubCategory { Id = 13, CategoryId = 3, Name = "Dining Set", Slug = "dining-set", SortOrder = 2 },
                new SubCategory { Id = 14, CategoryId = 3, Name = "Dining Wagon", Slug = "dining-wagon", SortOrder = 3 },
                new SubCategory { Id = 15, CategoryId = 3, Name = "Showcase", Slug = "showcase", SortOrder = 4 },
                new SubCategory { Id = 16, CategoryId = 3, Name = "Dining Table", Slug = "dining-table", SortOrder = 5 },
                // Home Decor
                new SubCategory { Id = 17, CategoryId = 4, Name = "Wall Art", Slug = "wall-art", SortOrder = 1 },
                new SubCategory { Id = 18, CategoryId = 4, Name = "Curtains", Slug = "curtains", SortOrder = 2 },
                new SubCategory { Id = 19, CategoryId = 4, Name = "Cushions", Slug = "cushions", SortOrder = 3 },
                new SubCategory { Id = 20, CategoryId = 4, Name = "Vase", Slug = "vase", SortOrder = 4 },
                new SubCategory { Id = 21, CategoryId = 4, Name = "Mirror", Slug = "mirror", SortOrder = 5 },
                // Interior Design
                new SubCategory { Id = 22, CategoryId = 5, Name = "Full Interior", Slug = "full-interior", SortOrder = 1 },
                new SubCategory { Id = 23, CategoryId = 5, Name = "Modular Kitchen", Slug = "modular-kitchen", SortOrder = 2 },
                new SubCategory { Id = 24, CategoryId = 5, Name = "Office Interior", Slug = "office-interior", SortOrder = 3 },
                // Others
                new SubCategory { Id = 25, CategoryId = 6, Name = "All Others", Slug = "all-others", SortOrder = 1 }
            );

            // Seed SiteSettings
            builder.Entity<SiteSetting>().HasData(
                new SiteSetting { Id = 1, SettingKey = "CompanyName", SettingValue = "BD Furniture", Description = "Company name" },
                new SiteSetting { Id = 2, SettingKey = "Address", SettingValue = "Dhaka, Bangladesh", Description = "Company address" },
                new SiteSetting { Id = 3, SettingKey = "Phone", SettingValue = "+880 1XXX-XXXXXX", Description = "Contact phone" },
                new SiteSetting { Id = 4, SettingKey = "WhatsApp", SettingValue = "8801XXXXXXXXX", Description = "WhatsApp number (without +)" },
                new SiteSetting { Id = 5, SettingKey = "Email", SettingValue = "info@bdfurniture.com", Description = "Contact email" },
                new SiteSetting { Id = 6, SettingKey = "Facebook", SettingValue = "https://facebook.com/bdfurniture", Description = "Facebook page" },
                new SiteSetting { Id = 7, SettingKey = "YouTube", SettingValue = "https://youtube.com/@bdfurniture", Description = "YouTube channel" },
                new SiteSetting { Id = 8, SettingKey = "HeroVideoUrl", SettingValue = "/videos/hero-bg.mp4", Description = "Hero background video" },
                new SiteSetting { Id = 9, SettingKey = "WatchVideoYouTubeId", SettingValue = "dQw4w9WgXcQ", Description = "Hero watch video YouTube ID" },
                new SiteSetting { Id = 10, SettingKey = "YouTubeChannelUrl", SettingValue = "https://youtube.com/@bdfurniture", Description = "YouTube channel URL" }
            );
        }
    }
}
