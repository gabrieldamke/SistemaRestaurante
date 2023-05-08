using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.ApiClient;

namespace Web.Pages.Mesa
{
    public class Details : PageModel
    {
        public ApiClient.Mesa Mesa { get; set; }
    
        public async Task OnGetAsync([FromServices] IClient apiClient, int id)
        {
            Mesa = await apiClient.GetMesaByIdAsync(id, true);
        }
    }
}