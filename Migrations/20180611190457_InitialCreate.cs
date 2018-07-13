using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TulostauluCore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AwayHitter = table.Column<int>(nullable: false),
                    AwayLastHitter = table.Column<int>(nullable: false),
                    AwayRuns = table.Column<int>(nullable: false),
                    AwayWins = table.Column<int>(nullable: false),
                    GamePeriod = table.Column<int>(nullable: false),
                    HomeHitter = table.Column<int>(nullable: false),
                    HomeLastHitter = table.Column<int>(nullable: false),
                    HomeRuns = table.Column<int>(nullable: false),
                    HomeWins = table.Column<int>(nullable: false),
                    InningInsideTeam = table.Column<string>(nullable: true),
                    InningJoker = table.Column<int>(nullable: false),
                    InningStrikes = table.Column<int>(nullable: false),
                    InningTurn = table.Column<char>(nullable: false),
                    PeriodInning = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Live",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AwayHitter = table.Column<int>(nullable: false),
                    AwayLastHitter = table.Column<int>(nullable: false),
                    AwayRuns = table.Column<int>(nullable: false),
                    AwayWins = table.Column<int>(nullable: false),
                    GamePeriod = table.Column<int>(nullable: false),
                    HomeHitter = table.Column<int>(nullable: false),
                    HomeLastHitter = table.Column<int>(nullable: false),
                    HomeRuns = table.Column<int>(nullable: false),
                    HomeWins = table.Column<int>(nullable: false),
                    InningInsideTeam = table.Column<string>(nullable: true),
                    InningJoker = table.Column<int>(nullable: false),
                    InningStrikes = table.Column<int>(nullable: false),
                    InningTurn = table.Column<char>(nullable: false),
                    PeriodInning = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Live", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Score",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AwayRuns = table.Column<int>(nullable: false),
                    GamePeriod = table.Column<int>(nullable: false),
                    HomeRuns = table.Column<int>(nullable: false),
                    PeriodInning = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Score", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.DropTable(
                name: "Live");

            migrationBuilder.DropTable(
                name: "Score");
        }
    }
}
