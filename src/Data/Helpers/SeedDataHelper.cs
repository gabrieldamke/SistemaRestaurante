using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Data.Helpers;

public static class SeedDataHelper
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>().HasData(
            new Categoria { Id = 1, Nome = "Entradas", Descricao = "Opções de entrada para abrir o apetite" },
            new Categoria
            {
                Id = 2, Nome = "Pratos Principais", Descricao = "Pratos principais com carnes, peixes e vegetarianos"
            },
            new Categoria { Id = 3, Nome = "Acompanhamentos", Descricao = "Acompanhamentos para os pratos principais" },
            new Categoria { Id = 4, Nome = "Sobremesas", Descricao = "Sobremesas deliciosas para fechar a refeição" },
            new Categoria
                { Id = 5, Nome = "Bebidas", Descricao = "Diversas opções de bebidas para acompanhar a refeição" }
        );

        modelBuilder.Entity<Produto>().HasData(
            new Produto
            {
                Id = 1, Nome = "Carpaccio de Carne",
                Descricao = "Carpaccio de carne com rúcula, parmesão e molho de mostarda", Preco = 25.90m,
                CategoriaId = 1
            },
            new Produto
            {
                Id = 2, Nome = "Risoto de Funghi",
                Descricao = "Risoto com cogumelos frescos, parmesão e azeite trufado", Preco = 39.90m, CategoriaId = 2
            },
            new Produto
            {
                Id = 3, Nome = "Arroz de Pato", Descricao = "Arroz com pato, linguiça, ervilhas e azeitonas",
                Preco = 44.90m, CategoriaId = 2
            },
            new Produto
            {
                Id = 4, Nome = "Purê de Batatas", Descricao = "Purê de batatas com alho e cebolinha", Preco = 9.90m,
                CategoriaId = 3
            },
            new Produto
            {
                Id = 5, Nome = "Mousse de Chocolate",
                Descricao = "Mousse de chocolate com chantilly e raspas de chocolate", Preco = 15.90m, CategoriaId = 4
            },
            new Produto
            {
                Id = 6, Nome = "Vinho Tinto Chileno", Descricao = "Vinho tinto chileno de uva cabernet sauvignon",
                Preco = 69.90m, CategoriaId = 5
            }
        );

        modelBuilder.Entity<Mesa>().HasData(
            new Mesa { Id = 1, Numero = 1, Status = StatusMesa.Livre },
            new Mesa { Id = 2, Numero = 2, Status = StatusMesa.Livre },
            new Mesa { Id = 3, Numero = 3, Status = StatusMesa.Livre },
            new Mesa { Id = 4, Numero = 4, Status = StatusMesa.Livre },
            new Mesa { Id = 5, Numero = 5, Status = StatusMesa.Livre }
        );

        modelBuilder.Entity<Garcom>().HasData(
            new Garcom { Id = 1, Nome = "André", Sobrenome = "Soares", NumeroTelefone = "45999999991"},
            new Garcom { Id = 2, Nome = "Fernanda", Sobrenome = "Silva", NumeroTelefone = "45999999992" },
            new Garcom { Id = 3, Nome = "Henrique", Sobrenome = "Souza", NumeroTelefone = "45999999993" },
            new Garcom { Id = 4, Nome = "Juliano", Sobrenome = "Santos", NumeroTelefone = "45999999994" },
            new Garcom { Id = 5, Nome = "Sabrina", Sobrenome = "Moreira", NumeroTelefone = "45999999995" } 
        );

        modelBuilder.Entity<Atendimento>().HasData(
            new Atendimento
            {
                Id = 1, MesaId = 1, GarcomId = 1, HoraAbertura = DateTime.Now, HoraFechamento = DateTime.Now.AddHours(1)
            },
            new Atendimento
            {
                Id = 2, MesaId = 2, GarcomId = 2, HoraAbertura = DateTime.Now, HoraFechamento = DateTime.Now.AddHours(1)
            },
            new Atendimento
            {
                Id = 3, MesaId = 3, GarcomId = 3, HoraAbertura = DateTime.Now, HoraFechamento = DateTime.Now.AddHours(1)
            },
            new Atendimento
            {
                Id = 4, MesaId = 4, GarcomId = 4, HoraAbertura = DateTime.Now, HoraFechamento = DateTime.Now.AddHours(1)
            },
            new Atendimento
            {
                Id = 5, MesaId = 5, GarcomId = 5, HoraAbertura = DateTime.Now, HoraFechamento = DateTime.Now.AddHours(1)
            }
        );

        modelBuilder.Entity<AtendimentoProduto>().HasData(
            new AtendimentoProduto { Id = 1, AtendimentoId = 1, ProdutoId = 1, Quantidade = 1, Preco = 24.9m },
            new AtendimentoProduto { Id = 2, AtendimentoId = 1, ProdutoId = 2, Quantidade = 2, Preco = 39.9m },
            new AtendimentoProduto { Id = 3, AtendimentoId = 1, ProdutoId = 3, Quantidade = 1, Preco = 40 },
            new AtendimentoProduto { Id = 4, AtendimentoId = 1, ProdutoId = 4, Quantidade = 3, Preco = 9.9m },
            new AtendimentoProduto { Id = 5, AtendimentoId = 1, ProdutoId = 5, Quantidade = 5, Preco = 14.9m },
            new AtendimentoProduto { Id = 6, AtendimentoId = 2, ProdutoId = 1, Quantidade = 3, Preco = 25.9m },
            new AtendimentoProduto { Id = 7, AtendimentoId = 2, ProdutoId = 2, Quantidade = 4, Preco = 37.5m },
            new AtendimentoProduto { Id = 8, AtendimentoId = 2, ProdutoId = 3, Quantidade = 3, Preco = 39.9m },
            new AtendimentoProduto { Id = 9, AtendimentoId = 2, ProdutoId = 4, Quantidade = 2, Preco = 17.9m },
            new AtendimentoProduto { Id = 10, AtendimentoId = 2, ProdutoId = 5, Quantidade = 5, Preco = 15.9m },
            new AtendimentoProduto { Id = 11, AtendimentoId = 2, ProdutoId = 6, Quantidade = 1, Preco = 69.9m }
        );
    }
}