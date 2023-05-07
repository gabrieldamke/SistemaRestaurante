using Data.Helpers;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class RestauranteDbContext : DbContext
{
    public DbSet<Mesa> Mesas { get; set; }
    public DbSet<Garcom> Garcons { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Atendimento> Atendimentos { get; set; }
    public DbSet<AtendimentoProduto> AtendimentoProdutos { get; set; }

    public RestauranteDbContext(DbContextOptions<RestauranteDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Produto>()
            .HasOne(p => p.Categoria)
            .WithMany()
            .HasForeignKey(p => p.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .Entity<Atendimento>()
            .HasOne(a => a.Mesa)
            .WithMany()
            .HasForeignKey(a => a.MesaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .Entity<Atendimento>()
            .HasOne(a => a.Garcom)
            .WithMany()
            .HasForeignKey(a => a.GarcomId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .Entity<Atendimento>()
            .HasMany(a => a.Produtos)
            .WithMany()
            .UsingEntity<AtendimentoProduto>(
                j => j
                    .HasOne(ap => ap.Produto)
                    .WithMany()
                    .HasForeignKey(ap => ap.ProdutoId),
                j => j
                    .HasOne(ap => ap.Atendimento)
                    .WithMany()
                    .HasForeignKey(ap => ap.AtendimentoId),
                j => { j.HasKey(ap => new { ap.AtendimentoId, ap.ProdutoId }); }
            );
        
        modelBuilder.SeedData();
    }
}