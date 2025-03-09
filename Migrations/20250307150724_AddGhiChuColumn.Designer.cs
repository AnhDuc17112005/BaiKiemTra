﻿// <auto-generated />
using Cau123.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Causo1.Migrations
{
    [DbContext(typeof(GoodsDbContext))]
    [Migration("20250307150724_AddGhiChuColumn")]
    partial class AddGhiChuColumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Causo1.Models.HangHoa", b =>
                {
                    b.Property<string>("MaHangHoa")
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<string>("TenHangHoa")
                        .IsRequired()
                        .HasColumnType("nvarchar(MAX)");

                    b.HasKey("MaHangHoa");

                    b.ToTable("Goods");
                });
#pragma warning restore 612, 618
        }
    }
}
