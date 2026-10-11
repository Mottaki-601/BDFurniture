using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BDFurniture_Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSubCategoryCoverImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverImagePath",
                table: "SubCategories",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3249));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3264));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3266));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3267));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3267));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3268));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3415));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3418));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3419));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3419));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 5,
                column: "UpdatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3420));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 6,
                column: "UpdatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3421));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 7,
                column: "UpdatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3421));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 8,
                column: "UpdatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3422));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 9,
                column: "UpdatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3423));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 10,
                column: "UpdatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3423));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CoverImagePath", "CreatedAt" },
                values: new object[] { null, new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3339) });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CoverImagePath", "CreatedAt" },
                values: new object[] { null, new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3342) });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CoverImagePath", "CreatedAt" },
                values: new object[] { null, new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3344) });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CoverImagePath", "CreatedAt" },
                values: new object[] { null, new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3344) });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CoverImagePath", "CreatedAt" },
                values: new object[] { null, new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3345) });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CoverImagePath", "CreatedAt" },
                values: new object[] { null, new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3346) });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CoverImagePath", "CreatedAt" },
                values: new object[] { null, new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3347) });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CoverImagePath", "CreatedAt" },
                values: new object[] { null, new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3348) });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CoverImagePath", "CreatedAt" },
                values: new object[] { null, new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3349) });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CoverImagePath", "CreatedAt" },
                values: new object[] { null, new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3350) });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CoverImagePath", "CreatedAt" },
                values: new object[] { null, new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3350) });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CoverImagePath", "CreatedAt" },
                values: new object[] { null, new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3351) });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CoverImagePath", "CreatedAt" },
                values: new object[] { null, new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3352) });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CoverImagePath", "CreatedAt" },
                values: new object[] { null, new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3381) });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CoverImagePath", "CreatedAt" },
                values: new object[] { null, new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3382) });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CoverImagePath", "CreatedAt" },
                values: new object[] { null, new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3383) });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CoverImagePath", "CreatedAt" },
                values: new object[] { null, new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3383) });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CoverImagePath", "CreatedAt" },
                values: new object[] { null, new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3384) });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CoverImagePath", "CreatedAt" },
                values: new object[] { null, new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3385) });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CoverImagePath", "CreatedAt" },
                values: new object[] { null, new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3386) });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CoverImagePath", "CreatedAt" },
                values: new object[] { null, new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3387) });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CoverImagePath", "CreatedAt" },
                values: new object[] { null, new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3387) });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CoverImagePath", "CreatedAt" },
                values: new object[] { null, new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3388) });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CoverImagePath", "CreatedAt" },
                values: new object[] { null, new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3389) });

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CoverImagePath", "CreatedAt" },
                values: new object[] { null, new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3390) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImagePath",
                table: "SubCategories");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7851));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7862));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7864));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7865));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7866));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7867));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7982));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7984));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(8009));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(8010));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 5,
                column: "UpdatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(8011));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 6,
                column: "UpdatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(8011));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 7,
                column: "UpdatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(8012));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 8,
                column: "UpdatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(8013));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 9,
                column: "UpdatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(8014));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 10,
                column: "UpdatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(8014));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7936));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7939));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7940));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7941));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7942));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7943));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7944));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7945));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7946));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7946));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7951));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7951));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7952));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7953));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7954));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7954));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7955));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7956));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7957));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7957));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7958));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7959));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7960));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7960));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedAt",
                value: new DateTime(2026, 9, 24, 17, 44, 9, 607, DateTimeKind.Local).AddTicks(7961));
        }
    }
}
