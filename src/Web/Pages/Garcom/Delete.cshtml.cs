using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.ApiClient;

namespace Web.Pages.Garcom;

public class Delete : PageModel
{
    public ApiClient.Garcom Garcom { get; set; }
    
    public async Task OnGetAsync([FromServices] IClient apiClient, int id)
    {
        Garcom = await apiClient.GetGarcomByIdAsync(id, true);
    }
    
    public async Task<IActionResult> OnPostAsync([FromServices] IClient apiClient, int id)
    {
        await apiClient.DeleteGarcomAsync(id);
        
        return RedirectToPagePermanent(nameof(Index));
    }
}