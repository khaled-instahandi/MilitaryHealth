using Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

public class UpdateFieldPermissionsHandler : IRequestHandler<UpdateFieldPermissionsCommand, bool>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UpdateFieldPermissionsHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Handle(UpdateFieldPermissionsCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.userId);
        if (user == null) return false;

        // إزالة جميع Claims القديمة من نفس النوع
        var existingClaims = await _userManager.GetClaimsAsync(user);
        var fieldClaims = existingClaims.Where(c => c.Type == "FieldPermission").ToList();
        foreach (var claim in fieldClaims)
        {
            await _userManager.RemoveClaimAsync(user, claim);
        }

        // إضافة Claims جديدة
        foreach (var perm in request.permissions)
        {
            var value = string.Join(',', perm.Fields.Select(f => $"{perm.Entity}:{f}"));
            await _userManager.AddClaimAsync(user, new Claim("FieldPermission", value));
        }

        return true;
    }
}
