using Customers.Common;
using ErrorOr;
using MediatR;

namespace Application.Customers.GetById;

// Clase sellada que implementa la interfaz IRequest y recibe un Guid
public record GetCustomerByIdQuery(Guid Id) : IRequest<ErrorOr<CustomerResponse>>;