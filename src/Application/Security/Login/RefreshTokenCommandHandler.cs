using Application.Abstractions;
using Application.DTOs.Auth;
using Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, LoginResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtTokenService _jwtService;

    public RefreshTokenCommandHandler(UserManager<ApplicationUser> userManager, IJwtTokenService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<LoginResponse> Handle(RefreshTokenCommand req, CancellationToken ct)
    {
       if (string.IsNullOrWhiteSpace(req.Request.RefreshToken))
            throw new ArgumentException("Refresh token is required");

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == req.Request.RefreshToken, ct)
                   ?? throw new UnauthorizedAccessException("Invalid refresh token");

        if (user.RefreshTokenExpiry < DateTime.UtcNow)
            throw new UnauthorizedAccessException("Refresh token expired");

        var roles = await _userManager.GetRolesAsync(user);
        var (access, refresh) = await _jwtService.GenerateTokensAsync(user, roles);

        return new LoginResponse
        {
            AccessToken = access,
            RefreshToken = refresh,
            AccessTokenExpires = DateTime.UtcNow.AddMinutes(15),
            RefreshTokenExpires = DateTime.UtcNow.AddDays(7)
        };
    }
}
