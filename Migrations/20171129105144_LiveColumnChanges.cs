﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TulostauluCore.Migrations
{
    public partial class LiveColumnChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InningHitter",
                table: "Live");

            migrationBuilder.DropColumn(
                name: "InningLastHitter",
                table: "Live");

            migrationBuilder.AddColumn<int>(
                name: "AwayHitter",
                table: "Live",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AwayLastHitter",
                table: "Live",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HomeHitter",
                table: "Live",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HomeLastHitter",
                table: "Live",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "InningInsideTeam",
                table: "Live",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<char>(
                name: "InningTurn",
                table: "Live",
                type: "INTEGER",
                nullable: false,
                defaultValue: ' ');
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AwayHitter",
                table: "Live");

            migrationBuilder.DropColumn(
                name: "AwayLastHitter",
                table: "Live");

            migrationBuilder.DropColumn(
                name: "HomeHitter",
                table: "Live");

            migrationBuilder.DropColumn(
                name: "HomeLastHitter",
                table: "Live");

            migrationBuilder.DropColumn(
                name: "InningInsideTeam",
                table: "Live");

            migrationBuilder.DropColumn(
                name: "InningTurn",
                table: "Live");

            migrationBuilder.AddColumn<int>(
                name: "InningHitter",
                table: "Live",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InningLastHitter",
                table: "Live",
                nullable: false,
                defaultValue: 0);
        }
    }
}
