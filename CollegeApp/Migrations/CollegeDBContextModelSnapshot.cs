﻿// <auto-generated />
using System;
using CollegeApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CollegeApp.Migrations
{
    [DbContext(typeof(CollegeDBContext))]
    partial class CollegeDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CollegeApp.Data.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Students", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Bangalore",
                            DOB = new DateTime(1982, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "Test@1",
                            StudentName = "Madhu"
                        },
                        new
                        {
                            Id = 2,
                            Address = "Bangalore",
                            DOB = new DateTime(2000, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "Test@2",
                            StudentName = "Dharani"
                        },
                        new
                        {
                            Id = 3,
                            Address = "Bangalore",
                            DOB = new DateTime(1982, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "Test@3",
                            StudentName = "MadhuDharani"
                        },
                        new
                        {
                            Id = 4,
                            Address = "Bangalore",
                            DOB = new DateTime(2001, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "Test@4",
                            StudentName = "DharaniMadhu"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
