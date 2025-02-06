using FluentValidation;

namespace Application.Customers.Update;
// Clase que contiene las reglas de validación para el comando de actualización de un cliente.
public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.") // El campo no puede estar vacío.
            .NotEqual(Guid.Empty).WithMessage("Id must not be empty."); // El campo no puede ser un Guid vacío.

        // Reglas de validación para el comando de creación de un cliente.
        RuleFor(x => x.Name)
            .NotEmpty() // El campo no puede estar vacío.
            .MaximumLength(50) // Longitud máxima del campo.
            .WithName("Name: "); // Nombre del campo en el mensaje de error.

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(50)
            .WithName("Last Name: ");

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress() // El campo debe ser una dirección de correo electrónico válida.
            .MaximumLength(255);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .MaximumLength(9)
            .WithName("Phone Number: ");

        RuleFor(x => x.Country)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.State)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.City)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Street)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.ZipCode)
            .NotEmpty()
            .MaximumLength(10)
            .WithName("Zip Code: ");
    }
}