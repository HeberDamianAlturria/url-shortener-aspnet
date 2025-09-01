using FluentValidation;

namespace UrlShortener.Filters;

// This is the filter that validates the request using FluentValidation.
public class ValidateRequestFilter<TRequest> : IEndpointFilter
{
    private readonly IValidator<TRequest> _validator;

    public ValidateRequestFilter(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next
    )
    {
        var request = context.Arguments.OfType<TRequest>().First();

        var validationResult = await _validator.ValidateAsync(
            request,
            context.HttpContext.RequestAborted
        );

        if (!validationResult.IsValid)
            return TypedResults.ValidationProblem(validationResult.ToDictionary());

        return await next(context);
    }
}

// This is an extension method to add the validation filter to the route handler builder.
public static class ValidationExtensions
{
    public static RouteHandlerBuilder WithRequestValidation<TRequest>(
        this RouteHandlerBuilder builder
    )
    {
        return builder
            .AddEndpointFilter<ValidateRequestFilter<TRequest>>()
            .ProducesValidationProblem();
    }
}
