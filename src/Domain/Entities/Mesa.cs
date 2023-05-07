using Domain.Entities.Shared;
using Domain.Enums;

namespace Domain.Entities;

public sealed class Mesa : Entity
{
    public Mesa()
    {
        Atendimentos = new HashSet<Atendimento>();
    }
    public int Numero { get; set; }
    public StatusMesa Status { get; set; }
    public DateTime? HoraAbertura { get; set; }
    public ICollection<Atendimento> Atendimentos { get; set; }
}