using Domain.Entities;
using Domain.Interfaces;
using Domain.Types;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1;

/// <summary>
/// Categorias
/// </summary>
[ApiVersion("1.0")]
public class CategoriaController : ApiBaseController
{
    private readonly IEntityRepository<Categoria> _categoriaEntityRepository;

    public CategoriaController(IEntityRepository<Categoria> categoriaEntityRepository)
    {
        _categoriaEntityRepository = categoriaEntityRepository;
    }

    /// <summary>
    /// Obtém todas as categorias de forma paginada
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="includeAll"></param>
    /// <returns></returns>
    [HttpGet(Name = "GetAllCategoriasPaginated")]
    [ProducesResponseType(typeof(PaginatedList<Categoria>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllPaginated(int pageIndex = 1, int pageSize = 10, bool includeAll = false)
    {
        var categorias =
            await _categoriaEntityRepository.GetPaginatedListAsync(pageIndex, pageSize, includeAll: includeAll);
        return Ok(categorias);
    }

    /// <summary>
    /// Obtém todas as categorias com filtro
    /// </summary>
    /// <param name="name"></param>
    /// <param name="includeAll"></param>
    /// <returns></returns>
    [HttpGet("filter", Name = "GetAllCategoriasFiltered")]
    [ProducesResponseType(typeof(IEnumerable<Categoria>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllFiltered([FromQuery] string? name, bool includeAll = false)
    {
        var categorias =
            await _categoriaEntityRepository.GetAllAsync(x => x.Nome.Contains(name ?? string.Empty),
                includeAll: includeAll);
        return Ok(categorias);
    }

    /// <summary>
    /// Obtém uma categoria pelo seu id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}", Name = "GetCategoriaById")]
    [ProducesResponseType(typeof(Categoria), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        var categoria = await _categoriaEntityRepository.GetByIdAsync(id);
        if (categoria == null)
        {
            return NotFound();
        }

        return Ok(categoria);
    }

    /// <summary>
    /// Cria uma nova categoria
    /// </summary>
    /// <param name="categoria"></param>
    /// <returns></returns>
    [HttpPost(Name = "CreateCategoria")]
    [ProducesResponseType(typeof(Categoria), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] Categoria categoria)
    {
        var categoriaCriada = await _categoriaEntityRepository.AddAsync(categoria);
        return CreatedAtAction(nameof(GetById), new { id = categoriaCriada.Id }, categoriaCriada);
    }

    /// <summary>
    /// Atualiza uma categoria
    /// </summary>
    /// <param name="id"></param>
    /// <param name="categoria"></param>
    /// <returns></returns>
    [HttpPut("{id}", Name = "UpdateCategoria")]
    [ProducesResponseType(typeof(Categoria), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] Categoria categoria)
    {
        var categoriaCriada = await _categoriaEntityRepository.UpdateAsync(categoria);
        return Ok(categoriaCriada);
    }

    /// <summary>
    /// Deleta uma categoria
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}", Name = "DeleteCategoria")]
    [ProducesResponseType(typeof(Categoria), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        var categoriaDeletada = await _categoriaEntityRepository.DeleteAsync(id);
        return Ok(categoriaDeletada);
    }
}