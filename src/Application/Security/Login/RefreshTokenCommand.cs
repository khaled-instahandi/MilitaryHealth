using Application.DTOs.Auth;
using MediatR;

public sealed record RefreshTokenCommand(RefreshTokenRequest Request) : IRequest<LoginResponse>;
