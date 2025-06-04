using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace BitTech.Vendas.Api.Application.Middleware;
public class ErrorHandlingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = exception switch
        {
            ValidationException validationEx => new ErrorResponse
            {
                Title = "Erro de Validação",
                Status = (int)HttpStatusCode.BadRequest,
                Detail = validationEx.Message,
                Type = "validation_error",
                Timestamp = DateTime.UtcNow,
                TraceId = context.TraceIdentifier
            },
            ArgumentException argEx => new ErrorResponse
            {
                Title = "Argumento Inválido",
                Status = (int)HttpStatusCode.BadRequest,
                Detail = argEx.Message,
                Type = "argument_error",
                Timestamp = DateTime.UtcNow,
                TraceId = context.TraceIdentifier
            },
           
            InvalidOperationException invalidOpEx => new ErrorResponse
            {
                Title = "Operação Inválida",
                Status = (int)HttpStatusCode.UnprocessableEntity,
                Detail = invalidOpEx.Message,
                Type = "invalid_operation_error",
                Timestamp = DateTime.UtcNow,
                TraceId = context.TraceIdentifier
            },
            KeyNotFoundException notFoundEx => new ErrorResponse
            {
                Title = "Recurso Não Encontrado",
                Status = (int)HttpStatusCode.NotFound,
                Detail = notFoundEx.Message,
                Type = "not_found_error",
                Timestamp = DateTime.UtcNow,
                TraceId = context.TraceIdentifier
            },
            UnauthorizedAccessException unauthorizedEx => new ErrorResponse
            {
                Title = "Acesso Negado",
                Status = (int)HttpStatusCode.Unauthorized,
                Detail = unauthorizedEx.Message,
                Type = "unauthorized_error",
                Timestamp = DateTime.UtcNow,
                TraceId = context.TraceIdentifier
            },
            TimeoutException timeoutEx => new ErrorResponse
            {
                Title = "Timeout",
                Status = (int)HttpStatusCode.RequestTimeout,
                Detail = "A operação excedeu o tempo limite",
                Type = "timeout_error",
                Timestamp = DateTime.UtcNow,
                TraceId = context.TraceIdentifier
            },
            _ => new ErrorResponse
            {
                Title = "Erro Interno do Servidor",
                Status = (int)HttpStatusCode.InternalServerError,
                Detail = "Ocorreu um erro inesperado. Tente novamente mais tarde.",
                Type = "internal_server_error",
                Timestamp = DateTime.UtcNow,
                TraceId = context.TraceIdentifier
            }
        };

        context.Response.StatusCode = response.Status;

        var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });

        await context.Response.WriteAsync(jsonResponse);
    }
}

public class ErrorResponse
{
    public string Title { get; set; } = string.Empty;
    public int Status { get; set; }
    public string Detail { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string TraceId { get; set; } = string.Empty;
    public Dictionary<string, object>? Extensions { get; set; }
}


public class ValidationErrorResponse : ErrorResponse
{
    public Dictionary<string, string[]> Errors { get; set; } = new();
}


