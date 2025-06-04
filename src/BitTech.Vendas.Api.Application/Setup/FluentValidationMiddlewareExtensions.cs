using BitTech.Vendas.Api.Application.Middleware;
using Microsoft.AspNetCore.Builder;

namespace BitTech.Vendas.Api.Application.Setup;

public static class FluentValidationMiddlewareExtensions
{
    public static IApplicationBuilder UseFluentValidation(this IApplicationBuilder builder)
    => builder.UseMiddleware<FluentValidationMiddleware>();
}
