using Domain.Customer;
using Domain.ObjetosValor;
using Domain.Primitivos;
using MediatR;

namespace Application.Customers.Create;

// Clase sellada que implementa la interfaz IRequestHandler
internal sealed class CreateCustomerCommandHAndler : IRequestHandler<CreateCustomerCommand, Unit>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerCommandHAndler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }


    // Método que se encarga de manejar la solicitud
    public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        // Se valida que el nombre no sea nulo o vacío
        if (PhoneNumber.Create(request.PhoneNumber) is not PhoneNumber phoneNumber)
        {
            throw new Exception("Phone number is required. " + nameof(PhoneNumber));
        }

        var address = Address.Create(request.Street, request.City, request.State, request.Country, request.ZipCode);
        // Se valida que la dirección no sea nula
        if (address is null)
        {
            throw new Exception("Address is required. " + nameof(Address));
        }

        var customer = new Customer(new CustomerId(Guid.NewGuid()), request.Name, request.LastName, request.Email, phoneNumber, address);
        if (customer is null)
        {
            throw new Exception("Customer is required. " + nameof(Customer));
        }

        await _customerRepository.Add(customer); // Se agrega el cliente
        await _unitOfWork.SaveChangesAsync(cancellationToken); // Se guardan los cambios en la base de datos

        return Unit.Value;
    }
}