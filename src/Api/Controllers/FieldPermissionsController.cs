using Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

//[ApiController]
//[Route("api/[controller]")]
public class FieldPermissionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;
    public FieldPermissionsController(IMediator mediator, UserManager<ApplicationUser> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }
    //[Authorize(Roles = "Admin")] // فقط المدير العام يمكنه تعديل صلاحيات الحقول
    [AllowAnonymous]

    // GET: api/FieldPermissions/{userId}
    //[HttpGet("{userId}")]
    public async Task<IActionResult> Get(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound(ApiResult.Fail("User not found", 404));

        var claims = await _userManager.GetClaimsAsync(user);
        var fieldClaims = claims.Where(c => c.Type == "FieldPermission")
                                .Select(c => {
                                    var parts = c.Value.Split(',');
                                    return parts.Select(p => {
                                        var kv = p.Split(':');
                                        return new FieldPermissionDto { Entity = kv[0], Fields = new List<string> { kv[1] } };
                                    });
                                }).SelectMany(x => x).ToList();

        return Ok(ApiResult.Ok(fieldClaims, "User field permissions", 200, HttpContext.TraceIdentifier));
    }
    [AllowAnonymous]

    // PUT: api/FieldPermissions
    //[HttpPut]
    //[Authorize(Roles = "Admin")] // فقط المدير العام يمكنه تعديل صلاحيات الحقول
    public async Task<IActionResult> Update([FromBody] UpdateFieldPermissionsRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null) return NotFound(ApiResult.Fail("User not found", 404));

        // إزالة أي صلاحيات قديمة من نفس النوع
        var existingClaims = (await _userManager.GetClaimsAsync(user))
                             .Where(c => c.Type == "FieldPermission").ToList();

        foreach (var claim in existingClaims)
            await _userManager.RemoveClaimAsync(user, claim);

        // إضافة الصلاحيات الجديدة
        foreach (var perm in request.Permissions)
        {
            var claim = new System.Security.Claims.Claim("FieldPermission", perm);
            await _userManager.AddClaimAsync(user, claim);
        }

        return Ok(ApiResult.Ok(true, "Field permissions updated", 200, HttpContext.TraceIdentifier));
    }

    [AllowAnonymous]

    // DELETE: api/FieldPermissions/{userId}
    //[HttpDelete("{userId}")]
    public async Task<IActionResult> Remove(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound(ApiResult.Fail("User not found", 404 ));

        var claims = await _userManager.GetClaimsAsync(user);
        var fieldClaims = claims.Where(c => c.Type == "FieldPermission").ToList();

        foreach (var claim in fieldClaims)
        {
            await _userManager.RemoveClaimAsync(user, claim);
        }

        return Ok(ApiResult.Ok(true, "Field permissions removed", 200, HttpContext.TraceIdentifier));
    }
}
