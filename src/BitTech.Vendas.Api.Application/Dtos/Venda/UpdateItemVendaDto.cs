using BitTech.Vendas.Api.Application.Dtos.Garantia;

namespace BitTech.Vendas.Api.Application.Dtos.Venda;

public record UpdateItemVendaDto(Guid Id, Guid ProdutoId, int Quantidade, Guid? GarantiaId) { }
