using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.ApiClient;

namespace Web.Pages.Garcom;

public class Details : PageModel
{
    public ApiClient.Garcom Garcom { get; set; }
    
    public async Task OnGetAsync([FromServices] IClient apiClient, int id)
    {
        Garcom = await apiClient.GetGarcomByIdAsync(id, true);
    }
}