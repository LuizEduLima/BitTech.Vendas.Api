using BitTech.Vendas.Api.Domain.Validators;
using FluentValidation.Results;

namespace BitTech.Vendas.Api.Domain.Models;

public class Garantia : Entity
{
    public string? Nome { get; set; }
    public decimal Valor { get; set; }
    public int Prazo { get; set; }

    protected override ValidationResult Validator => new GarantiaValidator().Validate(this);
}
