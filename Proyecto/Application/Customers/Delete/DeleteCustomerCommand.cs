namespace Application.Customers.Delete;

using ErrorOr;
using MediatR;

public record DeleteCustomerCommand(Guid Id) : IRequest<ErrorOr<Unit>>{}