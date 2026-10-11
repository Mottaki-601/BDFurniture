using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BDFurniture_Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProductImageIsBackground : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBackground",
                table: "ProductImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7840));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7853));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7854));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7855));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7856));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7857));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7987));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7990));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7990));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7991));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 5,
                column: "UpdatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7992));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 6,
                column: "UpdatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7993));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 7,
                column: "UpdatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7993));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 8,
                column: "UpdatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7994));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 9,
                column: "UpdatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7995));

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 10,
                column: "UpdatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7995));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7923));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7926));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7927));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7928));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7929));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7930));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7931));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7932));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7933));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7933));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7934));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7936));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7936));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7937));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7938));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7939));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7940));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7940));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7941));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7942));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7943));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7943));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7944));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7945));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 16, 45, 22, 47, DateTimeKind.Local).AddTicks(7946));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBackground",
                table: "ProductImages");

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
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3339));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3342));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3344));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3344));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3345));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3346));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3347));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3348));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3349));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3350));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3350));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3351));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3352));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3381));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3382));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3383));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3383));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3384));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3385));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3386));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3387));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3387));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3388));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3389));

            migrationBuilder.UpdateData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedAt",
                value: new DateTime(2026, 10, 11, 13, 32, 27, 621, DateTimeKind.Local).AddTicks(3390));
        }
    }
}
