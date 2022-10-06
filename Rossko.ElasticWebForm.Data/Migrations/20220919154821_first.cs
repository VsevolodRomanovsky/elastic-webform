using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rossko.ElasticWebForm.Data.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "oem_catalog",
                columns: table => new
                {
                    HitId = table.Column<string>(type: "nvarchar(450)", nullable: false, collation: "SQL_Latin1_General_CP1_CS_AS"),
                    Catalog = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateIndex = table.Column<DateTime>(type: "Date", nullable: false),
                    Event = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsInternalUser = table.Column<bool>(type: "bit", nullable: true),
                    IsMobile = table.Column<bool>(type: "bit", nullable: true),
                    MemberId = table.Column<int>(type: "int", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VinNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("HitId", x => x.HitId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_oem_catalog_DateIndex",
                table: "oem_catalog",
                column: "DateIndex");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "oem_catalog");
        }
    }
}
