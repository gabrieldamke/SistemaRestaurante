﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.ApiClient;
using ProblemDetails = Web.ApiClient.ProblemDetails;

namespace Web.Pages.Garcom;

public class CreateEdit : PageModel
{
    [BindProperty] public ApiClient.Garcom Garcom { get; set; }

    public IList<string> Errors { get; set; } = new List<string>();

    public async Task OnGetCreateAsync([FromServices] IClient apiClient)
    {
        Garcom = new ApiClient.Garcom();
    }

    public async Task<IActionResult> OnPostCreateAsync([FromServices] IClient apiClient)
    {
        try
        {
            var garcom = await apiClient.CreateGarcomAsync(Garcom);
            return RedirectToPagePermanent(nameof(Details), new { id = garcom.Id });
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
        Garcom = await apiClient.GetGarcomByIdAsync(id, false);
    }

    public async Task<IActionResult> OnPostEditAsync([FromServices] IClient apiClient)
    {
        try
        {
            var garcom = await apiClient.UpdateGarcomAsync(Garcom.Id, Garcom);
            return RedirectToPagePermanent(nameof(Details), new { id = garcom.Id });
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