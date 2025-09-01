using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/UserRoles")]
[Authorize(Roles = "Admin,Receptionist,Doctor,Supervisor,Diwan")]
public class UserRolesController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public UserRolesController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    [AllowAnonymous]
    // إضافة دور لمستخدم
    [HttpPost("{userId}/roles")]
    public async Task<IActionResult> AddRole(string userId, [FromBody] string role)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound(ApiResult.Fail("User not found", 404));

        if (!await _roleManager.RoleExistsAsync(role))
        {
            await _roleManager.CreateAsync(new IdentityRole<int> { Name = role });
        }

        var result = await _userManager.AddToRoleAsync(user, role);
        if (!result.Succeeded)
            return BadRequest(ApiResult.Fail("Failed to assign role", 400));

        return Ok(ApiResult.Ok(true, $"Role '{role}' added to user.", 200, HttpContext.TraceIdentifier));
    }

    // إزالة دور من مستخدم
    [HttpDelete("{userId}/roles/{role}")]
    public async Task<IActionResult> RemoveRole(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound(ApiResult.Fail("User not found", 404));

        var result = await _userManager.RemoveFromRoleAsync(user, role);
        if (!result.Succeeded)
            return BadRequest(ApiResult.Fail("Failed to remove role", 400));

        return Ok(ApiResult.Ok(true, $"Role '{role}' removed from user.", 200, HttpContext.TraceIdentifier));
    }


    // استعراض كل أدوار المستخدم
    [HttpGet("{userId}/roles")]
    public async Task<IActionResult> GetRoles(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound(ApiResult.Fail("User not found", 404));

        var roles = await _userManager.GetRolesAsync(user);
        return Ok(ApiResult.Ok(roles, "User roles fetched", 200, HttpContext.TraceIdentifier));
    }
    // GET: api/FieldPermissions/Al
    [HttpGet("getAll")]
    public IActionResult GetAllRoles()
    {
        var roles = _roleManager.Roles.Select(r => r.Name).ToList();

        return Ok(ApiResult.Ok(
            roles,
            "Fetched all roles successfully",
            200,
            HttpContext.TraceIdentifier
        ));
    }
    // l
    //[Authorize(Roles = "Admin")] // فقط المدير أو المسؤول
    //public async Task<IActionResult> GetAllUsersPermissions()
    //{
    //    // جلب كل المستخدمين
    //    var users = _userManager.Users.ToList();
    //    var result = new List<UserPermissionsDto>();

    //    foreach (var user in users)
    //    {
    //        var claims = await _userManager.GetClaimsAsync(user);
    //        var fieldClaims = claims
    //            .Where(c => c.Type == "FieldPermission")
    //            .Select(c =>
    //            {
    //                var parts = c.Value.Split(':'); // Entity:Field:PermissionType
    //                return new FieldPermissionDto
    //                {
    //                    Entity = parts[0],
    //                    Fields = new List<string> { parts[1] },
    //                    PermissionType = parts.Length > 2 ? parts[2] : "Read"
    //                };
    //            })
    //            .ToList();

    //        result.Add(new UserPermissionsDto
    //        {
    //            UserId = user.Id.ToString(),
    //            UserName = user.UserName,
    //            Email = user.Email,
    //            FieldPermissions = fieldClaims
    //        });
    //    }

    //    return Ok(ApiResult.Ok(result, "All users with permissions", 200, HttpContext.TraceIdentifier));
    //}
}
