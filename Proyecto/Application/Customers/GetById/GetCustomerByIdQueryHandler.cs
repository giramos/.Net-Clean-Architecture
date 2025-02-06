using Customers.Common;
using Domain.Customer;
using ErrorOr;
using MediatR;

namespace Application.Customers.GetById;

// Clase sellada que implementa la interfaz IRequest y recibe un Guid
internal sealed class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, ErrorOr<CustomerResponse>>
{
    private readonly ICustomerRepository _customerRepository; // Se declara una variable de solo lectura de tipo ICustomerRepository

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    // Método que se encarga de manejar la solicitud
    public async Task<ErrorOr<CustomerResponse>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        // Se obtiene el cliente por el id proporcionado en la solicitud y se almacena en la variable customer de tipo Customer 
        Customer? customer = await _customerRepository.GetByIdAsync(new CustomerId(request.Id));

        // Se retorna un error si el cliente es nulo o un CustomerResponse si no lo es
        return customer is null
            ? Error.NotFound("Customer.NotFound", "The customer with the provide Id was not found.")
            : new CustomerResponse(
                customer.Id.Value,
                customer.FullName,
                customer.Email,
                customer.PhoneNumber.Value,
                new AddressResponse(
                    customer.Address.Street,
                    customer.Address.City,
                    customer.Address.State,
                    customer.Address.Country,
                    customer.Address.ZipCode)
            );
    }
}