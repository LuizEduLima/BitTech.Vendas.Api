using Microsoft.AspNetCore.Builder;

namespace BitTech.Vendas.Api.Application.Setup;

public static class MiddlewareConfiguration
{
    public static void ConfigureErrorHandling(this IApplicationBuilder app)
    {        
        app.UseFluentValidation();
        app.UseErrorHandling();
    }
}
