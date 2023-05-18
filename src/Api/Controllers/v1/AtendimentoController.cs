using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers.v1;

/// <summary>
/// Atendimentos
/// </summary>
[ApiVersion("1.0")]
public class AtendimentoController : ApiBaseController
{
    private readonly IEntityRepository<Atendimento> _atendimentoEntityRepository;
    private readonly IEntityRepository<Produto> _produtoEntityRepository;
    private readonly IEntityRepository<AtendimentoProduto> _atendimentoProdutoEntityRepository;

    public AtendimentoController(
        IEntityRepository<Atendimento> atendimentoEntityRepository,
        IEntityRepository<Produto> produtoEntityRepository,
        IEntityRepository<AtendimentoProduto> atendimentoProdutoEntityRepository
    )
    {
        _atendimentoEntityRepository = atendimentoEntityRepository;
        _produtoEntityRepository = produtoEntityRepository;
        _atendimentoProdutoEntityRepository = atendimentoProdutoEntityRepository;
    }

    /// <summary>
    /// Obtém o total de produtos vendidos
    /// </summary>
    [HttpGet("TotalProductsSold")]
    public async Task<ActionResult<List<object>>> GetTotalProductsSold()
    {
        var productsSold = await _atendimentoProdutoEntityRepository.GetQueryable()
            .AsNoTracking()
            .GroupBy(ap => ap.Produto.Nome)
            .Select(group => new
            {
                ProductName = group.Key,
                QuantitySold = group.Sum(ap => ap.Quantidade)
            })
            .ToListAsync<object>();

        return Ok(productsSold);
    }

    /// <summary>
    /// Obtém o total por categoria
    /// </summary>
    [HttpGet("TotalProductsSoldByCategory")]
    public async Task<ActionResult<List<object>>> GetTotalProductsSoldByCategory()
    {
        var productsSold = await _atendimentoProdutoEntityRepository.GetQueryable()
            .AsNoTracking()
            .Include(i => i.Produto)
            .ThenInclude(ti => ti.Categoria)
            .GroupBy(ap => ap.Produto.Categoria.Nome)
            .Select(group => new
            {
                CategoryName = group.Key,
                QuantitySold = group.Sum(ap => ap.Quantidade)
            })
            .ToListAsync<object>();

        return Ok(productsSold);
    }

