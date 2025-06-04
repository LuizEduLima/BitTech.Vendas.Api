using BitTech.Vendas.Api.Application.Dtos.Garantia;
using BitTech.Vendas.Api.Application.Services;

namespace BitTech.Vendas.Api.Application.Interfaces;

public interface IGarantiaServices
{
    Task<Result<IEnumerable<GarantiaDto>>> GetAllAsync();
    Task<Result<GarantiaDto>> GetByIdAsync(Guid id);
    Task<Result<GarantiaDto>> CreateAsync(CreateGarantiaDto createDto);
    Task<Result<GarantiaDto>> UpdateAsync(UpdateGarantiaDto updateDto);
    Task<Result<bool>> DeleteAsync(Guid id);
}
