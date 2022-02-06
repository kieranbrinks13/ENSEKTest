﻿// <auto-generated />
using System;
using DataLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataLibrary.Migrations
{
    [DbContext(typeof(EnsekDbContext))]
    partial class EnsekDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EnsekGlobal.Models.AccountDM", b =>
                {
                    b.Property<long>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("AccountId"), 1L, 1);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccountId");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            AccountId = 2344L,
                            FirstName = "Tommy",
                            LastName = "Test"
                        },
                        new
                        {
                            AccountId = 2233L,
                            FirstName = "Barry",
                            LastName = "Test"
                        },
                        new
                        {
                            AccountId = 8766L,
                            FirstName = "Sally",
                            LastName = "Test"
                        },
                        new
                        {
                            AccountId = 2345L,
                            FirstName = "Jerry",
                            LastName = "Test"
                        },
                        new
                        {
                            AccountId = 2346L,
                            FirstName = "Ollie",
                            LastName = "Test"
                        },
                        new
                        {
                            AccountId = 2347L,
                            FirstName = "Tara",
                            LastName = "Test"
                        },
                        new
                        {
                            AccountId = 2348L,
                            FirstName = "Tammy",
                            LastName = "Test"
                        },
                        new
                        {
                            AccountId = 2349L,
                            FirstName = "Simon",
                            LastName = "Test"
                        },
                        new
                        {
                            AccountId = 2350L,
                            FirstName = "Colin",
                            LastName = "Test"
                        },
                        new
                        {
                            AccountId = 2351L,
                            FirstName = "Gladys",
                            LastName = "Test"
                        },
                        new
                        {
                            AccountId = 2352L,
                            FirstName = "Greg",
                            LastName = "Test"
                        },
                        new
                        {
                            AccountId = 2353L,
                            FirstName = "Tony",
                            LastName = "Test"
                        },
                        new
                        {
                            AccountId = 2355L,
                            FirstName = "Arthur",
                            LastName = "Test"
                        },
                        new
                        {
                            AccountId = 2356L,
                            FirstName = "Craig",
                            LastName = "Test"
                        },
                        new
                        {
                            AccountId = 6776L,
                            FirstName = "Laura",
                            LastName = "Test"
                        },
                        new
                        {
                            AccountId = 4534L,
                            FirstName = "JOSH",
                            LastName = "TEST"
                        },
                        new
                        {
                            AccountId = 1234L,
                            FirstName = "Freya",
                            LastName = "Test"
                        },
                        new
                        {
                            AccountId = 1239L,
                            FirstName = "Noddy",
                            LastName = "Test"
                        },
                        new
                        {
                            AccountId = 1240L,
                            FirstName = "Archie",
                            LastName = "Test"
                        },
                        new
                        {
                            AccountId = 1241L,
                            FirstName = "Lara",
                            LastName = "Test"
                        },
                        new
                        {
                            AccountId = 1242L,
                            FirstName = "Tim",
                            LastName = "Test"
                        },
                        new
                        {
                            AccountId = 1243L,
                            FirstName = "Graham",
                            LastName = "Test"
                        },
                        new
                        {
                            AccountId = 1244L,
                            FirstName = "Tony",
                            LastName = "Test"
                        },
                        new
                        {
                            AccountId = 1245L,
                            FirstName = "Neville",
                            LastName = "Test"
                        },
                        new
                        {
                            AccountId = 1246L,
                            FirstName = "Jo",
                            LastName = "Test"
                        },
                        new
                        {
                            AccountId = 1247L,
                            FirstName = "Jim",
                            LastName = "Test"
                        },
                        new
                        {
                            AccountId = 1248L,
                            FirstName = "Pam",
                            LastName = "Test"
                        });
                });

            modelBuilder.Entity("EnsekGlobal.Models.MeterReadingsDM", b =>
                {
                    b.Property<long>("MeterReadingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("MeterReadingId"), 1L, 1);

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<long?>("MeterReadValue")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("MeterReadingDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("MeterReadingId");

                    b.HasIndex("AccountId");

                    b.ToTable("MeterReadings");
                });

            modelBuilder.Entity("EnsekGlobal.Models.MeterReadingsDM", b =>
                {
                    b.HasOne("EnsekGlobal.Models.AccountDM", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });
#pragma warning restore 612, 618
        }
    }
}
