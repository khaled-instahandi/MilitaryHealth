using Application.DTOs.Auth;
using MediatR;

public sealed record LoginCommand(LoginRequest Request) : IRequest<LoginResponse>;
