﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.ApiClient;
using ProblemDetails = Web.ApiClient.ProblemDetails;

namespace Web.Pages.Atendimento;

public class Delete : PageModel
{
    public ApiClient.Atendimento Atendimento { get; set; }
    public IList<string> Errors { get; set; } = new List<string>();

    public async Task OnGetAsync([FromServices] IClient apiClient, int id)
    {
        Atendimento = await apiClient.GetAtendimentoByIdAsync(id);
    }

    public async Task<IActionResult> OnPostAsync([FromServices] IClient apiClient, int id)
    {
        try
        {
            await apiClient.DeleteAtendimentoAsync(id);

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