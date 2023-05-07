using Domain.Entities.Shared;

namespace Domain.Entities;

public class Produto : Entity
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public Categoria? Categoria { get; set; }
    public int CategoriaId { get; set; }
}