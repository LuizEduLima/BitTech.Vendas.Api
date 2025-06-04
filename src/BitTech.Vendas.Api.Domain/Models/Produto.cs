using BitTech.Vendas.Api.Domain.Validators;
using FluentValidation.Results;

namespace BitTech.Vendas.Api.Domain.Models;
public class Produto : Entity
{
    public string? Nome { get; set; }
    public decimal Valor { get; set; }
    public int EstoqueMinimo { get; set; }
    public int EstoqueMaximo { get; set; }
    public int SaldoEmEstoque { get; set; }
    public string? Fornecedor { get; set; }
    public bool PossuiGarantia { get; set; }

    protected override ValidationResult Validator => new ProdutoValidator().Validate(this);


    public void DebitarDoEstoque(int valor)
    {
        var valorDebito = valor < 0 ? valor * -1 : valor;

        if (!EstoqueInsuficiente(valor))
            SaldoEmEstoque -= valorDebito;
    }

    public void ReporEstoque(int valor)
    {
        var valorDebito = valor < 0 ? valor * -1 : valor;
        SaldoEmEstoque += valorDebito;
    }
    private bool EstoqueInsuficiente(int quantidade) => SaldoEmEstoque < quantidade;
}