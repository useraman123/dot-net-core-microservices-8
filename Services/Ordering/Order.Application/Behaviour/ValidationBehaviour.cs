using FluentValidation;
using MediatR;

namespace Order.Application.Behaviour;

/// <summary>
/// Enforcing the Fluent Validation 
/// TRequest is basically IRequest
/// This will collect the Fluent validations and run before handlers
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;   
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            // this will run all the validation rules one by one and returns the validation result
            var validationresult = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            // now need to check for any failures
            var failures = validationresult.SelectMany(e => e.Errors).Where(x => x != null).ToList();
            if(failures.Count != 0)
            {
                throw new ValidationException(failures);
            }
        }
        // on sucess
        return await next();
    }
}
