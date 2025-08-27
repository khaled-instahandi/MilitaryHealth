using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class ApplicationUser : IdentityUser<int>
{
    public string FullName { get; set; } = default!;
    //public string? Role { get; set; }            // احتفاظ بالقيمة القديمة إن لزم
    public int? DoctorID { get; set; }
    //public string? Permissions { get; set; }
    public string? Status { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }

    public DateTime? LastLogin { get; set; }
}
