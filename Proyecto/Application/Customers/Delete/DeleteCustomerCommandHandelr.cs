namespace Application.Customers.Delete
{
    using Domain.Customer;
    using Domain.Primitivos;
    using MediatR;
    using System;

    // Clase sellada que implementa la interfaz IRequestHandler
    internal sealed class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Unit>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        // Constructor que recibe un ICustomerRepository y un IUnitOfWork
        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            //  Se valida que el customerRepository no sea nulo
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        // Método que se encarga de manejar la solicitud
        public async Task<Unit> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
        {
            // Se valida que el id no sea nulo
            if (await _customerRepository.GetByIdAsync(new CustomerId(command.Id)) is not Customer customer)
            {
                throw new Exception("Customer not found.");
            }

            _customerRepository.Delete(customer); // Se elimina el cliente

            await _unitOfWork.SaveChangesAsync(cancellationToken); // Se guardan los cambios en la base de datos

            return Unit.Value; // Se retorna un valor Unit
        }
    }
}


