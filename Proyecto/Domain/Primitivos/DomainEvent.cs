using MediatR;

namespace Domain.Primitivos;

public record DomainEvent(Guid Id) : INotification;