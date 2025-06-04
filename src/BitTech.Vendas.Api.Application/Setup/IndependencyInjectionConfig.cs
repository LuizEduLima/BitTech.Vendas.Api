using BitTech.Vendas.Api.Application.Interfaces;
using BitTech.Vendas.Api.Application.Services;
using BitTech.Vendas.Api.Data.Repositories;
using BitTech.Vendas.Api.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BitTech.Vendas.Api.Application.Setup;

public static class IndependencyInjectionConfig
{
    public static void AddAutoMapperConfig(this IServiceCollection services)
    => services.AddAutoMapper(Assembly.GetExecutingAssembly());

    public static void AddDependencyConfig(this IServiceCollection services)
    => services.AddServicesConfig();    

    private static void AddServicesConfig(this IServiceCollection services)
    {
        services.AddSingleton<IGarantiaServices, GarantiaServices>();
        services.AddSingleton<IProdutoServices, ProdutoServices>();
        services.AddSingleton<IVendaServices, VendaServices>();

        services.AddSingleton<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<IGarantiaRepository, GarantiaRepository>();
        services.AddSingleton<IProdutoRepository, ProdutoRepository>();
        services.AddSingleton<IVendaRepository, VendaRepository>();
        services.AddSingleton(typeof(IBaseRepository<>), typeof(BaseRepository<>));
    }
}