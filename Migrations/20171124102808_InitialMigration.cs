using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TulostauluCore.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Live",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AwayRuns = table.Column<int>(type: "INTEGER", nullable: false),
                    AwayWins = table.Column<int>(type: "INTEGER", nullable: false),
                    GamePeriod = table.Column<int>(type: "INTEGER", nullable: false),
                    HomeRuns = table.Column<int>(type: "INTEGER", nullable: false),
                    HomeWins = table.Column<int>(type: "INTEGER", nullable: false),
                    InningHitter = table.Column<int>(type: "INTEGER", nullable: false),
                    InningJoker = table.Column<int>(type: "INTEGER", nullable: false),
                    InningLastHitter = table.Column<int>(type: "INTEGER", nullable: false),
                    InningStrikes = table.Column<int>(type: "INTEGER", nullable: false),
                    PeriodInning = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Live", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Live");
        }
    }
}
