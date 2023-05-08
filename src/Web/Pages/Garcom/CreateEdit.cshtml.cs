using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.ApiClient;

namespace Web.Pages.Garcom;

public class CreateEdit : PageModel
{
    [BindProperty]
    public ApiClient.Garcom Garcom { get; set; }

    public async Task OnGetCreateAsync([FromServices] IClient apiClient)
    {
        Garcom = new ApiClient.Garcom();
    }
    
    public async Task<IActionResult> OnPostCreateAsync([FromServices] IClient apiClient)
    {
        var garcom = await apiClient.CreateGarcomAsync(Garcom);
        return RedirectToPagePermanent(nameof(Details), new { id = garcom.Id });
    }

    public async Task OnGetEditAsync([FromServices] IClient apiClient, int id)
    {
        Garcom = await apiClient.GetGarcomByIdAsync(id, false);
    }
    
    public async Task<IActionResult> OnPostEditAsync([FromServices] IClient apiClient)
    {
        var garcom = await apiClient.UpdateGarcomAsync(Garcom.Id, Garcom);
        return RedirectToPagePermanent(nameof(Details), new { id = garcom.Id });
    }
}