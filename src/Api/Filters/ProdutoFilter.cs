namespace Api.Filters;

public class ProdutoFilter
{
    public string? Nome { get; set; }
    public decimal? PrecoMinimo { get; set; }
    public decimal? PrecoMaximo { get; set; }
    public string? Descricao { get; set; }
    public string? Categoria { get; set; }
}