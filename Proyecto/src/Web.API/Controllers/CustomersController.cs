using Application.Customers.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Controller;
using Web.API.Controllers;

namespace Api.Web.Controllers;

// Controlador para manejar las peticiones relacionadas con los clientes
[Route("customers")] // Ruta base para las peticiones relacionadas con los clientes
public class CustomersController : ApiController
{
    private readonly ISender _mediator; // Mediator para enviar comandos y consultas

    public CustomersController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    // Crear un cliente con los datos recibidos en el cuerpo de la petición
    [HttpPost] // POST /customers
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
    {
        var createResult = await _mediator.Send(command); // Enviar el comando para crear un cliente

        return createResult.Match(
            customerId => Ok(customerId), // Si se creó el cliente, devolver el ID
            errors => Problem(errors) // Si hubo errores, devolverlos
        );
    }
}