    /// <summary>
    /// Obtém todos os atendimentos de forma paginada
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="includeAll"></param>
    /// <returns></returns>
    [HttpGet(Name = "GetAllAtendimentosPaginated")]
    [ProducesResponseType(typeof(PaginatedList<Atendimento>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllPaginated(
        int pageIndex = 1,
        int pageSize = 10,
        bool includeAll = false
    )
    {
        var atendimentos = await _atendimentoEntityRepository.GetPaginatedListAsync(
            pageIndex,
            pageSize,
            includeAll: includeAll
        );
        return Ok(atendimentos);
    }

    /// <summary>
    /// Obtém todos os atendimentos com filtro
    /// </summary>
    /// <param name="mesaId"></param>
    /// <param name="garcomId"></param>
    /// <param name="includeAll"></param>
    /// <returns></returns>
    [HttpGet("filter", Name = "GetAllAtendimentosFiltered")]
    [ProducesResponseType(typeof(IEnumerable<Atendimento>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllFiltered(
        [FromQuery] int? mesaId,
        [FromQuery] int? garcomId,
        bool includeAll = false
    )
    {
        var atendimentos = await _atendimentoEntityRepository.GetAllAsync(
            x => x.MesaId == mesaId && x.GarcomId == garcomId,
            includeAll: includeAll
        );
        return Ok(atendimentos);
    }

    /// <summary>
    /// Obtém um atendimento pelo seu id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}", Name = "GetAtendimentoById")]
    [ProducesResponseType(typeof(Atendimento), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        var atendimento = await _atendimentoEntityRepository.GetByIdAsync(id, includeAll: true);
        if (atendimento == null)
        {
            return NotFound();
        }

        return Ok(atendimento);
    }

    /// <summary>
    /// Cria um novo atendimento
    /// </summary>
    /// <param name="atendimento"></param>
    /// <returns></returns>
    [HttpPost(Name = "CreateAtendimento")]
    [ProducesResponseType(typeof(Atendimento), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] Atendimento atendimento)
    {
        var atendimentoCriado = await _atendimentoEntityRepository.AddAsync(atendimento);
        return CreatedAtAction(
            nameof(GetById),
            new { id = atendimentoCriado.Id },
            atendimentoCriado
        );
    }

    /// <summary>
    /// Atualiza um atendimento
    /// </summary>
    /// <param name="id"></param>
    /// <param name="atendimento"></param>
    /// <returns></returns>
    [HttpPut("{id}", Name = "UpdateAtendimento")]
    [ProducesResponseType(typeof(Atendimento), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] Atendimento atendimento)
    {
        var atendimentoAtualizado = await _atendimentoEntityRepository.UpdateAsync(atendimento);
        return Ok(atendimentoAtualizado);
    }

    /// <summary>
    /// Deleta um atendimento
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}", Name = "DeleteAtendimento")]
    [ProducesResponseType(typeof(Atendimento), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        var atendimentoDeletado = await _atendimentoEntityRepository.DeleteAsync(id);
        return Ok(atendimentoDeletado);
    }

    /// <summary>
    /// Abrir uma conta para atendimento
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPatch("{id}/open", Name = "OpenBill")]
    [ProducesResponseType(typeof(Atendimento), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> OpenBill(int id)
    {
        var atendimento = await _atendimentoEntityRepository.GetByIdAsync(id, includeAll: true);
        if (atendimento == null)
        {
            return NotFound();
        }

        atendimento.HoraAbertura = DateTime.Now;
        atendimento.Mesa!.Status = StatusMesa.Ocupada;
        var atendimentoAtualizado = await _atendimentoEntityRepository.UpdateAsync(atendimento);
        return Ok(atendimentoAtualizado);
    }

    /// <summary>
    /// Fechar conta de atendimento
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPatch("{id}/close", Name = "CloseBill")]
    [ProducesResponseType(typeof(Atendimento), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CloseBill(int id)
    {
        var atendimento = await _atendimentoEntityRepository.GetByIdAsync(id, includeAll: true);
        if (atendimento == null)
        {
            return NotFound();
        }

        atendimento.HoraFechamento = DateTime.Now;
        atendimento.Mesa!.Status = StatusMesa.Livre;
        var atendimentoAtualizado = await _atendimentoEntityRepository.UpdateAsync(atendimento);
        return Ok(atendimentoAtualizado);
    }

    /// <summary>
    /// Adicionar produto ao atendimento
    /// </summary>
    /// <param name="id"></param>
    /// <param name="produtoId"></param>
    /// <param name="quantidade"></param>
    /// <returns></returns>
    [HttpPut("{id}/add-produto", Name = "AddProduto")]
    [ProducesResponseType(typeof(AtendimentoProduto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddProduto(int id, int produtoId, int quantidade)
    {
        var atendimento = await _atendimentoEntityRepository.GetByIdAsync(id, includeAll: true);
        if (atendimento == null)
        {
            return NotFound();
        }

        var produto = await _produtoEntityRepository.GetByIdAsync(produtoId);
        if (produto == null)
        {
            return NotFound();
        }

        var item = new AtendimentoProduto
        {
            AtendimentoId = id,
            ProdutoId = produtoId,
            Quantidade = quantidade,
            Preco = produto.Preco
        };

        var atendimentoProduto = await _atendimentoProdutoEntityRepository.AddAsync(item);
        return Ok(atendimentoProduto);
    }

    /// <summary>
    /// Remover produto do atendimento
    /// </summary>
    /// <param name="id"></param>
    /// <param name="produtoId"></param>
    /// <returns></returns>
    [HttpPut("{id}/remove-produto", Name = "RemoveProduto")]
    [ProducesResponseType(typeof(AtendimentoProduto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveProduto(int id, int produtoId)
    {
        var atendimentoProduto = await _atendimentoProdutoEntityRepository
            .GetQueryable()
            .AsNoTracking()
            .Where(x => x.AtendimentoId == id && x.ProdutoId == produtoId)
            .FirstOrDefaultAsync();

        if (atendimentoProduto == null)
        {
            return NotFound();
        }

        var atendimentoProdutoDeletado = await _atendimentoProdutoEntityRepository.DeleteAsync(
            atendimentoProduto.Id
        );
        return Ok(atendimentoProdutoDeletado);
    }
}
