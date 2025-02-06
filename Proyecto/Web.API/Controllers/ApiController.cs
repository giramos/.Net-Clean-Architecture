using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.API.Common.Http;

namespace Web.Api.Controller;
// Clase base para los controladores de la API
[ApiController] // Decorador para indicar que es un controlador de API
public class ApiController : ControllerBase
{
    // Método para devolver un error
    protected IActionResult Problem(List<Error> errors)
    {
        // Si no hay errores en la lista
        if (errors is null || errors.Count == 0)
        {
            return Problem(); // 500
        }
        // Si todos los errores son de validación
        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors); // 400
        }
        // Si hay errores de validación y otros tipos de errores mezclados en la lista de errores
        HttpContext.Items[HttpContextItemKeys.Errors] = errors;
        return Problem(errors[0]);
    }

    // Método para devolver un error 
    private IActionResult Problem(Error error)
    {
        // Dependiendo del tipo de error, se devuelve un código de estado HTTP diferente
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };
        return Problem(statusCode: statusCode, title: error.Description);
    }

    // Método para devolver un error de validación 
    private IActionResult ValidationProblem(List<Error> errors)
    {
        var modelState = new ModelStateDictionary(); // Diccionario de errores de validación
        foreach (var error in errors)
        {
            // Se añade un error de validación al diccionario de errores
            modelState.AddModelError(error.Code, error.Description);
        }
        return ValidationProblem(modelState);
    }
}