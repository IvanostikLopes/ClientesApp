using ClientesApp.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace ClientesApp.API.Middlewares
{
    /// <summary>
    /// Middleware para tratar a exceção ClienteNotFoundException.
    /// </summary>
    public class NotFoundExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public NotFoundExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ClienteNotFoundException ex)
            {
                await HandleClienteNotFoundExceptionAsync(context, ex);
            }
        }

        private static Task HandleClienteNotFoundExceptionAsync(HttpContext context, ClienteNotFoundException exception)
        {
            // Definir o status code como NotFound (404)
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            context.Response.ContentType = "application/json";

            // Construir a resposta de erro
            var errorResponse = new
            {
                Message = "Cliente não encontrado.",
                Details = exception.Message
            };

            // Serializar a resposta como JSON
            var jsonResponse = JsonSerializer.Serialize(errorResponse);

            return context.Response.WriteAsync(jsonResponse);
        }
    }

}
