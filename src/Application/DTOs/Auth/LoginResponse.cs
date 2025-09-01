using Application.DTOs.Users;

namespace Application.DTOs.Auth;

public class LoginResponse
{
    public UserDto? User { get; set; }
    public DoctorDto Doctor { get; set; }
    public IEnumerable<string> Roles { get; set; } = new List<string>();

    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;

    public DateTime AccessTokenExpires { get; set; }
    public DateTime RefreshTokenExpires { get; set; }
}
