namespace Application;

using Domain.Customer;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Interfaz que define el contexto de la aplicación.
/// </summary>
public interface IApplicationDbContext
{
    public DbSet<Customer> Customers { get; set; } // Propiedad que representa la tabla de clientes en la base de datos.
    // Método que guarda los cambios en la base de datos.
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}