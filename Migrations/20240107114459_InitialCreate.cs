using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scraping.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    ID_brand = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.ID_brand);
                });

            migrationBuilder.CreateTable(
                name: "Smartphone",
                columns: table => new
                {
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_smartphone = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Screen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Processor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MainCamera = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ram = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InternalMemory = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BatteryCapacity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OperatingSystem = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Smartphone", x => x.BrandId);
                    table.ForeignKey(
                        name: "FK_Smartphone_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "ID_brand",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Smartphone");

            migrationBuilder.DropTable(
                name: "Brand");
        }
    }
}
