using BitTech.Vendas.Api.Application.Dtos.Venda;
using BitTech.Vendas.Api.Application.Services;

namespace BitTech.Vendas.Api.Application.Interfaces;

public interface IVendaServices
{
    Task<Result<IEnumerable<VendaDto>>> GetAllAsync();
    Task<Result<VendaDto>> GetByIdAsync(Guid id);
    Task<Result<VendaDto>> CreateAsync(CreateVendaDto createDto);
    Task<Result<VendaDto>> UpdateAsync(UpdateVendaDto updateVendaDto);
    Task<Result<bool>> DeleteAsync(Guid id);
}