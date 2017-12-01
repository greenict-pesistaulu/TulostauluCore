using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TulostauluCore.Migrations
{
    public partial class scoresAndUndoBugfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tulostaulu",
                table: "Tulostaulu");

            migrationBuilder.RenameTable(
                name: "Tulostaulu",
                newName: "Live");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Live",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Live",
                table: "Live",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Live",
                table: "Live");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Live");

            migrationBuilder.RenameTable(
                name: "Live",
                newName: "Tulostaulu");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tulostaulu",
                table: "Tulostaulu",
                column: "Id");
        }
    }
}
