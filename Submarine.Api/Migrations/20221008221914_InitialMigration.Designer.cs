﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Submarine.Api.Models.Database;

#nullable disable

namespace Submarine.Api.Migrations
{
    [DbContext(typeof(PostgresDatabaseContext))]
    [Migration("20221008221914_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Submarine.Core.Provider.Provider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ApiKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Mode")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<short>("Priority")
                        .HasColumnType("smallint");

                    b.Property<int>("Protocol")
                        .HasColumnType("integer");

                    b.Property<List<string>>("Tags")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Providers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Provider");
                });

            modelBuilder.Entity("Submarine.Core.Provider.BittorrentTracker", b =>
                {
                    b.HasBaseType("Submarine.Core.Provider.Provider");

                    b.Property<int?>("MinimumSeeders")
                        .HasColumnType("integer");

                    b.Property<long?>("SeasonPackSeedTime")
                        .HasColumnType("bigint");

                    b.Property<float?>("SeedRatio")
                        .HasColumnType("real");

                    b.Property<long?>("SeedTime")
                        .HasColumnType("bigint");

                    b.HasDiscriminator().HasValue("BittorrentTracker");
                });

            modelBuilder.Entity("Submarine.Core.Provider.UsenetIndexer", b =>
                {
                    b.HasBaseType("Submarine.Core.Provider.Provider");

                    b.HasDiscriminator().HasValue("UsenetIndexer");
                });
#pragma warning restore 612, 618
        }
    }
}
