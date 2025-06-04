namespace BitTech.Vendas.Api.Application.Dtos.Venda;

public record UpdateVendaDto(Guid Id,List<UpdateItemVendaDto> Itens);
