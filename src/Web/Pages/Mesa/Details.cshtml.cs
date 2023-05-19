using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.ApiClient;

namespace Web.Pages.Mesa
{
    public class Details : PageModel
    {
        [BindProperty] public ApiClient.Mesa Mesa { get; set; }

        public async Task OnGetAsync([FromServices] IClient apiClient, int id)
        {
            Mesa = await apiClient.GetMesaByIdAsync(id, true);
        }

        public async Task<RedirectToPageResult> OnPostAsync([FromServices] IClient apiClient, int id)
        {
            switch (Mesa.Status)
            {
                case MesaStatus.Livre:
                    if (Mesa.HoraAbertura is null)
                        await apiClient.OpenMesaAsync(id);
                    else
                        await apiClient.ReserveMesaAsync(id, Mesa.HoraAbertura.Value);
                    break;
                case MesaStatus.Ocupada:
                    await apiClient.CloseMesaAsync(id);
                    break;
            }

            Mesa = await apiClient.GetMesaByIdAsync(id, true);
            return RedirectToPage("Details", new { id });
        }
    }
}