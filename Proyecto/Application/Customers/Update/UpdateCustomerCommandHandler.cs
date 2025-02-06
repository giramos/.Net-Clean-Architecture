using Domain.Customer;
using Domain.ObjetosValor;
using Domain.Primitivos;
using ErrorOr;
using MediatR;

namespace Application.Customers.Update;
// Clase sellada que implementa la interfaz IRequest y recibe un Guid
internal sealed class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ErrorOr<Unit>>
{

    private readonly ICustomerRepository _customerRepository; // Se declara una variable de solo lectura de tipo ICustomerRepository
    private readonly IUnitOfWork _unitOfWork; // Se declara una variable de solo lectura de tipo IUnitOfWork

    public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    // Método que se encarga de manejar la solicitud
    public async Task<ErrorOr<Unit>> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
    {
        // Se valida que el id no sea nulo
        if (await _customerRepository.GetByIdAsync(new CustomerId(command.Id)) is not Customer customer)
        {
            return Error.NotFound("Customer.NotFound", "The customer with the provide Id was not found.");
        }

        // Se actualiza el cliente  con los datos proporcionados en la solicitud y se almacena en la variable x de tipo Customer 
        Customer x = Customer.Update(
              new CustomerId(command.Id),
              command.Name,
              command.LastName,
              command.Email,
              PhoneNumber.Create(command.PhoneNumber) ?? throw new Exception("Phone number is required. " + nameof(PhoneNumber)),
              Address.Create(command.Street, command.City, command.State, command.Country, command.ZipCode) ?? throw new Exception("Address is required. " + nameof(Address)
              )
          );

        _customerRepository.Update(x); // Se actualiza el cliente en la base de datos 

        await _unitOfWork.SaveChangesAsync(cancellationToken); // Se guardan los cambios en la base de datos  

        return Unit.Value; // Se retorna un valor Unit
    }
}