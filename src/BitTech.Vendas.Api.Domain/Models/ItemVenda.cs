using BitTech.Vendas.Api.Domain.Validators;
using FluentValidation.Results;

namespace BitTech.Vendas.Api.Domain.Models;

public class ItemVenda : Entity
{
    public Guid ProdutoId { get; set; }
    public Produto? Produto { get; set; }
    public int Quantidade { get; set; }
    public Guid GarantiaId { get; set; }    
    public Garantia? Garantia { get; set; }
    public decimal ValorUnitario => Produto?.Valor ?? 0;
    public decimal ValorTotal => (Quantidade * ValorUnitario) + (Garantia?.Valor ?? 0);

    protected override ValidationResult Validator => new ItemVendaValidator().Validate(this);
}