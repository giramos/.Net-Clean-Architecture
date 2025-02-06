using Domain.Customer;
using Domain.ObjetosValor;
using Domain.Primitivos;
using ErrorOr;
using MediatR;
using Domain.DomainErrors;

namespace Application.Customers.Create;

// Clase sellada que implementa la interfaz IRequestHandler
public sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ErrorOr<Unit>>
{
    private readonly ICustomerRepository _customerRepository; // Se declara una variable de solo lectura de tipo ICustomerRepository
    private readonly IUnitOfWork _unitOfWork; // Se declara una variable de solo lectura de tipo IUnitOfWork

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        // Se inicializan las variables
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }


    // Método que se encarga de manejar la solicitud
    public async Task<ErrorOr<Unit>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {

        // Se valida que el nombre no sea nulo o vacío
        if (PhoneNumber.Create(request.PhoneNumber) is not PhoneNumber phoneNumber)
        {
            // throw new Exception("Phone number is required. " + nameof(PhoneNumber))
            // return Error.Validation("Customer.Phone " + "Customer phone number is required. Format valid [9 digits]"); // Se retorna un error de validación
            return Errors.Customer.PhoneNumberWithBadFormat; // Se retorna un error de validación
        }

        var address = Address.Create(request.Street, request.City, request.State, request.Country, request.ZipCode);
        // Se valida que la dirección no sea nula
        if (address is null)
        {
            // throw new Exception("Address is required. " + nameof(Address));
            // return Error.Validation("Customer.Address: " + "Customer address is required."); // Se retorna un error de validación
            return Errors.Customer.AddressIsRequired; // Se retorna un error de validación
        }

        var customer = new Customer(new CustomerId(Guid.NewGuid()), request.Name, request.LastName, request.Email, phoneNumber, address);
        if (customer is null)
        {
            // throw new Exception("Customer.Customer: " + "Customer is null");
            // return Error.Validation("Customer.Customer: " + "Customer is null"); // Se retorna un error de validación
            return Errors.Customer.CustomerIsNull; // Se retorna un error de validación
        }

        await _customerRepository.Add(customer); // Se agrega el cliente
        await _unitOfWork.SaveChangesAsync(cancellationToken); // Se guardan los cambios en la base de datos

        return Unit.Value;
    }

}