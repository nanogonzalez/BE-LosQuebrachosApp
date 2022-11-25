﻿// <auto-generated />
using System;
using BE_LosQuebrachosApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BE_LosQuebrachosApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BE_LosQuebrachosApp.Entities.Chofer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Cuit")
                        .HasColumnType("bigint");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransporteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransporteId");

                    b.ToTable("Choferes");
                });

            modelBuilder.Entity("BE_LosQuebrachosApp.Entities.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<long>("Cuit")
                        .HasColumnType("bigint");

                    b.Property<string>("DestinoCarga")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RazonSocial")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("BE_LosQuebrachosApp.Entities.OrdenDeCarga", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DestinoCarga")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DestinoDescarga")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DiaHoraCarga")
                        .HasColumnType("datetime2");

                    b.Property<string>("TipoMercaderia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OrdenesDeCargas");
                });

            modelBuilder.Entity("BE_LosQuebrachosApp.Entities.Transporte", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Cuit")
                        .HasColumnType("bigint");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Transportes");
                });

            modelBuilder.Entity("BE_LosQuebrachosApp.Entities.Vehiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Acoplado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CapacidadTN")
                        .HasColumnType("int");

                    b.Property<string>("Chasis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransporteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransporteId");

                    b.ToTable("Vehiculos");
                });

            modelBuilder.Entity("BE_LosQuebrachosApp.Entities.Chofer", b =>
                {
                    b.HasOne("BE_LosQuebrachosApp.Entities.Transporte", "Transporte")
                        .WithMany()
                        .HasForeignKey("TransporteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Transporte");
                });

            modelBuilder.Entity("BE_LosQuebrachosApp.Entities.Vehiculo", b =>
                {
                    b.HasOne("BE_LosQuebrachosApp.Entities.Transporte", "Transporte")
                        .WithMany()
                        .HasForeignKey("TransporteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Transporte");
                });
#pragma warning restore 612, 618
        }
    }
}
