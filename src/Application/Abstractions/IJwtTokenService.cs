using Infrastructure.Identity;
using System.Security.Claims;

namespace Application.Abstractions
{
    public interface IJwtTokenService
    {
        Task<(string AccessToken, string RefreshToken)> GenerateTokensAsync(ApplicationUser user, IList<string> roles);

        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);

        string GenerateRefreshToken();
    }
}
