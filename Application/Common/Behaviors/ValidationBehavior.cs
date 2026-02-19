using Domain.Common.Enums;
using Domain.Model.ReturnEntity;
using FluentValidation;
using MediatR;

namespace Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, ct)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
            var mainError = failures.First();
            string errorMessage = mainError.ErrorMessage;
            if (typeof(TResponse).IsGenericType &&
           typeof(TResponse).GetGenericTypeDefinition() == typeof(TResult<>))
            {
                return (TResponse)typeof(TResponse)
                    .GetMethod(nameof(TResult.FailedOperation))!
                    .Invoke(null, new object[] { ErrorCode.BadRequest, errorMessage })!;
            }
            return (TResponse)(object)TResult.FailedOperation(ErrorCode.BadRequest);
        }
        return await next();
    }
}
