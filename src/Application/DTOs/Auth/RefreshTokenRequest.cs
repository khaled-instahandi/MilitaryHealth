﻿namespace Application.DTOs.Auth;

public sealed class RefreshTokenRequest
{
    public string RefreshToken { get; set; } = default!;
}
