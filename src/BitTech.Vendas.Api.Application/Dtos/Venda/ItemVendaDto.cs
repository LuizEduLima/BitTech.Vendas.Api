using BitTech.Vendas.Api.Application.Dtos.Garantia;

namespace BitTech.Vendas.Api.Application.Dtos.Venda;

public record ItemVendaDto(Guid Id,Guid ProdutoId, int Quantidade, decimal ValorUnitario,  GarantiaDto Garantia)
{
    public decimal  ValorTotal { get; set; }
}
