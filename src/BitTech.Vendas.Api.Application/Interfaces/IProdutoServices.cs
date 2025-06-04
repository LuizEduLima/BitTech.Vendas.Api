using BitTech.Vendas.Api.Application.Dtos.Produto;
using BitTech.Vendas.Api.Application.Services;

namespace BitTech.Vendas.Api.Application.Interfaces;

public interface IProdutoServices
{
    Task<Result<IEnumerable<ProdutoDto>>> GetAllAsync();
    Task<Result<ProdutoDto>> GetByIdAsync(Guid id);
    Task<Result<ProdutoDto>> CreateAsync(CreateProdutoDto createDto);
    Task<Result<ProdutoDto>> UpdateAsync(UpdateProdutoDto updateDto);
    Task<Result<bool>> DeleteAsync(Guid id);
}
