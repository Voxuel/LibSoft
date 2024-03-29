﻿// <auto-generated />
using System;
using LibSoft_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibSoft_API.Migrations.SqlServerMigrations
{
    [DbContext(typeof(LibSoftDbContext))]
    [Migration("20230901125856_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LibSoft_Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "John R Sanders",
                            Description = "John Sanders tour of the seven seas and what he faced along the way",
                            Genre = "Autobiography",
                            Title = "Over the ocean",
                            Year = 2011
                        },
                        new
                        {
                            Id = 2,
                            Author = "Dr Emilia Wing",
                            Description = "The moments in medicine and its challenging nature",
                            Genre = "Autobiography",
                            Title = "Patience",
                            Year = 2023
                        },
                        new
                        {
                            Id = 3,
                            Author = "Martin Andersson",
                            Description = "Set in 1800 england, a story about a beggars rise to become an assassin fora dark brotherhood created to dismantle the rich",
                            Genre = "Fiction/Thriller",
                            Title = "The Highborn's demise",
                            Year = 2015
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
