﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using TulostauluCore.Models;

namespace TulostauluCore.Migrations
{
    [DbContext(typeof(TulostauluContext))]
    partial class TulostauluContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("TulostauluCore.Models.Score", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AwayRuns");

                    b.Property<int>("GamePeriod");

                    b.Property<int>("HomeRuns");

                    b.Property<int>("PeriodInning");

                    b.HasKey("Id");

                    b.ToTable("Score");
                });

            modelBuilder.Entity("TulostauluCore.Models.Tulostaulu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AwayHitter");

                    b.Property<int>("AwayLastHitter");

                    b.Property<int>("AwayRuns");

                    b.Property<int>("AwayWins");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("GamePeriod");

                    b.Property<int>("HomeHitter");

                    b.Property<int>("HomeLastHitter");

                    b.Property<int>("HomeRuns");

                    b.Property<int>("HomeWins");

                    b.Property<string>("InningInsideTeam");

                    b.Property<int>("InningJoker");

                    b.Property<int>("InningStrikes");

                    b.Property<char>("InningTurn");

                    b.Property<int>("PeriodInning");

                    b.HasKey("Id");

                    b.ToTable("Live");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Tulostaulu");
                });

            modelBuilder.Entity("TulostauluCore.Models.History", b =>
                {
                    b.HasBaseType("TulostauluCore.Models.Tulostaulu");


                    b.ToTable("History");

                    b.HasDiscriminator().HasValue("History");
                });
#pragma warning restore 612, 618
        }
    }
}
