using Application;
using Domain.Customer;
using Domain.Primitivos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    // clase que implementa la interfaz IApplicationDbContext y la interfaz IUnitOfWork
    public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
    {
        private readonly IPublisher _publisher; // propiedad de solo lectura para acceder al publicador
        public ApplicationDbContext(DbContextOptions options, IPublisher publisher) : base(options)
        {
            // asignar el publicador a la propiedad
            _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
        }

        // propiedad para acceder a la tabla de clientes
        public DbSet<Customer> Customers { get; set; }

        // método para configurar las entidades
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // aplicar las configuraciones de las entidades
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        // método para guardar los cambios en la base de datos
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            // obtener los eventos de dominio de las entidades que implementan AggregateRoot
            var domainEvents = ChangeTracker.Entries<AggregateRoot>()
                .Select(e => e.Entity)
                .Where(e => e.GetDomainEvents().Any())
                .SelectMany(e => e.GetDomainEvents());

            // guardar los cambios en la base de datos
            var result = await base.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken); // publicar los eventos de dominio
            }

            return result;
        }
    }
}