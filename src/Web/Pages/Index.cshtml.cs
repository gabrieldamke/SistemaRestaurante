using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages;

public class IndexModel : PageModel
{
    public void OnGet([FromServices] IConfiguration configuration)
    {
        var uri = configuration.GetSection("Api:Uri").Value;
        uri = "http://localhost:5000";
        ViewData["Uri"] = uri;
    }
}
