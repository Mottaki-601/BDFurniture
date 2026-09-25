using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BDFurniture_Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFurnitureTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IconClass = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Message = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsletterSubscribers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SubscribedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsletterSubscribers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SettingKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SettingValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YouTubeVideos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    YouTubeId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YouTubeVideos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    DiscountPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    WhatsAppNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    AverageRating = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    TotalRatings = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    AltText = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductInquiries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    InquirerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsResolved = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInquiries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductInquiries_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductRatings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    ReviewerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ReviewText = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductReviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "IconClass", "ImagePath", "IsActive", "Name", "Slug", "SortOrder" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7851), "Complete bedroom furniture solutions", "fas fa-bed", null, true, "Bedroom", "bedroom", 1 },
                    { 2, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7862), "Elegant living room furniture", "fas fa-couch", null, true, "Living Room", "living-room", 2 },
                    { 3, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7864), "Stylish dining room furniture", "fas fa-utensils", null, true, "Dining Room", "dining-room", 3 },
                    { 4, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7865), "Beautiful home decoration items", "fas fa-paint-brush", null, true, "Home Decor", "home-decor", 4 },
                    { 5, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7866), "Professional interior design services", "fas fa-drafting-compass", null, true, "Interior Design", "interior-design", 5 },
                    { 6, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7867), "Other furniture and accessories", "fas fa-box", null, true, "Others", "others", 6 }
                });

            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "Id", "Description", "SettingKey", "SettingValue", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Company name", "CompanyName", "BD Furniture", new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7982) },
                    { 2, "Company address", "Address", "Dhaka, Bangladesh", new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7984) },
                    { 3, "Contact phone", "Phone", "+880 1XXX-XXXXXX", new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(8009) },
                    { 4, "WhatsApp number (without +)", "WhatsApp", "8801XXXXXXXXX", new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(8010) },
                    { 5, "Contact email", "Email", "info@bdfurniture.com", new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(8011) },
                    { 6, "Facebook page", "Facebook", "https://facebook.com/bdfurniture", new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(8011) },
                    { 7, "YouTube channel", "YouTube", "https://youtube.com/@bdfurniture", new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(8012) },
                    { 8, "Hero background video", "HeroVideoUrl", "/videos/hero-bg.mp4", new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(8013) },
                    { 9, "Hero watch video YouTube ID", "WatchVideoYouTubeId", "dQw4w9WgXcQ", new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(8014) },
                    { 10, "YouTube channel URL", "YouTubeChannelUrl", "https://youtube.com/@bdfurniture", new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(8014) }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "ImagePath", "IsActive", "Name", "Slug", "SortOrder" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7936), null, null, true, "Almirah", "almirah", 1 },
                    { 2, 1, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7939), null, null, true, "Bed", "bed", 2 },
                    { 3, 1, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7940), null, null, true, "Bedroom Set", "bedroom-set", 3 },
                    { 4, 1, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7941), null, null, true, "Bedside Table", "bedside-table", 4 },
                    { 5, 1, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7942), null, null, true, "Dressing Table", "dressing-table", 5 },
                    { 6, 1, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7943), null, null, true, "King Chair", "king-chair", 6 },
                    { 7, 2, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7944), null, null, true, "2+2+1 Sofa", "2-2-1-sofa", 1 },
                    { 8, 2, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7945), null, null, true, "Center Table", "center-table", 2 },
                    { 9, 2, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7946), null, null, true, "Corner Sofa", "corner-sofa", 3 },
                    { 10, 2, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7946), null, null, true, "Single Sofa", "single-sofa", 4 },
                    { 11, 2, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7951), null, null, true, "Sofa Set", "sofa-set", 5 },
                    { 12, 3, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7951), null, null, true, "All Dining Chair", "dining-chair", 1 },
                    { 13, 3, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7952), null, null, true, "Dining Set", "dining-set", 2 },
                    { 14, 3, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7953), null, null, true, "Dining Wagon", "dining-wagon", 3 },
                    { 15, 3, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7954), null, null, true, "Showcase", "showcase", 4 },
                    { 16, 3, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7954), null, null, true, "Dining Table", "dining-table", 5 },
                    { 17, 4, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7955), null, null, true, "Wall Art", "wall-art", 1 },
                    { 18, 4, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7956), null, null, true, "Curtains", "curtains", 2 },
                    { 19, 4, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7957), null, null, true, "Cushions", "cushions", 3 },
                    { 20, 4, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7957), null, null, true, "Vase", "vase", 4 },
                    { 21, 4, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7958), null, null, true, "Mirror", "mirror", 5 },
                    { 22, 5, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7959), null, null, true, "Full Interior", "full-interior", 1 },
                    { 23, 5, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7960), null, null, true, "Modular Kitchen", "modular-kitchen", 2 },
                    { 24, 5, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7960), null, null, true, "Office Interior", "office-interior", 3 },
                    { 25, 6, new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7961), null, null, true, "All Others", "all-others", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Slug",
                table: "Categories",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NewsletterSubscribers_Email",
                table: "NewsletterSubscribers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInquiries_ProductId",
                table: "ProductInquiries",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRatings_ProductId_UserId",
                table: "ProductRatings",
                columns: new[] { "ProductId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_ProductId",
                table: "ProductReviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Slug",
                table: "Products",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubCategoryId",
                table: "Products",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteSettings_SettingKey",
                table: "SiteSettings",
                column: "SettingKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_Slug",
                table: "SubCategories",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactMessages");

            migrationBuilder.DropTable(
                name: "NewsletterSubscribers");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProductInquiries");

            migrationBuilder.DropTable(
                name: "ProductRatings");

            migrationBuilder.DropTable(
                name: "ProductReviews");

            migrationBuilder.DropTable(
                name: "SiteSettings");

            migrationBuilder.DropTable(
                name: "YouTubeVideos");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
