using BitTech.Vendas.Api.Domain.Interfaces;

namespace BitTech.Vendas.Api.Data.Repositories;

public class UnitOfWork(
    IProdutoRepository produtoRepository,
    IGarantiaRepository garantiaRepository,
    IVendaRepository vendaRepository) : IUnitOfWork
{
    public IProdutoRepository ProdutoRepository { get; } = produtoRepository;
    public IGarantiaRepository GarantiaRepository { get; } = garantiaRepository;
    public IVendaRepository VendaRepository { get; } = vendaRepository;
}
