using System.Reflection;
using Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Security;
using MediatR;

namespace Application.Common.Behaviors;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IUser _user;
    //private readonly IIdentityService _identityService;

    public AuthorizationBehavior(
        IUser user) => _user = user;//_identityService = identityService;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        IEnumerable<AuthorizeAttribute> authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

        if (authorizeAttributes.Any())
        {
            Guid? userId = new Guid(_user.Id ?? string.Empty);
            // Must be authenticated user
            if (userId == Guid.Empty)
            {
                throw new UnauthorizedAccessException();
            }
        }

        // User is authorized / authorization not required
        return await next();
    }
}
