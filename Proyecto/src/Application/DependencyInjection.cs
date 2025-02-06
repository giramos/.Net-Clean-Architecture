using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using FluentValidation;
using MediatR;
using Application.Command.Behaviors;

namespace Application;

// Clase que contiene los métodos de extensión para la inyección de dependencias.
public static class DependencyInjection
{
    // Método de extensión que añade los servicios de la aplicación.
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        // añadiendo los servicios de MediatR
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
        });

        // añadiendo los servicios de FluentValidation
        services.AddScoped(
            typeof(IPipelineBehavior<,>), // tipo de servicio a añadir al contenedor de servicios
            typeof(ValidationBehavior<,>) // tipo de implementación del servicio 
        );

        // añadiendo los servicios de FluentValidation
        services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();

        return services;
    }

}