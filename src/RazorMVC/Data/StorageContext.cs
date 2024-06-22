using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorMVC.Models;

namespace RazorMVC.Data
{
    public class StorageContext : DbContext
    {
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public StorageContext(DbContextOptions<StorageContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Fornecedor>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nome)
                      .IsRequired();

                entity.ToTable(t => t.HasCheckConstraint("Telefone Inválido", "length(Telefone) = 9"));
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Nome)
                      .IsUnique();

                entity.Property(e => e.Nome)
                      .IsRequired();

                entity.Property(e => e.Preço)
                       .IsRequired()
                       .HasColumnType("INTEGER")
                       .HasConversion(
                            v => (int)(v * 100),
                            v => v / 100.0m);

                entity.ToTable(t => t.HasCheckConstraint("Preço precisa ser maior do que 0", "Preço > 0"));

                entity.Property(e => e.DataDeCriação)
                      .HasDefaultValueSql("datetime('now', 'localtime')");

                entity.HasOne(d => d.Fornecedor)
                      .WithMany(p => p.Produtos)
                      .HasForeignKey(d => d.FornecedorId)
                      .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
