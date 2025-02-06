using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Middlewares;

// Clase para el manejo de excepciones globales
public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger; // Logger para el middleware

    // Constructor de la clase 
    public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger) => _logger = logger;

    // Método para invocar el middleware
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context); // Invocar el siguiente middleware
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message); // Loggear la excepción
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // Establecer el código de estado
            ProblemDetails problem = new()
            {
                Title = "An error occurred. Server error",
                Detail = "An internal server has ocurred: " + ex.Message,
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Server error"
            };

            string json = JsonSerializer.Serialize(problem); // Serializar el objeto a JSON
            context.Response.ContentType = "application/json"; // Establecer el tipo de contenido

            await context.Response.WriteAsync(json); // Escribir la respuesta
        }
    }

}