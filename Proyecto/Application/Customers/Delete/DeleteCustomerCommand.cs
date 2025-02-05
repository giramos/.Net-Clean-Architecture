namespace Application.Customers.Delete

using MediatR;

public record DeleteCustomerCommand(Guid Id) : IRequest<Unit>;