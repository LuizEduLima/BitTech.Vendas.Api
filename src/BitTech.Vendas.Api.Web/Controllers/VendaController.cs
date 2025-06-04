using BitTech.Vendas.Api.Application.Dtos.Venda;
using BitTech.Vendas.Api.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BitTech.Vendas.Api.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VendaController(IVendaServices vendaServices) : ControllerBase
{
    private readonly IVendaServices _vendaServices = vendaServices;

    [HttpGet("obter-todos")]
    [ProducesResponseType(typeof(IEnumerable<VendaDto>), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<IEnumerable<VendaDto>>> GetAll()
    {
        var result = await _vendaServices.GetAllAsync();
        return Ok(result.Data);
    }

    [HttpGet("obter-por-id/{id:guid}")]
    [ProducesResponseType(typeof(VendaDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<VendaDto>> GetById(Guid id)
    {
        var result = await _vendaServices.GetByIdAsync(id);

        if (result.IsFailure)
            return BadRequest(new { errors = result.Errors });

        return Ok(result.Data);
    }

    [HttpPost("novo")]
    [ProducesResponseType(typeof(VendaDto), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<VendaDto>> Create([FromBody] CreateVendaDto createDto)
    {
        var result = await _vendaServices.CreateAsync(createDto);

        if (result.IsFailure)
            return BadRequest(new { errors = result.Errors });

        return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
    }

    [HttpPut("atualizar")]
    [ProducesResponseType(typeof(VendaDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<VendaDto>> Update([FromBody] UpdateVendaDto vendaDto)
    {
        var result = await _vendaServices.UpdateAsync(vendaDto);

        if (result.IsFailure)
            return BadRequest(new { errors = result.Errors });

        return Ok(result.Data);
    }

    [HttpDelete("excluir/{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _vendaServices.DeleteAsync(id);

        if (result.IsFailure)
            return BadRequest(new { errors = result.Errors });

        return NoContent();
    }
}