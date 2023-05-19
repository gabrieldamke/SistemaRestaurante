using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.ApiClient;

namespace Web.Pages.Atendimento;

public class Details : PageModel
{
    public ApiClient.Atendimento Atendimento { get; set; }
    public IEnumerable<AtendimentoProduto> AtendimentoProdutos { get; set; }

    public async Task OnGetAsync([FromServices] IClient apiClient, int id)
    {
        AtendimentoProdutos = await apiClient.GetAllAtendimentosProdutosAsync(id);
        Atendimento = await apiClient.GetAtendimentoByIdAsync(id);
    }
}