using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TulostauluCore.Migrations
{
    public partial class Undochange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Live");

            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AwayHitter = table.Column<int>(type: "INTEGER", nullable: false),
                    AwayLastHitter = table.Column<int>(type: "INTEGER", nullable: false),
                    AwayRuns = table.Column<int>(type: "INTEGER", nullable: false),
                    AwayWins = table.Column<int>(type: "INTEGER", nullable: false),
                    GamePeriod = table.Column<int>(type: "INTEGER", nullable: false),
                    HomeHitter = table.Column<int>(type: "INTEGER", nullable: false),
                    HomeLastHitter = table.Column<int>(type: "INTEGER", nullable: false),
                    HomeRuns = table.Column<int>(type: "INTEGER", nullable: false),
                    HomeWins = table.Column<int>(type: "INTEGER", nullable: false),
                    InningInsideTeam = table.Column<string>(type: "TEXT", nullable: true),
                    InningJoker = table.Column<int>(type: "INTEGER", nullable: false),
                    InningStrikes = table.Column<int>(type: "INTEGER", nullable: false),
                    InningTurn = table.Column<char>(type: "INTEGER", nullable: false),
                    PeriodInning = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Live",
                nullable: false,
                defaultValue: "");
        }
    }
}
