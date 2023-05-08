using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.ApiClient;

namespace Web.Pages.Atendimento;

public class Delete : PageModel
{
    public ApiClient.Atendimento Atendimento { get; set; }
    
    public async Task OnGetAsync([FromServices] IClient apiClient, int id)
    {
        Atendimento = await apiClient.GetAtendimentoByIdAsync(id);
    }
    
    public async Task<IActionResult> OnPostAsync([FromServices] IClient apiClient, int id)
    {
        await apiClient.DeleteAtendimentoAsync(id);
        
        return RedirectToPagePermanent(nameof(Index));
    }
}