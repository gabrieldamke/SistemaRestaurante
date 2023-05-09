using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.ApiClient;
using ProblemDetails = Web.ApiClient.ProblemDetails;

namespace Web.Pages.Categoria;

public class CreateEdit : PageModel
{
    [BindProperty] public ApiClient.Categoria Categoria { get; set; }
    public IList<string> Errors { get; set; } = new List<string>();

    public async Task OnGetCreateAsync([FromServices] IClient apiClient)
    {
        Categoria = new ApiClient.Categoria();
    }

    public async Task<IActionResult> OnPostCreateAsync([FromServices] IClient apiClient)
    {
        try
        {
            var categoria = await apiClient.CreateCategoriaAsync(Categoria);
            return RedirectToPagePermanent(nameof(Details), new { id = categoria.Id });
        }
        catch (ApiException<ProblemDetails> e)
        {
            Errors.Add(e.Result.Detail!);
            return Page();
        }
    }

    public async Task OnGetEditAsync([FromServices] IClient apiClient, int id)
    {
        Categoria = await apiClient.GetCategoriaByIdAsync(id);
    }

    public async Task<IActionResult> OnPostEditAsync([FromServices] IClient apiClient)
    {
        try
        {
            var categoria = await apiClient.UpdateCategoriaAsync(Categoria.Id, Categoria);
            return RedirectToPagePermanent(nameof(Details), new { id = categoria.Id });
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