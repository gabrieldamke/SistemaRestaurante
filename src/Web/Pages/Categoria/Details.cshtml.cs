using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.ApiClient;

namespace Web.Pages.Categoria;

public class Details : PageModel
{
    public ApiClient.Categoria Categoria { get; set; }
    
    public async Task OnGetAsync([FromServices] IClient apiClient, int id)
    {
        Categoria = await apiClient.GetCategoriaByIdAsync(id);
    }
}