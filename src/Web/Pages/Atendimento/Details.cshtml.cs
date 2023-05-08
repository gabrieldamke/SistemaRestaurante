using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.ApiClient;

namespace Web.Pages.Atendimento;

public class Details : PageModel
{
    public ApiClient.Atendimento Atendimento { get; set; }
    
    public async Task OnGetAsync([FromServices] IClient apiClient, int id)
    {
        Atendimento = await apiClient.GetAtendimentoByIdAsync(id);
    }
}