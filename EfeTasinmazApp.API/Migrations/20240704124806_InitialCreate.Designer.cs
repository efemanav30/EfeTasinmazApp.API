﻿// <auto-generated />
using EfeTasinmazApp.API.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EfeTasinmazApp.API.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20240704124806_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("EfeTasinmazApp.API.Entities.Il", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Iller");
                });

            modelBuilder.Entity("EfeTasinmazApp.API.Entities.Ilce", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("IlId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IlId");

                    b.ToTable("Ilceler");
                });

            modelBuilder.Entity("EfeTasinmazApp.API.Entities.Mahalle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("IlceId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IlceId");

                    b.ToTable("Mahalleler");
                });

            modelBuilder.Entity("EfeTasinmazApp.API.Entities.Tasinmaz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Ada")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("KoordinatBilgileri")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MahalleId")
                        .HasColumnType("integer");

                    b.Property<string>("Nitelik")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Parsel")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("MahalleId");

                    b.ToTable("Tasinmazlar");
                });

            modelBuilder.Entity("EfeTasinmazApp.API.Entities.Ilce", b =>
                {
                    b.HasOne("EfeTasinmazApp.API.Entities.Il", "Il")
                        .WithMany("Ilceler")
                        .HasForeignKey("IlId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EfeTasinmazApp.API.Entities.Mahalle", b =>
                {
                    b.HasOne("EfeTasinmazApp.API.Entities.Ilce", "Ilce")
                        .WithMany("Mahalleler")
                        .HasForeignKey("IlceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EfeTasinmazApp.API.Entities.Tasinmaz", b =>
                {
                    b.HasOne("EfeTasinmazApp.API.Entities.Mahalle", "Mahalle")
                        .WithMany("Tasinmazlar")
                        .HasForeignKey("MahalleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
