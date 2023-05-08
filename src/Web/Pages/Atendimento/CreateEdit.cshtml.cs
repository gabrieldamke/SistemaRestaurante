using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.ApiClient;

namespace Web.Pages.Atendimento;

public class CreateEdit : PageModel
{
    [BindProperty] public ApiClient.Atendimento Atendimento { get; set; }

    public SelectList Garcons { get; set; }
    public SelectList Mesas { get; set; }

    public async Task OnGetCreateAsync([FromServices] IClient apiClient)
    {
        Atendimento = new ApiClient.Atendimento();
        var garcons = await apiClient.GetAllGarconsFilteredAsync(null, false);
        var mesas = await apiClient.GetAllMesasFilteredAsync(StatusMesa.Livre, null, false);
        Garcons = new SelectList(garcons, nameof(ApiClient.Garcom.Id), nameof(ApiClient.Garcom.Nome));
        Mesas = new SelectList(mesas, nameof(ApiClient.Mesa.Id), nameof(ApiClient.Mesa.Numero));
    }

    public async Task<IActionResult> OnPostCreateAsync([FromServices] IClient apiClient)
    {
        var atendimento = await apiClient.CreateAtendimentoAsync(Atendimento);
        return RedirectToPagePermanent(nameof(Details), new { id = atendimento.Id });
    }

    public async Task OnGetEditAsync([FromServices] IClient apiClient, int id)
    {
        Atendimento = await apiClient.GetAtendimentoByIdAsync(id);
        var garcons = await apiClient.GetAllGarconsFilteredAsync(null, false);
        var mesas = await apiClient.GetAllMesasFilteredAsync(StatusMesa.Livre, null, false);
        Garcons = new SelectList(garcons, nameof(ApiClient.Garcom.Id), nameof(ApiClient.Garcom.Nome));
        Mesas = new SelectList(mesas, nameof(ApiClient.Mesa.Id), nameof(ApiClient.Mesa.Numero));
    }

    public async Task<IActionResult> OnPostEditAsync([FromServices] IClient apiClient)
    {
        var atendimento = await apiClient.UpdateAtendimentoAsync(Atendimento.Id, Atendimento);
        return RedirectToPagePermanent(nameof(Details), new { id = atendimento.Id });
    }
}