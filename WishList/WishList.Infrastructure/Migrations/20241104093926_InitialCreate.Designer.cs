﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WishList.Infrastructure.Data;

#nullable disable

namespace WishList.Infrastructure.Migrations
{
    [DbContext(typeof(WishListContext))]
    [Migration("20241104093926_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WishList.Infrastructure.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(max)");

                    b.Property<string>("ProfilePicture")
                        .IsRequired()
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WishList.Infrastructure.Models.Wish", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(max)");

                    b.Property<bool>("IsSelected")
                        .HasColumnType("bit");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Wishes");
                });

            modelBuilder.Entity("WishList.Infrastructure.Models.WishFulfillment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("WishGranterId")
                        .HasColumnType("int");

                    b.Property<int>("WishId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WishGranterId");

                    b.HasIndex("WishId");

                    b.ToTable("WishFulfillments");
                });

            modelBuilder.Entity("WishList.Infrastructure.Models.Wish", b =>
                {
                    b.HasOne("WishList.Infrastructure.Models.User", null)
                        .WithMany("Wishes")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("WishList.Infrastructure.Models.WishFulfillment", b =>
                {
                    b.HasOne("WishList.Infrastructure.Models.User", "WishGranter")
                        .WithMany()
                        .HasForeignKey("WishGranterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WishList.Infrastructure.Models.Wish", "Wish")
                        .WithMany()
                        .HasForeignKey("WishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Wish");

                    b.Navigation("WishGranter");
                });

            modelBuilder.Entity("WishList.Infrastructure.Models.User", b =>
                {
                    b.Navigation("Wishes");
                });
#pragma warning restore 612, 618
        }
    }
}