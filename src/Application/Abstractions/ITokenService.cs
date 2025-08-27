using Infrastructure.Identity;

public interface ITokenService
{
    string GenerateAccessToken(ApplicationUser user);
    UserRefreshToken GenerateRefreshToken();
    Task SaveRefreshTokenAsync(int userId, UserRefreshToken refreshToken);
    Task<UserRefreshToken?> GetValidRefreshTokenAsync(int userId, string token);
}