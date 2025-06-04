using BitTech.Vendas.Api.Domain.Models;
using BitTech.Vendas.Api.Domain.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BitTech.Vendas.Api.Application.Setup;

public static class ValidatorExtensions
{
    public static void AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<Garantia>, GarantiaValidator>();
        services.AddScoped<IValidator<Produto>, ProdutoValidator>();
        services.AddScoped<IValidator<ItemVenda>, ItemVendaValidator>();
        services.AddScoped<IValidator<Venda>, VendaValidator>();
    }        
}