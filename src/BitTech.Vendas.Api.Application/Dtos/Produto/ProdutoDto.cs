namespace BitTech.Vendas.Api.Application.Dtos.Produto;

public record ProdutoDto(Guid Id, string Nome, decimal Valor, int EstoqueMinimo, int EstoqueMaximo, int SaldoEmEstoque, string Fornecedor, bool PossuiGarantia);
