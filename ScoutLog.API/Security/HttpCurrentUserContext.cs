using System.Security.Claims;
using ScoutLog.Application.Interfaces.Security;

namespace ScoutLog.API.Security;

public class HttpCurrentUserContext(IHttpContextAccessor httpContextAccessor) : ICurrentUserContext
{
    public int UserId => GetRequiredIntClaim(ClaimTypes.NameIdentifier);

    public int ClubId => GetRequiredIntClaim("clubId");

    public string Role => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role)
        ?? throw new InvalidOperationException("Current user role claim is missing.");

    private int GetRequiredIntClaim(string claimType)
    {
        var value = httpContextAccessor.HttpContext?.User.FindFirstValue(claimType);

        return int.TryParse(value, out var claimValue)
            ? claimValue
            : throw new InvalidOperationException($"Current user claim '{claimType}' is missing or invalid.");
    }
}
