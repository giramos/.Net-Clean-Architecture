namespace Domain.Primitivos;

public interface IUnitOfWork
{
    // método para guardar los cambios en la base de datos
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}