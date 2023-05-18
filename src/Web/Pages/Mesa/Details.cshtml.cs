using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.ApiClient;

namespace Web.Pages.Mesa
{
    public class Details : PageModel
    {
        [BindProperty]
        public ApiClient.Mesa Mesa { get; set; }
    
        public async Task OnGetAsync([FromServices] IClient apiClient, int id)
        {
            Mesa = await apiClient.GetMesaByIdAsync(id, true);
        }
        
        public async Task<RedirectToPageResult> OnPostAsync([FromServices] IClient apiClient, int id)
        {
            if (Mesa.HoraAbertura is null)
            {
                await apiClient.OpenMesaAsync(id);
            }
            else
            {
                await apiClient.ReserveMesaAsync(id, Mesa.HoraAbertura.Value);
            }
            
            Mesa = await apiClient.GetMesaByIdAsync(id, true);
            return RedirectToPage("Details", new { id });
        }
    }
}