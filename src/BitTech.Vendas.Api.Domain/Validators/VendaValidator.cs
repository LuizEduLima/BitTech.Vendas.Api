using BitTech.Vendas.Api.Domain.Models;
using FluentValidation;

namespace BitTech.Vendas.Api.Domain.Validators;

public class VendaValidator : AbstractValidator<Venda>
{
    public VendaValidator()
    {
        RuleFor(v => v.Itens)
            .NotNull()
            .WithMessage("A lista de itens não pode ser nula.")
            .NotEmpty()
            .WithMessage("A venda deve conter pelo menos um item.");

        RuleForEach(v => v.Itens)
            .SetValidator(new ItemVendaValidator())
            .When(v => v.Itens != null);

        // Validação do valor total
        RuleFor(v => v.ValorTotal)
            .GreaterThan(0)
            .WithMessage("O valor total da venda deve ser maior que zero.")
            .When(v => v.Itens != null && v.Itens.Any());

        // Validação customizada para limite máximo de itens
        RuleFor(v => v.Itens)
            .Must(itens => itens == null || itens.Count <= 50)
            .WithMessage("Uma venda não pode ter mais de 50 itens.");

        // Validação para evitar produtos duplicados
        RuleFor(v => v.Itens)
            .Must(itens => itens == null ||
                  itens.GroupBy(i => i.ProdutoId).All(g => g.Count() == 1))
            .WithMessage("Não é permitido ter o mesmo produto mais de uma vez na venda. Ajuste a quantidade do item existente.");
    }
}
