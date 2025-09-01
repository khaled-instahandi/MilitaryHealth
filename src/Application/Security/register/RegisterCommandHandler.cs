using Application.Abstractions;
using Application.DTOs.Auth;
using Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtTokenService _jwtService;

    public RegisterCommandHandler(UserManager<ApplicationUser> userManager,
                                  IJwtTokenService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }


    public async Task<RegisterResponse> Handle(RegisterCommand req, CancellationToken ct)
    {
        // Check if username exists
        var existing = await _userManager.FindByNameAsync(req.Request.Username);
        if (existing is not null)
            throw new InvalidOperationException("Username already exists.");

        var user = new ApplicationUser
        {
            UserName = req.Request.Username,
            Email = req.Request.Email,
            FullName = req.Request.FullName
        };

        var result = await _userManager.CreateAsync(user, req.Request.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Registration failed: {errors}");
        }

        // Assign default role
        //await _userManager.AddToRoleAsync(user, "Receptionist");

        // Generate JWT
        var roles = await _userManager.GetRolesAsync(user);
        var (access, refresh) = await _jwtService.GenerateTokensAsync(user, roles);

        return new RegisterResponse
        {
            AccessToken = access,
            RefreshToken = refresh,
            AccessTokenExpires = DateTime.UtcNow.AddMinutes(15),
            RefreshTokenExpires = DateTime.UtcNow.AddDays(7)
        };
    }
}
