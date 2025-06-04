using BitTech.Vendas.Api.Application.Dtos.Produto;
using BitTech.Vendas.Api.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BitTech.Vendas.Api.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController(IProdutoServices produtoServices) : ControllerBase
{
    private readonly IProdutoServices _produtoServices = produtoServices;


    [HttpGet("obter-todos")]
    [ProducesResponseType(typeof(IEnumerable<ProdutoDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetAll()
    {
        var result = await _produtoServices.GetAllAsync();

        if (result.IsFailure)
            return BadRequest(new { errors = result.Errors });

        return Ok(result.Data);
    }

    [HttpGet("obter-por-id/{id:guid}")]
    [ProducesResponseType(typeof(ProdutoDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<ProdutoDto>> GetById(Guid id)
    {
        var result = await _produtoServices.GetByIdAsync(id);

        if (result.IsFailure)
            return BadRequest(new { errors = result.Errors });

        return Ok(result.Data);
    }

    [HttpPost("novo")]
    [ProducesResponseType(typeof(ProdutoDto), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<ProdutoDto>> Create([FromBody] CreateProdutoDto createDto)
    {
        var result = await _produtoServices.CreateAsync(createDto);

        if (result.IsFailure)
            return BadRequest(new { errors = result.Errors });

        return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
    }

    [HttpPut("atualizar")]
    [ProducesResponseType(typeof(ProdutoDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<ProdutoDto>> Update([FromBody] UpdateProdutoDto updateDto)
    {
        var result = await _produtoServices.UpdateAsync(updateDto);

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
        var result = await _produtoServices.DeleteAsync(id);

        if (result.IsFailure)
            return BadRequest(new { errors = result.Errors });

        return NoContent();
    }
}