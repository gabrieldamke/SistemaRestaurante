using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.ApiClient;

namespace Web.Pages.Produto;

public class CreateEdit : PageModel
{
    [BindProperty]
    public ApiClient.Produto Produto { get; set; }
    public SelectList Categorias { get; set; }
    
    public async Task OnGetCreateAsync([FromServices] IClient apiClient)
    {
        Produto = new ApiClient.Produto();
        var categorias = await apiClient.GetAllCategoriasFilteredAsync(null, false);
        Categorias = new SelectList(categorias, nameof(Categoria.Id), nameof(Categoria.Nome));
    }
    
    public async Task<IActionResult> OnPostCreateAsync([FromServices] IClient apiClient)
    {
        var produto = await apiClient.CreateProdutoAsync(Produto);
        return RedirectToPagePermanent(nameof(Details), new { id = produto.Id });
    }

    public async Task OnGetEditAsync([FromServices] IClient apiClient, int id)
    {
        Produto = await apiClient.GetProdutoByIdAsync(id);
        var categorias = await apiClient.GetAllCategoriasFilteredAsync(null, false);
        Categorias = new SelectList(categorias, nameof(Categoria.Id), nameof(Categoria.Nome));
    }
    
    public async Task<IActionResult> OnPostEditAsync([FromServices] IClient apiClient)
    {
        var produto = await apiClient.UpdateProdutoAsync(Produto.Id, Produto);
        return RedirectToPagePermanent(nameof(Details), new { id = produto.Id });
    }
}