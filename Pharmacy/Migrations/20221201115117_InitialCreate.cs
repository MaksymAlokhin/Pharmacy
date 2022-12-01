using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medicine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    BoxArt = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pharmacy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manager",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PharmacyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manager", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manager_Pharmacy_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacy",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PharmacyMedicine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PharmacyId = table.Column<int>(type: "int", nullable: false),
                    MedicineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PharmacyMedicine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PharmacyMedicine_Medicine_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PharmacyMedicine_Pharmacy_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Manager_PharmacyId",
                table: "Manager",
                column: "PharmacyId",
                unique: true,
                filter: "[PharmacyId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PharmacyMedicine_MedicineId",
                table: "PharmacyMedicine",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_PharmacyMedicine_PharmacyId",
                table: "PharmacyMedicine",
                column: "PharmacyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Manager");

            migrationBuilder.DropTable(
                name: "PharmacyMedicine");

            migrationBuilder.DropTable(
                name: "Medicine");

            migrationBuilder.DropTable(
                name: "Pharmacy");
        }
    }
}
