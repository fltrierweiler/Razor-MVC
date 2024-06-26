﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RazorMVC.Data;

#nullable disable

namespace RazorMVC.Migrations
{
    [DbContext(typeof(StorageContext))]
    [Migration("20240625123001_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("RazorMVC.Models.Fornecedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("Telefone")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Nome")
                        .IsUnique();

                    b.ToTable("Fornecedores", t =>
                        {
                            t.HasCheckConstraint("Telefone Inválido", "length(Telefone) = 9");
                        });
                });

            modelBuilder.Entity("RazorMVC.Models.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DataDeCriação")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("datetime('now', 'localtime')");

                    b.Property<string>("Descrição")
                        .HasColumnType("TEXT");

                    b.Property<int?>("FornecedorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("Preço")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FornecedorId");

                    b.HasIndex("Nome")
                        .IsUnique();

                    b.ToTable("Produtos", t =>
                        {
                            t.HasCheckConstraint("Preço precisa ser maior do que 0", "Preço > 0");
                        });
                });

            modelBuilder.Entity("RazorMVC.Models.Produto", b =>
                {
                    b.HasOne("RazorMVC.Models.Fornecedor", "Fornecedor")
                        .WithMany("Produtos")
                        .HasForeignKey("FornecedorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Fornecedor");
                });

            modelBuilder.Entity("RazorMVC.Models.Fornecedor", b =>
                {
                    b.Navigation("Produtos");
                });
#pragma warning restore 612, 618
        }
    }
}
