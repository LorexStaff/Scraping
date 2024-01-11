using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scraping.Migrations
{
    /// <inheritdoc />
    public partial class Headphones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Smartphone");

            migrationBuilder.CreateTable(
                name: "Headphone",
                columns: table => new
                {
                    ID_headphone = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Warranty = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BluetoothVersion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Color = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DeviceType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Impedance = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Headphone", x => x.ID_headphone);
                    table.ForeignKey(
                        name: "FK_Headphone_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "ID_brand",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Headphone_BrandId",
                table: "Headphone",
                column: "BrandId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Headphone");

            migrationBuilder.AddColumn<string>(
                name: "Price",
                table: "Smartphone",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
