namespace BitTech.Vendas.Api.Application.Dtos.Venda;

public record VendaDto(Guid Id, List<ItemVendaDto> Itens)
{
    public decimal ValorTotal { get; set; }
}
