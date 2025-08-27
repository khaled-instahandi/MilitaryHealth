using Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class LogoutHandler : IRequestHandler<LogoutCommand, bool>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public LogoutHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Request.RefreshToken))
            throw new ArgumentException("Refresh token is required.");

        var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.RefreshToken == request.Request.RefreshToken, cancellationToken);

        if (user == null)
            return false; // أو throw new UnauthorizedAccessException("Invalid refresh token");


        // مسح الـ RefreshToken ووقت انتهاء صلاحيته
        user.RefreshToken = null;
        user.RefreshTokenExpiry = null;

        var result = await _userManager.UpdateAsync(user);

        return result.Succeeded;
    }

}
