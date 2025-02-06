using FluentValidation;

namespace Application.Customers.Delete;
// Clase sellada que hereda de AbstractValidator y recibe un DeleteCustomerCommand
public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    // Constructor que inicializa la regla de validación
    public DeleteCustomerCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty(); // Se valida que el Id no sea nulo
    }
}