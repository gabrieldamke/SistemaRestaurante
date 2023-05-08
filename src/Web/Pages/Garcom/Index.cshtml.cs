using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.ApiClient;

namespace Web.Pages.Garcom;

public class Index : PageModel
{
    public GarcomPaginatedList Garcons { get; set; }
    
    public async Task OnGetAsync([FromServices] IClient apiClient, int? pageIndex = 1, int? pageSize = 10)
    {
        Garcons = await apiClient.GetAllGarconsPaginatedAsync(pageIndex, pageSize, true);
    }
}