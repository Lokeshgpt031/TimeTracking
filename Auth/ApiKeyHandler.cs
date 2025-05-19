using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace TimeTracking.Auth;

public class ApiKeyOptions : AuthenticationSchemeOptions
{
    public string? DisplayMessage { get; set; }

}

public class ApiKeyHandler : AuthenticationHandler<ApiKeyOptions>
{
    
    public string[] KEYS = ["1234567890", "0987654321"];

    public ApiKeyHandler(IOptionsMonitor<ApiKeyOptions> options, ILoggerFactory logger, UrlEncoder encoder) : base(options, logger, encoder)
    {

    }


    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey(HeaderNames.Authorization))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }
        var authHeader = Request.Headers[HeaderNames.Authorization].ToString();

        string apiKey = authHeader.Split(" ")[1];
        if (KEYS.Contains(apiKey))
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, apiKey),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new GenericPrincipal(identity, null);
            var ticket = new AuthenticationTicket(principal, "APIKEY");
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
        

        return Task.FromResult(AuthenticateResult.Fail("Authentication failed"));
    }
}