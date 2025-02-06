using System.Threading.Tasks;
using Application.Customers.Create;
using Domain.Customer;
using Domain.Primitivos;
using Domain.DomainErrors;

namespace Application.Customers.UnitTests;
// Clase de pruebas unitarias
public class CreateCustomerCommandHandlerUnitTest
{
    private readonly Mock<ICustomerRepository> _mockCustomerRepository; // Se declara una variable de solo lectura de tipo Mock<ICustomerRepository>

    private readonly Mock<IUnitOfWork> _mockUnitOfWork; // Se declara una variable de solo lectura de tipo Mock<IUnitOfWork>

    private readonly CreateCustomerCommandHandler _handler; // Se declara una variable de solo lectura de tipo CreateCustomerCommandHandler
    public CreateCustomerCommandHandlerUnitTest()
    {
        // Se inicializan las variables
        _mockCustomerRepository = new Mock<ICustomerRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _handler = new CreateCustomerCommandHandler(_mockCustomerRepository.Object, _mockUnitOfWork.Object);
    }
    // Qué vamos a testear
    // Escenario
    // Lo que debe devolver
    [Fact]
    public async Task HandlerCreateCustomer_WhenPhoneNumberHasBadFormat_ShouldReturnValidationError()
    {
        //Arrange
        //Se configura los parametros de entrada de nuestra prueba unitaria
        CreateCustomerCommand command = new CreateCustomerCommand("Name"
        , "LastName"
        , "email@email.com"
        , "1234567890"
        , "Country"
        , "State"
        , "City"
        , "Street"
        , "ZipCode");
        //Act
        // Se ejecuta el metodo a probar de nuestra prueba unitaria
        var result = await _handler.Handle(command, default);
        //Assert
        // Se verifica los datos de retorno de nuestra prueba unitaria
        result.IsError.Should().BeTrue(); // Debe ser verdadero
        result.FirstError.Type.Should().Be(ErrorType.Validation); // Debe ser de tipo validación
        result.FirstError.Code.Should().Be(Errors.Customer.PhoneNumberWithBadFormat.Code); // Debe ser igual a "Customer.Phone"
        result.FirstError.Description.Should().Be(Errors.Customer.PhoneNumberWithBadFormat.Description); // Debe ser igual a "Customer phone number is required. Format valid [9 digits]"

    }
}