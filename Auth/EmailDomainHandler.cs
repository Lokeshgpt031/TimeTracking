using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TimeTracking.Auth;

public class EmailDomainHandler : AuthorizationHandler<EmailDomainRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EmailDomainRequirement requirement)
    {
        var emailClaim = context.User.FindFirst(c => c.Type == ClaimTypes.Email);
        if (emailClaim != null)
        {
            var email = emailClaim.Value;
            if (email.EndsWith(requirement.Domain,StringComparison.InvariantCultureIgnoreCase))
            {
                context.Succeed(requirement);
            }
        }

        return Task.CompletedTask;
    }
}