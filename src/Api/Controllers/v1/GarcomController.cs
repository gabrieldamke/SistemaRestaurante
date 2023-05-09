using Domain.Entities;
using Domain.Interfaces;
using Domain.Types;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1;

/// <summary>
/// Garçons
/// </summary>
[ApiVersion("1.0")]
public class GarcomController : ApiBaseController
{
    private readonly IEntityRepository<Garcom> _garcomEntityRepository;

    public GarcomController(IEntityRepository<Garcom> garcomEntityRepository)
    {
        _garcomEntityRepository = garcomEntityRepository;
    }

    /// <summary>
    /// Obtém todos os garçons de forma paginada
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="includeAll"></param>
    /// <returns></returns>
    [HttpGet(Name = "GetAllGarconsPaginated")]
    [ProducesResponseType(typeof(PaginatedList<Garcom>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllPaginated(int pageIndex = 1, int pageSize = 10, bool includeAll = false)
    {
        var garcons = await _garcomEntityRepository.GetPaginatedListAsync(pageIndex, pageSize, includeAll: includeAll);
        return Ok(garcons);
    }

    /// <summary>
    /// Obtém todos os garçons com filtro
    /// </summary>
    /// <param name="name"></param>
    /// <param name="includeAll"></param>
    /// <returns></returns>
    [HttpGet("filter", Name = "GetAllGarconsFiltered")]
    [ProducesResponseType(typeof(IEnumerable<Garcom>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllFiltered([FromQuery] string? name, bool includeAll = false)
    {
        var garcons =
            await _garcomEntityRepository.GetAllAsync(x => x.Nome.Contains(name ?? string.Empty),
                includeAll: includeAll);
        return Ok(garcons);
    }

    /// <summary>
    /// Obtém um garçons pelo seu id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="includeAtendimentos"></param>
    /// <returns></returns>
    [HttpGet("{id}", Name = "GetGarcomById")]
    [ProducesResponseType(typeof(Garcom), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id, bool includeAtendimentos = false)
    {
        var garcom = await _garcomEntityRepository.GetByIdAsync(id, includeAtendimentos);
        if (garcom == null)
        {
            return NotFound();
        }

        return Ok(garcom);
    }

    /// <summary>
    /// Cria um novo garçons
    /// </summary>
    /// <param name="garcom"></param>
    /// <returns></returns>
    [HttpPost(Name = "CreateGarcom")]
    [ProducesResponseType(typeof(Garcom), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] Garcom garcom)
    {
        var garcomCriado = await _garcomEntityRepository.AddAsync(garcom);
        return CreatedAtAction(nameof(GetById), new { id = garcomCriado.Id }, garcomCriado);
    }

    /// <summary>
    /// Atualiza um garçons
    /// </summary>
    /// <param name="id"></param>
    /// <param name="garcom"></param>
    /// <returns></returns>
    [HttpPut("{id}", Name = "UpdateGarcom")]
    [ProducesResponseType(typeof(Garcom), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] Garcom garcom)
    {
        var garcomAtualizado = await _garcomEntityRepository.UpdateAsync(garcom);
        return Ok(garcomAtualizado);
    }

    /// <summary>
    /// Deleta um garçom
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}", Name = "DeleteGarcom")]
    [ProducesResponseType(typeof(Garcom), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        var garcomDeletado = await _garcomEntityRepository.DeleteAsync(id);
        return Ok(garcomDeletado);
    }
}