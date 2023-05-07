using Domain.Entities;
using Domain.Interfaces;
using Domain.Types;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1;

[ApiVersion("1.0")]
public class CategoriaController : ApiBaseController
{
    private readonly IRepository<Categoria> _categoriaRepository;

    public CategoriaController(IRepository<Categoria> categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    [HttpGet(Name = "Obtém todas as categorias de forma paginada")]
    [ProducesResponseType(typeof(PaginatedList<Categoria>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllPaginated(int pageIndex = 1, int pageSize = 10)
    {
        var categorias = await _categoriaRepository.GetPaginatedListAsync(pageIndex, pageSize);
        return Ok(categorias);
    }

    [HttpGet("filter", Name = "Obtém todas as categorias com filtro")]
    [ProducesResponseType(typeof(IEnumerable<Categoria>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllFiltered([FromQuery] string? name)
    {
        var categorias = await _categoriaRepository.GetAllAsync(x => x.Nome.Contains(name ?? string.Empty));
        return Ok(categorias);
    }

    [HttpGet("{id}", Name = "Obtém uma categoria pelo seu id")]
    [ProducesResponseType(typeof(Categoria), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        var categoria = await _categoriaRepository.GetByIdAsync(id);
        if (categoria == null)
        {
            return NotFound();
        }
        
        return Ok(categoria);
    }

    [HttpPost(Name = "Cria uma nova categoria")]
    [ProducesResponseType(typeof(Categoria), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] Categoria categoria)
    {
        var categoriaCriada = await _categoriaRepository.AddAsync(categoria);
        return CreatedAtAction(nameof(GetById), new { id = categoriaCriada.Id }, categoriaCriada);
    }

    [HttpPut("{id}", Name = "Atualiza uma categoria")]
    [ProducesResponseType(typeof(Categoria), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] Categoria categoria)
    {
        var categoriaCriada = await _categoriaRepository.UpdateAsync(categoria);
        return Ok(categoriaCriada);
    }

    [HttpDelete("{id}", Name = "Deleta uma categoria")]
    [ProducesResponseType(typeof(Categoria), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        var categoriaDeletada = await _categoriaRepository.DeleteAsync(id);
        return Ok(categoriaDeletada);
    }
}