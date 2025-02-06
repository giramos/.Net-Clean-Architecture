using Application.Customers.Create;
using Domain.Customer;
using Domain.Primitivos;

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
    public void HandlerCreateCustomer_WhenPhoneNumberHasBadFormat_ShouldReturnValidationError()
    {

    }
}