using Domain.Entities.Shared;

namespace Domain.Entities;

public class Categoria : Entity
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
}