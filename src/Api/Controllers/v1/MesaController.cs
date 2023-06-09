﻿using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Types;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1;

/// <summary>
/// Mesas
/// </summary>
[ApiVersion("1.0")]
public class MesaController : ApiBaseController
{
    private readonly IEntityRepository<Mesa> _mesaEntityRepository;
    private readonly IEntityRepository<Atendimento> _atendimentoEntityRepository;

    public MesaController(IEntityRepository<Mesa> mesaEntityRepository, IEntityRepository<Atendimento> atendimentoEntityRepository)
    {
        _mesaEntityRepository = mesaEntityRepository;
        _atendimentoEntityRepository = atendimentoEntityRepository;
    }

    /// <summary>
    /// Obtém as vendas por Mesa
    /// </summary>
    [HttpGet("SalesByMesa")]
    public async Task<ActionResult<List<object>>> GetSalesByMesa()
    {
        var atendimentos = await _atendimentoEntityRepository.GetAllAsync();
        var mesas = await _mesaEntityRepository.GetAllAsync();

        var salesByMesa = mesas.Select(
            mesa =>
                new
                {
                    mesaId = mesa.Id,
                    total_sales = atendimentos
                        .Where(atendimento => atendimento.MesaId == mesa.Id)
                        .SelectMany(atendimento => atendimento.Produtos)
                        .Sum(atendimentoProduto => atendimentoProduto.Preco)
                }
        );

        return Ok(salesByMesa.ToList<object>());
    }

    /// <summary>
    /// Obtém todas as mesas de forma paginada
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <param name="includeAll"></param>
    /// <returns></returns>
    [HttpGet(Name = "GetAllMesasPaginated")]
    [ProducesResponseType(typeof(PaginatedList<Mesa>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllPaginated(
        int pageIndex = 1,
        int pageSize = 10,
        bool includeAll = false
    )
    {
        var mesas = await _mesaEntityRepository.GetPaginatedListAsync(
            pageIndex,
            pageSize,
            includeAll: includeAll
        );
        return Ok(mesas);
    }

    /// <summary>
    /// Obtém todas as mesas com filtro
    /// </summary>
    /// <param name="statusMesa"></param>
    /// <param name="numero"></param>
    /// <param name="includeAll"></param>
    /// <returns></returns>
    [HttpGet("filter", Name = "GetAllMesasFiltered")]
    [ProducesResponseType(typeof(IEnumerable<Mesa>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllFiltered(
        StatusMesa statusMesa,
        int numero,
        bool includeAll = false
    )
    {
        var mesas = await _mesaEntityRepository.GetAllAsync(
            x => x.Status == statusMesa && x.Numero == numero,
            includeAll: includeAll
        );
        return Ok(mesas);
    }

    /// <summary>
    /// Obtém uma mesa pelo seu id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="includeAtendimentos"></param>
    /// <returns></returns>
    [HttpGet("{id}", Name = "GetMesaById")]
    [ProducesResponseType(typeof(Mesa), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id, bool includeAtendimentos = false)
    {
        var mesa = await _mesaEntityRepository.GetByIdAsync(id, includeAtendimentos);
        if (mesa == null)
        {
            return NotFound();
        }

        return Ok(mesa);
    }

    /// <summary>
    /// Cria uma nova mesa
    /// </summary>
    /// <param name="mesa"></param>
    /// <returns></returns>
    [HttpPost(Name = "CreateMesa")]
    [ProducesResponseType(typeof(Mesa), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] Mesa mesa)
    {
        var mesaCriada = await _mesaEntityRepository.AddAsync(mesa);
        return CreatedAtAction(nameof(GetById), new { id = mesaCriada.Id }, mesaCriada);
    }

    /// <summary>
    /// Atualiza uma mesa
    /// </summary>
    /// <param name="id"></param>
    /// <param name="mesa"></param>
    /// <returns></returns>
    [HttpPut("{id}", Name = "UpdateMesa")]
    [ProducesResponseType(typeof(Mesa), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] Mesa mesa)
    {
        var mesaCriada = await _mesaEntityRepository.UpdateAsync(mesa);
        return Ok(mesaCriada);
    }

    /// <summary>
    /// Deleta uma mesa
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}", Name = "DeleteMesa")]
    [ProducesResponseType(typeof(Mesa), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        var mesaDeletada = await _mesaEntityRepository.DeleteAsync(id);
        return Ok(mesaDeletada);
    }

    /// <summary>
    /// Abre uma mesa para torná-la ocupada
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPatch("{id}/open", Name = "OpenMesa")]
    [ProducesResponseType(typeof(Mesa), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Open(int id)
    {
        var mesa = await _mesaEntityRepository.GetByIdAsync(id);
        if (mesa == null)
        {
            return NotFound();
        }

        if (mesa.Status != StatusMesa.Livre)
        {
            return BadRequest("Mesa não está livre");
        }

        mesa.Status = StatusMesa.Ocupada;
        mesa.HoraAbertura = DateTime.Now;
        var mesaAtualizada = await _mesaEntityRepository.UpdateAsync(mesa);
        return Ok(mesaAtualizada);
    }

    /// <summary>
    /// Reserva uma mesa para torná-la reservada
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPatch("{id}/reserve", Name = "ReserveMesa")]
    [ProducesResponseType(typeof(Mesa), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Reserve(int id, DateTime horaReserva)
    {
        var mesa = await _mesaEntityRepository.GetByIdAsync(id);
        if (mesa == null)
        {
            return NotFound();
        }

        if (mesa.Status != StatusMesa.Livre)
        {
            return BadRequest("Mesa não está livre");
        }

        mesa.Status = StatusMesa.Reservada;
        mesa.HoraAbertura = horaReserva;
        var mesaAtualizada = await _mesaEntityRepository.UpdateAsync(mesa);
        return Ok(mesaAtualizada);
    }

    /// <summary>
    /// Fecha a mesa para torná-la livre
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPatch("{id}/close", Name = "CloseMesa")]
    [ProducesResponseType(typeof(Mesa), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Close(int id)
    {
        var mesa = await _mesaEntityRepository.GetByIdAsync(id);
        if (mesa == null)
        {
            return NotFound();
        }

        if (mesa.Status != StatusMesa.Ocupada)
        {
            return BadRequest("Mesa não está ocupada");
        }

        mesa.Status = StatusMesa.Livre;
        mesa.HoraAbertura = null;
        var mesaAtualizada = await _mesaEntityRepository.UpdateAsync(mesa);
        return Ok(mesaAtualizada);
    }
}
