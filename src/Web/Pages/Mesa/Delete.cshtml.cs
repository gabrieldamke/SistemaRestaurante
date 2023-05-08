using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.ApiClient;

namespace Web.Pages.Mesa
{
    public class Delete : PageModel
    {
        public ApiClient.Mesa Mesa { get; set; }
    
        public async Task OnGetAsync([FromServices] IClient apiClient, int id)
        {
            Mesa = await apiClient.GetMesaByIdAsync(id, false);
        }
    
        public async Task<IActionResult> OnPostAsync([FromServices] IClient apiClient, int id)
        {
            await apiClient.DeleteMesaAsync(id);
        
            return RedirectToPagePermanent(nameof(Index));
        }
    }
}