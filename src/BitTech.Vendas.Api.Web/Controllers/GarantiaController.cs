using BitTech.Vendas.Api.Application.Dtos.Garantia;
using BitTech.Vendas.Api.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BitTech.Vendas.Api.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GarantiaController(IGarantiaServices garantiaServices) : ControllerBase
{
    private readonly IGarantiaServices _garantiaServices = garantiaServices;

    [HttpGet("obter-todos")]
    [ProducesResponseType(typeof(IEnumerable<GarantiaDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<IEnumerable<GarantiaDto>>> GetAll()
    {
        var result = await _garantiaServices.GetAllAsync();
        if (result.IsFailure)
            return BadRequest(new { errors = result.Errors });

        return Ok(result.Data);
    }

    [HttpGet("obter-por-id/{id:guid}")]
    [ProducesResponseType(typeof(GarantiaDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<GarantiaDto>> GetById(Guid id)
    {
        var result = await _garantiaServices.GetByIdAsync(id);

        if (result.IsFailure)
            return BadRequest(new { errors = result.Errors });

        return Ok(result.Data);
    }

    [HttpPost("novo")]
    [ProducesResponseType(typeof(GarantiaDto), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<GarantiaDto>> Create([FromBody] CreateGarantiaDto createDto)
    {
        var result = await _garantiaServices.CreateAsync(createDto);

        if (result.IsFailure)
            return BadRequest(new { errors = result.Errors });

        return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
    }

    [HttpPut("atualizar")]
    [ProducesResponseType(typeof(GarantiaDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<GarantiaDto>> Update([FromBody] UpdateGarantiaDto updateDto)
    {
        var result = await _garantiaServices.UpdateAsync(updateDto);

        if (result.IsFailure)
        {
            return BadRequest(new { errors = result.Errors });
        }

        return Ok(result.Data);
    }

    [HttpDelete("excluir/{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _garantiaServices.DeleteAsync(id);

        if (result.IsFailure)
            return BadRequest(new { errors = result.Errors });

        return NoContent();
    }
}