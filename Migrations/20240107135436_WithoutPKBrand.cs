using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scraping.Migrations
{
    /// <inheritdoc />
    public partial class WithoutPKBrand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Smartphone",
                table: "Smartphone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Smartphone",
                table: "Smartphone",
                column: "ID_smartphone");

            migrationBuilder.CreateIndex(
                name: "IX_Smartphone_BrandId",
                table: "Smartphone",
                column: "BrandId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Smartphone",
                table: "Smartphone");

            migrationBuilder.DropIndex(
                name: "IX_Smartphone_BrandId",
                table: "Smartphone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Smartphone",
                table: "Smartphone",
                column: "BrandId");
        }
    }
}
