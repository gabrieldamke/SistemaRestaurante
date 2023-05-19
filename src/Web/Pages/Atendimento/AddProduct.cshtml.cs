using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.ApiClient;

namespace Web.Pages.Atendimento;

public class AddProduct : PageModel
{
    [BindProperty] public AtendimentoProduto AtendimentoProduto { get; set; }
    public ApiClient.Atendimento Atendimento { get; set; }
    public SelectList Produtos { get; set; }

    public async Task OnGetAsync([FromServices] IClient apiClient, int id)
    {
        Atendimento = await apiClient.GetAtendimentoByIdAsync(id);
        var produtos = await apiClient.GetAllProdutosFilteredAsync(null, false);
        Produtos = new SelectList(produtos, nameof(ApiClient.Produto.Id), nameof(ApiClient.Produto.Nome));
    }

    public async Task<IActionResult> OnPostAsync([FromServices] IClient apiClient, int id)
    {
        var atendimentoProduto =
            await apiClient.AddProdutoAsync(id, AtendimentoProduto.ProdutoId, AtendimentoProduto.Quantidade);
        
        return RedirectToPagePermanent(nameof(Details), new { id = atendimentoProduto.AtendimentoId });
    }
}