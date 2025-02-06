using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Extensions;

// Clase de extensión para aplicar las migraciones a la base de datos
public static class MigrationExtensions
{
    // Método para aplicar las migraciones a la base de datos
    public static void ApplyMigrations(this WebApplication app){
        // Crear un alcance para acceder a los servicios
        using var scope = app.Services.CreateScope();
        // Obtener el contexto de la aplicación
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        // Aplicar las migraciones a la base de datos
        dbContext.Database.Migrate();
    }
}