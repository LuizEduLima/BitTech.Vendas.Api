using BitTech.Vendas.Api.Domain.Models;
using FluentValidation;

namespace BitTech.Vendas.Api.Domain.Validators;

public class ProdutoValidator : AbstractValidator<Produto>
{
    public ProdutoValidator()
    {
        RuleFor(p => p.Nome)
            .NotEmpty()
            .WithMessage("O nome do produto é obrigatório.")
            .MaximumLength(200)
            .WithMessage("O nome do produto deve ter no máximo 200 caracteres.");

        RuleFor(p => p.Valor)
            .GreaterThan(0)
            .WithMessage("O valor do produto deve ser maior que zero.")
            .ScalePrecision(2, 10)
            .WithMessage("O valor do produto deve ter no máximo 2 casas decimais.");

        RuleFor(p => p.EstoqueMinimo)
            .GreaterThanOrEqualTo(5)
            .WithMessage("O estoque mínimo deve ser maior ou igual a 5.");

        RuleFor(p => p.EstoqueMaximo)
            .GreaterThan(0)
            .WithMessage("O estoque máximo deve ser maior que zero.")
            .GreaterThanOrEqualTo(p => p.EstoqueMinimo)
            .WithMessage("O estoque máximo deve ser maior ou igual ao estoque mínimo.");      

        RuleFor(p => p.Fornecedor)
            .NotEmpty()
            .WithMessage("O fornecedor é obrigatório.")
            .MaximumLength(150)
            .WithMessage("O nome do fornecedor deve ter no máximo 150 caracteres.");

       
        RuleFor(p => p.SaldoEmEstoque)
            .Must((produto, saldo) => saldo >= produto.EstoqueMinimo)
            .WithMessage("Atenção: O saldo em estoque está abaixo do estoque mínimo.")
            .WithSeverity(Severity.Warning);

        RuleFor(p => p.SaldoEmEstoque)
          .Must((produto, saldo) => saldo >= produto.EstoqueMinimo && saldo <= produto.EstoqueMaximo)
          .WithMessage("Atenção: O saldo em estoque deve ser igual ou menor que Estoque Máximo e maior que Estoque mínimo.")
          .WithSeverity(Severity.Warning);
    }
}
