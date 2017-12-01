using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TulostauluCore.Migrations
{
    public partial class scoresAndUndo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Live",
                table: "Live");

            migrationBuilder.RenameTable(
                name: "Live",
                newName: "Tulostaulu");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tulostaulu",
                table: "Tulostaulu",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Score",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AwayRuns = table.Column<int>(type: "INTEGER", nullable: false),
                    GamePeriod = table.Column<int>(type: "INTEGER", nullable: false),
                    HomeRuns = table.Column<int>(type: "INTEGER", nullable: false),
                    PeriodInning = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Score", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Score");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tulostaulu",
                table: "Tulostaulu");

            migrationBuilder.RenameTable(
                name: "Tulostaulu",
                newName: "Live");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Live",
                table: "Live",
                column: "Id");
        }
    }
}
