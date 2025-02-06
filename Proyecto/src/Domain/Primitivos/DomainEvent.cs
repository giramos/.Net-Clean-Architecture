using MediatR;

namespace Domain.Primitivos;

// Clase sellada, es decir, no puede ser heredada
public record DomainEvent(Guid Id) : INotification;