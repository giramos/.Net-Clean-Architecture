using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using FluentValidation;
using Web.Api.Middlewares;

namespace Web.API;
// Clase de extensión para la inyección de dependencias
public static class DependencyInjection
{
    // Método para agregar la inyección de dependencias
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        // Agregar los servicios de MVC
        services.AddControllers(); 
        // Agregar la validación de FluentValidation
        services.AddEndpointsApiExplorer(); 
        // Agregar la documentación de Swagger
        services.AddSwaggerGen();
        // Agregar la inyección de dependencias de FluentValidation
        services.AddTransient<GlobalExceptionHandlingMiddleware>();
        return services;
    }

}