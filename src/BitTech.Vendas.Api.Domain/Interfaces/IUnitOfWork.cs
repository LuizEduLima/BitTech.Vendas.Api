namespace BitTech.Vendas.Api.Domain.Interfaces;

public interface IUnitOfWork
{
    IGarantiaRepository GarantiaRepository { get; }
    IProdutoRepository ProdutoRepository { get; }
    IVendaRepository VendaRepository { get; }
}
