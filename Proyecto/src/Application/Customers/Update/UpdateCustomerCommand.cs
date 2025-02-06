using ErrorOr;
using MediatR;

namespace Application.Customers.Update;

public record UpdateCustomerCommand(Guid Id,
    string Name,
    string LastName,
    string Email,
    string PhoneNumber,
    string Country,
    string State,
    string City,
    string Street,
    string ZipCode) : IRequest<ErrorOr<Unit>>;
