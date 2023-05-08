using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.ApiClient;

namespace Web.Pages.Atendimento;

public class Index : PageModel
{
    public AtendimentoPaginatedList Atendimentos { get; set; }
    
    public async Task OnGetAsync([FromServices] IClient apiClient, int? pageIndex = 1, int? pageSize = 10)
    {
        Atendimentos = await apiClient.GetAllAtendimentosPaginatedAsync(pageIndex, pageSize, true);
    }
}