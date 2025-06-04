namespace BitTech.Vendas.Api.Application.Dtos.Garantia;

public record UpdateGarantiaDto(Guid Id, string Nome, decimal Valor, int Prazo);
