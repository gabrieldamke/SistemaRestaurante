using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.ApiClient;

namespace Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IClient _client;

    public IndexModel(ILogger<IndexModel> logger, IClient client)
    {
        _logger = logger;
        _client = client;
    }

    public void OnGet()
    {
        // TODO - Please Mr. Trevisan, delete this junior's code
        var mesas = _client.GetAllMesasPaginatedAsync(1, 10);
        Console.WriteLine("GetAllMesasPaginatedAsync");
        foreach (var mesa in mesas.Result.Items)
        {
            Console.WriteLine($"Mesa: {mesa.Numero} - {mesa.Status}");
        }
    }
}
