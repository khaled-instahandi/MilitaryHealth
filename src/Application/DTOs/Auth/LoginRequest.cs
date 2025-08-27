using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Auth;

public sealed class LoginRequest
{
    [Required(ErrorMessage = "Username is required")]

    public string Username { get; set; } = default!;
    [Required(ErrorMessage = "Password is required")]

    public string Password { get; set; } = default!;
}
