﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WordBox.Api;

#nullable disable

namespace WordBox.Api.Migrations
{
    [DbContext(typeof(WordBoxDbContext))]
    partial class WordBoxDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WordBox.Api.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("WordBox.Api.Word", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("LanguageId")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.ToTable("Words");
                });

            modelBuilder.Entity("WordBox.Api.WordMeaning", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("WordId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("WordId");

                    b.ToTable("WordMeanings");
                });

            modelBuilder.Entity("WordBox.Api.Word", b =>
                {
                    b.HasOne("WordBox.Api.Language", null)
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WordBox.Api.WordMeaning", b =>
                {
                    b.HasOne("WordBox.Api.Word", "Word")
                        .WithMany("Meanings")
                        .HasForeignKey("WordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Word");
                });

            modelBuilder.Entity("WordBox.Api.Word", b =>
                {
                    b.Navigation("Meanings");
                });
#pragma warning restore 612, 618
        }
    }
}
