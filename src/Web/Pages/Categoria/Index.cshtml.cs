using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.ApiClient;
using Web.Constants;
using Web.Model;

namespace Web.Pages.Categoria;

public class Index : PageModel
{
    public IEnumerable<ApiClient.Categoria>? Categorias { get; set; }
    public PageViewModel PageViewModel { get; set; }

    public async Task OnGetAsync([FromServices] IClient apiClient, int? pageIndex = 1, int? pageSize = 10)
    {
        var response = await apiClient.GetAllCategoriasPaginatedAsync(pageIndex, Pagination.PageSize, true);
        Categorias = response.Items;
        PageViewModel = new PageViewModel(response.PageIndex, response.TotalPages, response.HasPreviousPage,
            response.HasNextPage);
    }
}