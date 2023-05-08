using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.ApiClient;

namespace Web.Pages.Produto;

public class Index : PageModel
{
    public ProdutoPaginatedList Produtos { get; set; }
    
    public async Task OnGetAsync([FromServices] IClient apiClient, int? pageIndex = 1, int? pageSize = 10)
    {
        Produtos = await apiClient.GetAllProdutosPaginatedAsync(pageIndex, pageSize, true);
    }
}