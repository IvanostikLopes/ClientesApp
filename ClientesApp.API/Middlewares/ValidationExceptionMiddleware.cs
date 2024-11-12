using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace ClientesApp.API.Middlewares
{
    /// <summary>
    /// Middleware para tratar exceções de validação.
    /// </summary>
    public class ValidationExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        // RequestDelegate é um delegate de requisição
        // Toda vez que bater uma requisição na minha API, ele será acionado
        public ValidationExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // InvokeAsync é o meu try/cach. Ele será chamado sempre que 
        // minha API receber uma requisição
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                // Capitura (intercepta) e trata o erro
                await HandleValidationExceptionAsync(context, ex);
            }
        }

        private static Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            // Devolve o código do request
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            // Devolve uma resposta do tipo json
            context.Response.ContentType = "application/json";

            // Erros do Fluent Validation
            var errors = exception.Errors
                .Select(e => new
                {
                    Field = e.PropertyName,
                    ErrorMessage = e.ErrorMessage,
                    Severity = e.Severity.ToString()
                });

            var errorResponse = new
            {
                Message = "Ocorreram erros de validação.",
                Errors = errors
            };

            // Serealizo e devolvo em json
            var jsonResponse = JsonSerializer.Serialize(errorResponse);

            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
