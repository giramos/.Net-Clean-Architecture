

using System.Linq;
using Customers.Common;
using Domain.Customer;
using ErrorOr;
using MediatR;

namespace Application.Customers.GetAll;
// Clase sellada que implementa la interfaz IRequestHandler
internal sealed class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, ErrorOr<IReadOnlyList<CustomerResponse>>>
{
    private readonly ICustomerRepository _customerRepository;   // Se declara una variable de solo lectura de tipo ICustomerRepository

    public GetAllCustomersQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    // Método que se encarga de manejar la solicitud
    public async Task<ErrorOr<IReadOnlyList<CustomerResponse>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        // Se obtienen todos los clientes y se convierten a una lista de CustomerResponse 
        IReadOnlyList<Customer> customers = (IReadOnlyList<Customer>)await _customerRepository.GetAll();

        // Se retorna la lista de CustomerResponse 
        return customers.Select(customer => new CustomerResponse(
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
            )).ToList();
    }
}