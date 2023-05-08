using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.ApiClient;
using Web.Helpers;

namespace Web.Pages.Mesa
{
    public class CreateEdit : PageModel
    {
        [BindProperty]
        public ApiClient.Mesa Mesa { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }
    
        public async Task OnGetCreateAsync([FromServices] IClient apiClient)
        {
            Mesa = new ApiClient.Mesa();
            Status = EnumHelper.GetSelectList(typeof(MesaStatus));
        }
    
        public async Task<IActionResult> OnPostCreateAsync([FromServices] IClient apiClient)
        {
            var mesa = await apiClient.CreateMesaAsync(Mesa);
            return RedirectToPagePermanent(nameof(Details), new { id = mesa.Id });
        }

        public async Task OnGetEditAsync([FromServices] IClient apiClient, int id)
        {
            Mesa = await apiClient.GetMesaByIdAsync(id, false);
            Status = EnumHelper.GetSelectList(typeof(MesaStatus));
        }
    
        public async Task<IActionResult> OnPostEditAsync([FromServices] IClient apiClient)
        {
            var mesa = await apiClient.UpdateMesaAsync(Mesa.Id, Mesa);
            return RedirectToPagePermanent(nameof(Details), new { id = mesa.Id });
        }
    }
}