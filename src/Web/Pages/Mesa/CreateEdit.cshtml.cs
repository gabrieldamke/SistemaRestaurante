using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.ApiClient;
using Web.Helpers;
using ProblemDetails = Web.ApiClient.ProblemDetails;

namespace Web.Pages.Mesa
{
    public class CreateEdit : PageModel
    {
        [BindProperty] public ApiClient.Mesa Mesa { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }
        public IList<string> Errors { get; set; } = new List<string>();

        public async Task OnGetCreateAsync([FromServices] IClient apiClient)
        {
            Mesa = new ApiClient.Mesa();
            Status = EnumHelper.GetSelectList(typeof(MesaStatus));
        }

        public async Task<IActionResult> OnPostCreateAsync([FromServices] IClient apiClient)
        {
            try
            {
                var mesa = await apiClient.CreateMesaAsync(Mesa);
                return RedirectToPagePermanent(nameof(Details), new { id = mesa.Id });
            }
            catch (ApiException<ProblemDetails> e)
            {
                Errors.Add(e.Result.Detail ?? e.Result.Title ?? e.Message);
                return Page();
            }
            catch (Exception e)
            {
                Errors.Add(e.Message);
                return Page();
            }
        }

        public async Task OnGetEditAsync([FromServices] IClient apiClient, int id)
        {
            Mesa = await apiClient.GetMesaByIdAsync(id, false);
            Status = EnumHelper.GetSelectList(typeof(MesaStatus));
        }

        public async Task<IActionResult> OnPostEditAsync([FromServices] IClient apiClient)
        {
            try
            {
                var mesa = await apiClient.UpdateMesaAsync(Mesa.Id, Mesa);
                return RedirectToPagePermanent(nameof(Details), new { id = mesa.Id });
            }
            catch (ApiException<ProblemDetails> e)
            {
                Errors.Add(e.Result.Detail ?? e.Result.Title ?? e.Message);
                return Page();
            }
            catch (Exception e)
            {
                Errors.Add(e.Message);
                return Page();
            }
        }
    }
}