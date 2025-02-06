namespace Domain.Primitivos;

public abstract class AggregateRoot
{
    // lista de eventos de dominio
    private readonly List<DomainEvent> _domainEvents = new();

    // propiedad de solo lectura para acceder a la lista de eventos
    public ICollection<DomainEvent> GetDomainEvents() => _domainEvents;

    // método para levantar eventos de dominio
    protected void Raise(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}