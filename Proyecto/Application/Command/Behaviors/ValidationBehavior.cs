using ErrorOr;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Application.Command.Behaviors
{
    // clase que implementa la interfaz IPipelineBehavior
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse> // TRequest debe ser IRequest<TResponse>
    where TResponse : IErrorOr  // TResponse debe ser IErrorOr

    {
        // campo de solo lectura que almacena un validador de tipo TRequest
        private readonly IValidator<TRequest>? _validator;

        public ValidationBehavior(IValidator<TRequest>? validator = null)
        {
            _validator = validator;
        }

        // método que maneja la solicitud
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (_validator is null)
            {
                return await next(); // si el validador es nulo, se llama al siguiente manejador
            }

            // se valida la solicitud
            var validatorResult = await _validator.ValidateAsync(request, cancellationToken);

            if (validatorResult.IsValid)
            {
                return await next(); // si la solicitud es válida, se llama al siguiente manejador
            }
            // si la solicitud no es válida, se crea una lista de errores
            var errors = validatorResult.Errors
                .ConvertAll(validationFailure => Error.Validation(
                    validationFailure.PropertyName, // se obtiene el nombre de la propiedad
                    validationFailure.ErrorMessage // se obtiene el mensaje de error
                ));

            return (dynamic)errors; // se retorna la lista de errores como un objeto dinámico
        }
    }
}