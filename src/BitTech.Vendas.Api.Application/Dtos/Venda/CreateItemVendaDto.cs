namespace BitTech.Vendas.Api.Application.Dtos.Venda;

public record CreateItemVendaDto(Guid ProdutoId, int Quantidade, Guid? GarantiaId);
