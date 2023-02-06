﻿// <auto-generated />
using System;
using BancoVirtual.API.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BancoVirtual.API.Migrations
{
    [DbContext(typeof(sqlserverdbcontext))]
    partial class sqlserverdbcontextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BancoVirtual.API.Models.Conta", b =>
                {
                    b.Property<int>("ContaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContaId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Saldo")
                        .HasColumnType("real");

                    b.Property<string>("Titular")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ContaId");

                    b.ToTable("Contas");
                });

            modelBuilder.Entity("BancoVirtual.API.Models.Extrato", b =>
                {
                    b.Property<int>("ExtratoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExtratoId"));

                    b.Property<DateTime>("DataDaAcao")
                        .HasColumnType("datetime2");

                    b.Property<int>("Transferido")
                        .HasColumnType("int");

                    b.Property<int>("Transferidor")
                        .HasColumnType("int");

                    b.Property<float>("Valor")
                        .HasColumnType("real");

                    b.HasKey("ExtratoId");

                    b.ToTable("Extratos");
                });
#pragma warning restore 612, 618
        }
    }
}