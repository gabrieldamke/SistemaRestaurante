using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.ApiClient;

namespace Web.Pages.Mesa
{
    public class Index : PageModel
    {
        public MesaPaginatedList Mesas { get; set; }
    
        public async Task OnGetAsync([FromServices] IClient apiClient, int? pageIndex = 1, int? pageSize = 10)
        {
            Mesas = await apiClient.GetAllMesasPaginatedAsync(pageIndex, pageSize, true);
        }
    }
}