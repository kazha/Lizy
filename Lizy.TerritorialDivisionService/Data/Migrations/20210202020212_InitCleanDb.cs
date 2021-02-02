using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lizy.TerritorialDivisionService.Data.Migrations
{
    public partial class InitCleanDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Counties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Coordinates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Penetration = table.Column<double>(type: "float", nullable: true),
                    PenetrationPercentileFrom = table.Column<int>(type: "int", nullable: true),
                    PenetrationPercentileTill = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parishes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Coordinates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Penetration = table.Column<double>(type: "float", nullable: true),
                    PenetrationPercentileFrom = table.Column<int>(type: "int", nullable: true),
                    PenetrationPercentileTill = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parishes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parishes_Counties_CountyId",
                        column: x => x.CountyId,
                        principalTable: "Counties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SquareKilometers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParishId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Inhabitants = table.Column<double>(type: "float", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    MaleDensity = table.Column<int>(type: "int", nullable: false),
                    KidDensity = table.Column<int>(type: "int", nullable: false),
                    WorkAgeDensity = table.Column<int>(type: "int", nullable: false),
                    ElderlyDensity = table.Column<int>(type: "int", nullable: false),
                    Clients = table.Column<int>(type: "int", nullable: false),
                    Potential = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Coordinates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Penetration = table.Column<double>(type: "float", nullable: true),
                    PenetrationPercentileFrom = table.Column<int>(type: "int", nullable: true),
                    PenetrationPercentileTill = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SquareKilometers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SquareKilometers_Parishes_ParishId",
                        column: x => x.ParishId,
                        principalTable: "Parishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parishes_CountyId",
                table: "Parishes",
                column: "CountyId");

            migrationBuilder.CreateIndex(
                name: "IX_SquareKilometers_ParishId",
                table: "SquareKilometers",
                column: "ParishId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SquareKilometers");

            migrationBuilder.DropTable(
                name: "Parishes");

            migrationBuilder.DropTable(
                name: "Counties");
        }
    }
}
