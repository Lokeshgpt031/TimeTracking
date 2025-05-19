using Microsoft.AspNetCore.Authorization;

namespace TimeTracking.Auth;

public class EmailDomainRequirement : IAuthorizationRequirement
{
    public EmailDomainRequirement(string allowedDomain)
    {
        Domain = allowedDomain;
    }
    public string Domain { get; set; }
    
}