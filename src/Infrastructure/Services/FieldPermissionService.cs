using Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

public class FieldPermissionService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FieldPermissionService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> CanReadField(string entity, string field)
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User);
        if (user == null) return false;

        var claims = await _userManager.GetClaimsAsync(user);
        return claims.Any(c => c.Type == "FieldPermission" && c.Value.Split(',').Contains($"{entity}:{field}"));
    }
}
