﻿// <auto-generated />
using System;
using EstacionamentoBackoffice.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EstacionamentoBackoffice.Data.Migrations
{
    [DbContext(typeof(MeuDbContext))]
    [Migration("20231118172815_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EstacionamentoBackoffice.Business.Models.Carro", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasColumnType("varchar(12)");

                    b.HasKey("Id");

                    b.HasIndex("Placa")
                        .IsUnique();

                    b.ToTable("Carros", (string)null);
                });

            modelBuilder.Entity("EstacionamentoBackoffice.Business.Models.FormaPagamento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("varchar(3)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Codigo")
                        .IsUnique();

                    b.ToTable("FormasPagamento", (string)null);
                });

            modelBuilder.Entity("EstacionamentoBackoffice.Business.Models.Garagem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<decimal>("PrecoHorasExtra")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PrecoMensalista")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PrecoUmaHora")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("Codigo")
                        .IsUnique();

                    b.ToTable("Garagens", (string)null);
                });

            modelBuilder.Entity("EstacionamentoBackoffice.Business.Models.Passagem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CarroId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataHoraEntrada")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataHoraSaida")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FormaPagamentoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GaragemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("PrecoTotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CarroId");

                    b.HasIndex("FormaPagamentoId");

                    b.HasIndex("GaragemId");

                    b.ToTable("Passagens", (string)null);
                });

            modelBuilder.Entity("EstacionamentoBackoffice.Business.Models.Passagem", b =>
                {
                    b.HasOne("EstacionamentoBackoffice.Business.Models.Carro", "Carro")
                        .WithMany("Passagens")
                        .HasForeignKey("CarroId")
                        .IsRequired();

                    b.HasOne("EstacionamentoBackoffice.Business.Models.FormaPagamento", "FormaPagamento")
                        .WithMany("Passagens")
                        .HasForeignKey("FormaPagamentoId")
                        .IsRequired();

                    b.HasOne("EstacionamentoBackoffice.Business.Models.Garagem", "Garagem")
                        .WithMany("Passagens")
                        .HasForeignKey("GaragemId")
                        .IsRequired();

                    b.Navigation("Carro");

                    b.Navigation("FormaPagamento");

                    b.Navigation("Garagem");
                });

            modelBuilder.Entity("EstacionamentoBackoffice.Business.Models.Carro", b =>
                {
                    b.Navigation("Passagens");
                });

            modelBuilder.Entity("EstacionamentoBackoffice.Business.Models.FormaPagamento", b =>
                {
                    b.Navigation("Passagens");
                });

            modelBuilder.Entity("EstacionamentoBackoffice.Business.Models.Garagem", b =>
                {
                    b.Navigation("Passagens");
                });
#pragma warning restore 612, 618
        }
    }
}