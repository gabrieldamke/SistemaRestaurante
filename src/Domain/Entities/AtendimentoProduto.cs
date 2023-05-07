using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class AtendimentoProduto
{
    public int AtendimentoId { get; set; }
    public Atendimento Atendimento { get; set; }
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; }
    public decimal Preco { get; set; }
    public int Quantidade { get; set;}
    [NotMapped]
    public decimal ValorTotal => Preco * Quantidade;
}