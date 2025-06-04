using BitTech.Vendas.Api.Application.Middleware;
using Microsoft.AspNetCore.Builder;

namespace BitTech.Vendas.Api.Application.Setup;

public static class ErrorHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
     => builder.UseMiddleware<ErrorHandlingMiddleware>();
}