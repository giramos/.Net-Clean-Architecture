using MediatR;

namespace Application.Customers.Create
{
    // Es una clase sellada, es decir, no puede ser heredada
    public record CreateCustomerCommand(
        string Name,
        string LastName,
        string Email,
        string PhoneNumber,
        string Country,
        string State,
        string City,
        string Street,
        string ZipCode
    ) : IRequest<Unit>;

}