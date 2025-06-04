using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace BitTech.Vendas.Api.Application.Middleware;

public class FluentValidationMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (FluentValidation.ValidationException validationException)
        {      

            await HandleFluentValidationAsync(context, validationException);
        }
    }

    private static async Task HandleFluentValidationAsync(HttpContext context, FluentValidation.ValidationException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        var errors = exception.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                group => group.Key,
                group => group.Select(e => e.ErrorMessage).ToArray()
            );

        var response = new ValidationErrorResponse
        {
            Title = "Erro de Validação",
            Status = (int)HttpStatusCode.BadRequest,
            Detail = "Um ou mais campos contêm valores inválidos",
            Type = "validation_error",
            Timestamp = DateTime.UtcNow,
            TraceId = context.TraceIdentifier,
            Errors = errors
        };

        var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });

        await context.Response.WriteAsync(jsonResponse);
    }
}
