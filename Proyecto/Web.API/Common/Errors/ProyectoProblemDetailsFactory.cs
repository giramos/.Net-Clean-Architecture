using System.Diagnostics;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.API.Common.Http;

namespace Web.API.Common.Errors;

// Clase que implementa la interfaz IProblemDetailsFactory para la creación de problemas
public class ProyectoProblemDetailsFactory : ProblemDetailsFactory
{
    private readonly ApiBehaviorOptions _options; // Opciones de comportamiento de la API

    public ProyectoProblemDetailsFactory(ApiBehaviorOptions options)
    {
        // Inicializamos las opciones
        this._options = options ?? throw new ArgumentNullException(nameof(options));
    }

    // Método para crear detalles de problemas de errores genéricos 
    public override ProblemDetails CreateProblemDetails(
        HttpContext httpContext, int? statusCode = null,
         string? title = null, string? type = null, string? detail = null, string? instance = null)
    {
        statusCode ??= 500; // Si el código de estado es nulo, lo establecemos a 500

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance
        };

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value); // Aplicamos los valores por defecto
        return problemDetails;
    }

    // Método para crear detalles de problemas de validación de errores 
    public override ValidationProblemDetails CreateValidationProblemDetails(HttpContext httpContext,
     ModelStateDictionary modelStateDictionary, int? statusCode = null, string? title = null,
      string? type = null, string? detail = null, string? instance = null)
    {

        if (modelStateDictionary == null)
        {
            throw new ArgumentNullException(nameof(modelStateDictionary));
        }

        statusCode ??= 400; // Si el código de estado es nulo, lo establecemos a 400

        var problemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance
        };
        if (title == null)
        {
            problemDetails.Title = title; // Establecemos el título
        }
        // Aplicamos los valores por defecto 
        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);
        return problemDetails;
    }

    // Método para aplicar los valores por defecto de los detalles del problema 
    private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
    {
        // Si el código de estado se encuentra en el mapeo de errores del cliente
        if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
        {
            problemDetails.Title ??= clientErrorData.Title; // Establecemos el título
            problemDetails.Type ??= clientErrorData.Link; // Establecemos el tipo
        }

        // Si el código de estado es 500
        var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

        if (traceId != null)
        {
            problemDetails.Extensions["traceId"] = traceId; // Establecemos el identificador de traza
        }

        // Si el contexto HTTP contiene errores 
        var errors = httpContext.Items[HttpContextItemKeys.Errors] as List<Error>;

        if (errors != null)
        {
            problemDetails.Extensions.Add("errorCodes", errors.Select(e => e.Code)); // Añadimos los códigos de error
        }
    }
}