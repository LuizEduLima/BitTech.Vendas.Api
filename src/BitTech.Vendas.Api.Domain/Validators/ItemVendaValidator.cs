using BitTech.Vendas.Api.Domain.Models;
using FluentValidation;

namespace BitTech.Vendas.Api.Domain.Validators;

// Validador para a classe ItemVenda
public class ItemVendaValidator : AbstractValidator<ItemVenda>
{
    public ItemVendaValidator()
    {
        RuleFor(i => i.ProdutoId)
            .NotEmpty()
            .WithMessage("O ID do produto é obrigatório.");

        RuleFor(i => i.Produto)
            .NotNull()
            .WithMessage("O produto é obrigatório.")
            .SetValidator(new ProdutoValidator())
            .When(i => i.Produto != null);

        RuleFor(i => i.Quantidade)
            .GreaterThan(0)
            .WithMessage("A quantidade deve ser maior que zero.")
            .LessThanOrEqualTo(1000)
            .WithMessage("A quantidade não pode ser superior a 1000 unidades.");

        // Validação da garantia quando presente
        RuleFor(i => i.Garantia)
            .SetValidator(new GarantiaValidator())
            .When(i => i.Garantia != null);

        // Validação customizada para verificar se o produto possui garantia
        RuleFor(i => i.Garantia)
            .Must((item, garantia) => item.Produto?.PossuiGarantia == true || garantia == null)
            .WithMessage("Apenas produtos que possuem garantia podem ter uma garantia associada.");

        // Validação de estoque disponível
        RuleFor(i => i.Quantidade)
            .Must((item, quantidade) => item.Produto?.SaldoEmEstoque >= quantidade)
            .WithMessage($"Quantidade solicitada maior que o estoque disponível.")
            .When(i => i.Produto != null);
    }
}
