using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.ApiClient;

namespace Web.Pages.Atendimento;

public class RemoveProduct : PageModel
{
    public async Task<IActionResult> OnGetAsync([FromServices] IClient apiClient, int id, int produtoId)
    {
        await apiClient.RemoveProdutoAsync(id, produtoId);
        
        return RedirectToPagePermanent(nameof(Details), new { id });
    }
}