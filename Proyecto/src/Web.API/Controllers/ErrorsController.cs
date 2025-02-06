using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers;
// Controlador para manejar los errores de la API
public class ErrorsController : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)] // Ignorar en la documentación de Swagger
    [Route("/error")] // Ruta para manejar errores
    public IActionResult Error()
    {
        // Obtener el error de la petición      
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        return Problem();
    }
    
}