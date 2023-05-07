using Domain.Entities.Shared;

namespace Domain.Entities;

public sealed class Atendimento : Entity
{
    public Atendimento()
    {
        Produtos = new HashSet<Produto>();
    }
    
    public Mesa? Mesa { get; set; }
    public int MesaId { get; set; }
    public Garcom? Garcom { get; set; }
    public int GarcomId { get; set; }
    public DateTime HoraAbertura { get; set; }
    public DateTime HoraFechamento { get; set; }
    public ICollection<Produto> Produtos { get; set; }
}