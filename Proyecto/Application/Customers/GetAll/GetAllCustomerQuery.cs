using Customers.Common;
using ErrorOr;
using MediatR;


namespace Application.Customers.GetAll;
// Clase sellada que implementa la interfaz IRequest
public record GetAllCustomersQuery() : IRequest<ErrorOr<IReadOnlyList<CustomerResponse>>>;