using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.ApiClient;

namespace Web.Pages.Produto;

public class Details : PageModel
{
    public ApiClient.Produto Produto { get; set; }
    
    public async Task OnGetAsync([FromServices] IClient apiClient, int id)
    {
        Produto = await apiClient.GetProdutoByIdAsync(id);
    }
}