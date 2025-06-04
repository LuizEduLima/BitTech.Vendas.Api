using BitTech.Vendas.Api.Domain.Validators;
using FluentValidation.Results;

namespace BitTech.Vendas.Api.Domain.Models;
public class Venda : Entity
{
    public List<ItemVenda>? Itens { get; set; } = [];  
    public decimal ValorTotal => Itens.Any() ? Itens.Sum(i => i.ValorTotal) : 0m;

    protected override ValidationResult Validator => new VendaValidator().Validate(this);
}