using BitTech.Vendas.Api.Domain.Models;
using FluentValidation;

namespace BitTech.Vendas.Api.Domain.Validators;
public class GarantiaValidator : AbstractValidator<Garantia>
{
    public GarantiaValidator()
    {
        RuleFor(g => g.Nome)
            .NotEmpty()
            .WithMessage("O nome da garantia é obrigatório.")
            .MaximumLength(100)
            .WithMessage("O nome da garantia deve ter no máximo 100 caracteres.");

        RuleFor(g => g.Valor)
            .GreaterThanOrEqualTo(10)
            .WithMessage("O valor da garantia deve ser maior ou igual a 10 Reais.")
            .ScalePrecision(2, 10)
            .WithMessage("O valor da garantia deve ter no máximo 2 casas decimais.");

        RuleFor(g => g.Prazo)
            .GreaterThan(0)
            .WithMessage("O prazo da garantia deve ser maior que zero.")
            .LessThanOrEqualTo(120)
            .WithMessage("O prazo da garantia deve ser no máximo 120 meses.");
    }
}
