using Application;
using Domain.Customer;
using Domain.Primitivos;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

//(1) Se crea una clase estática llamada DependencyInjection
public static class DependencyInjection
{
    //(2) Se crea un método de extensión llamado AddInfrastructure
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration); //Se llama al método AddPersistence
        return services;
    }

    //(3) Se crea un método de extensión llamado AddPersistence
    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        //Se agrega el contexto de la base de datos
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Database")));

        //Se agregan los servicios necesarios para la inyección de dependencias
        services.AddScoped<IApplicationDbContext>(sp =>
                sp.GetRequiredService<ApplicationDbContext>());
        // Se agrega el UnitOfWork como servicio
        services.AddScoped<IUnitOfWork>(sp =>
                sp.GetRequiredService<ApplicationDbContext>());

        //Se agregan los repositorios necesarios para la inyección de dependencias
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}