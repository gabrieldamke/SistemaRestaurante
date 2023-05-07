using Domain.Entities;
using Domain.Interfaces;
using Domain.Types;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1;

[ApiVersion("1.0")]
public class ProdutoController : ApiBaseController
{
    private readonly IRepository<Produto> _produtoRepository;

    public ProdutoController(IRepository<Produto> produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    [HttpGet(Name = "Obtém todos os produtos de forma paginada")]
    [ProducesResponseType(typeof(PaginatedList<Produto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllPaginated(int pageIndex = 1, int pageSize = 10)
    {
        var produtos = await _produtoRepository.GetPaginatedListAsync(pageIndex, pageSize);
        return Ok(produtos);
    }

    [HttpGet("filter", Name = "Obtém todos os produtos com filtro")]
    [ProducesResponseType(typeof(IEnumerable<Produto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllFiltered([FromQuery] string? name)
    {
        var produtos = await _produtoRepository.GetAllAsync(x => x.Nome.Contains(name ?? string.Empty));
        return Ok(produtos);
    }

    [HttpGet("{id}", Name = "Obtém um produto pelo seu id")]
    [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        var produto = await _produtoRepository.GetByIdAsync(id);
        if (produto == null)
        {
            return NotFound();
        }
        
        return Ok(produto);
    }

    [HttpPost(Name = "Cria um novo produto")]
    [ProducesResponseType(typeof(Produto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] Produto produto)
    {
        var produtoCriado = await _produtoRepository.AddAsync(produto);
        return CreatedAtAction(nameof(GetById), new { id = produtoCriado.Id }, produtoCriado);
    }

    [HttpPut("{id}", Name = "Atualiza um produto")]
    [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] Produto produto)
    {
        var produtoAtualizado = await _produtoRepository.UpdateAsync(produto);
        return Ok(produtoAtualizado);
    }

    [HttpDelete("{id}", Name = "Deleta um produto")]
    [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        var produtoDeletado = await _produtoRepository.DeleteAsync(id);
        return Ok(produtoDeletado);
    }
}