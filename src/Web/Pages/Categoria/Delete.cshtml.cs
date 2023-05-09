using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.ApiClient;
using ProblemDetails = Web.ApiClient.ProblemDetails;

namespace Web.Pages.Categoria;

public class Delete : PageModel
{
    public ApiClient.Categoria Categoria { get; set; }
    public IList<string> Errors { get; set; } = new List<string>();

    public async Task OnGetAsync([FromServices] IClient apiClient, int id)
    {
        Categoria = await apiClient.GetCategoriaByIdAsync(id);
    }

    public async Task<IActionResult> OnPostAsync([FromServices] IClient apiClient, int id)
    {
        try
        {
            await apiClient.DeleteCategoriaAsync(id);

            return RedirectToPagePermanent(nameof(Index));
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