using FluentValidation;
using MediatR;

namespace Ordering.Application.Common.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (!_validators.Any()) return await next();

        var validationResults = await Task.WhenAll(_validators.Select(x => x.ValidateAsync(request, cancellationToken)));

        var failures = validationResults
        .Where(r => r.Errors.Any())
        .SelectMany(r => r.Errors)
        .ToList();

        if (failures.Any()) throw new ValidationException(failures);

        return await next();
    }
}