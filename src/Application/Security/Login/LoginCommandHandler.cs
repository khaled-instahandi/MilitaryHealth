using Application.Abstractions;
using Application.DTOs.Auth;
using Application.DTOs.Users;
using Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IDoctorQueryService _doctorService;

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IJwtTokenService _jwtService;

    public LoginCommandHandler(UserManager<ApplicationUser> userManager,
                               SignInManager<ApplicationUser> signInManager,
                               IJwtTokenService jwtService, IDoctorQueryService doctorService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
        _doctorService = doctorService;
    }

    public async Task<LoginResponse> Handle(LoginCommand req, CancellationToken ct)
    {
        var user = await _userManager.FindByNameAsync(req.Request.Username)
                   ?? throw new UnauthorizedAccessException("Invalid username or password");

        var result = await _signInManager.CheckPasswordSignInAsync(user, req.Request.Password, false);
        if (!result.Succeeded)
            throw new UnauthorizedAccessException("Invalid username or password");

        var roles = await _userManager.GetRolesAsync(user);
        var (access, refresh) = await _jwtService.GenerateTokensAsync(user, roles);

        user.LastLogin = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);
        var userDto = new UserDto
        {
            UserID = user.Id,
            Username = user.UserName!,
            Email = user.Email,
            FullName=user.FullName,
            DoctorID = user.DoctorID,
            LastLogin = DateTime.UtcNow,
            PhoneNumber = user.PhoneNumber,
            Status = user.Status,

        };
        var doctorDto = user.DoctorID.HasValue 
    ? await _doctorService.GetByIdAsync(user.DoctorID.Value, ct) 
    : null;
        return new LoginResponse
        {
            User= userDto,
            Roles = roles,
            Doctor=doctorDto,
            AccessToken = access,
            RefreshToken = refresh,
            AccessTokenExpires = DateTime.UtcNow.AddMinutes(15),
            RefreshTokenExpires = DateTime.UtcNow.AddDays(7)
        };
    }
}
