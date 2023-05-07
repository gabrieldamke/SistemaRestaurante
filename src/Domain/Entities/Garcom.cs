using Domain.Entities.Shared;

namespace Domain.Entities;

public sealed class Garcom : Entity
{
    public Garcom()
    {
        Atendimentos = new HashSet<Atendimento>();
    }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string NumeroTelefone { get; set; }

    public ICollection<Atendimento> Atendimentos { get; set; }
}
