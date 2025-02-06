namespace Application.Customers.Delete
{
    using Domain.Customer;
    using Domain.Primitivos;
    using ErrorOr;
    using MediatR;
    using System;

    // Clase sellada que implementa la interfaz IRequestHandler
    internal sealed class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, ErrorOr<Unit>>
    {
        private readonly ICustomerRepository _customerRepository; // Se declara una variable de solo lectura de tipo ICustomerRepository
        private readonly IUnitOfWork _unitOfWork; // Se declara una variable de solo lectura de tipo IUnitOfWork

        // Constructor que recibe un ICustomerRepository y un IUnitOfWork
        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            //  Se valida que el customerRepository no sea nulo
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        // Método que se encarga de manejar la solicitud
        public async Task<ErrorOr<Unit>> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
        {
            // Se valida que el id no sea nulo
            if (await _customerRepository.GetByIdAsync(new CustomerId(command.Id)) is not Customer customer)
            {
                // throw new Exception("Customer not found.");
                return Error.NotFound("Customer.NotFound", "The customer with the provide Id was not found.");
            }

            _customerRepository.Delete(customer); // Se elimina el cliente

            await _unitOfWork.SaveChangesAsync(cancellationToken); // Se guardan los cambios en la base de datos

            return Unit.Value; // Se retorna un valor Unit
        }
    }
}


