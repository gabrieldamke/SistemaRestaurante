using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.ApiClient;
using ProblemDetails = Web.ApiClient.ProblemDetails;

namespace Web.Pages.Produto;

public class CreateEdit : PageModel
{
    [BindProperty] public ApiClient.Produto Produto { get; set; }
    public SelectList Categorias { get; set; }
    public IList<string> Errors { get; set; } = new List<string>();

    public async Task OnGetCreateAsync([FromServices] IClient apiClient)
    {
        Produto = new ApiClient.Produto();
        var categorias = await apiClient.GetAllCategoriasFilteredAsync(null, false);
        Categorias = new SelectList(categorias, nameof(ApiClient.Categoria.Id), nameof(ApiClient.Categoria.Nome));
    }

    public async Task<IActionResult> OnPostCreateAsync([FromServices] IClient apiClient)
    {
        try
        {
            var produto = await apiClient.CreateProdutoAsync(Produto);
            return RedirectToPagePermanent(nameof(Details), new { id = produto.Id });
        }
        catch (ApiException<ProblemDetails> e)
        {
            Errors.Add(e.Result.Detail!);
            return Page();
        }
    }

    public async Task OnGetEditAsync([FromServices] IClient apiClient, int id)
    {
        Produto = await apiClient.GetProdutoByIdAsync(id);
        var categorias = await apiClient.GetAllCategoriasFilteredAsync(null, false);
        Categorias = new SelectList(categorias, nameof(ApiClient.Categoria.Id), nameof(ApiClient.Categoria.Nome));
    }

    public async Task<IActionResult> OnPostEditAsync([FromServices] IClient apiClient)
    {
        try
        {
            var produto = await apiClient.UpdateProdutoAsync(Produto.Id, Produto);
            return RedirectToPagePermanent(nameof(Details), new { id = produto.Id });
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