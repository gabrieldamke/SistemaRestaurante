using Domain.Entities;
using Domain.Interfaces;
using Domain.Types;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1;

/// <summary>
/// Produtos
/// </summary>
[ApiVersion("1.0")]
public class ProdutoController : ApiBaseController
{
    private readonly IEntityRepository<Produto> _produtoEntityRepository;

    public ProdutoController(IEntityRepository<Produto> produtoEntityRepository)
    {
        _produtoEntityRepository = produtoEntityRepository;
    }

    /// <summary>
    /// Obtém todos os produtos de forma paginada
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="includeAll"></param>
    /// <returns></returns>
    [HttpGet(Name = "GetAllProdutosPaginated")]
    [ProducesResponseType(typeof(PaginatedList<Produto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllPaginated(
        int pageIndex = 1,
        int pageSize = 10,
        bool includeAll = false
    )
    {
        var produtos = await _produtoEntityRepository.GetPaginatedListAsync(
            pageIndex,
            pageSize,
            includeAll: includeAll
        );
        return Ok(produtos);
    }

    /// <summary>
    /// Obtém todos os produtos com filtro
    /// </summary>
    /// <param name="name"></param>
    /// <param name="includeAll"></param>
    /// <returns></returns>
    [HttpGet("filter", Name = "GetAllProdutosFiltered")]
    [ProducesResponseType(typeof(IEnumerable<Produto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllFiltered(
        [FromQuery] string? name,
        bool includeAll = false
    )
    {
        var produtos = await _produtoEntityRepository.GetAllAsync(
            x => x.Nome.Contains(name ?? string.Empty),
            includeAll: includeAll
        );
        return Ok(produtos);
    }

    /// <summary>
    /// Obtém um produto pelo seu id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}", Name = "GetProdutoById")]
    [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        var produto = await _produtoEntityRepository.GetByIdAsync(id, includeAll: true);
        if (produto == null)
        {
            return NotFound();
        }

        return Ok(produto);
    }

    /// <summary>
    /// Cria um novo produto
    /// </summary>
    /// <param name="produto"></param>
    /// <returns></returns>
    [HttpPost(Name = "CreateProduto")]
    [ProducesResponseType(typeof(Produto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] Produto produto)
    {
        var produtoCriado = await _produtoEntityRepository.AddAsync(produto);
        return CreatedAtAction(nameof(GetById), new { id = produtoCriado.Id }, produtoCriado);
    }

    /// <summary>
    /// Atualiza um produto
    /// </summary>
    /// <param name="id"></param>
    /// <param name="produto"></param>
    /// <returns></returns>
    [HttpPut("{id}", Name = "UpdateProduto")]
    [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] Produto produto)
    {
        var produtoAtualizado = await _produtoEntityRepository.UpdateAsync(produto);
        return Ok(produtoAtualizado);
    }

    /// <summary>
    /// Deleta um produto
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}", Name = "DeleteProduto")]
    [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        var produtoDeletado = await _produtoEntityRepository.DeleteAsync(id);
        return Ok(produtoDeletado);
    }
}
