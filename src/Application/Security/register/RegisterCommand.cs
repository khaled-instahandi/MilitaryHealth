using Application.DTOs.Auth;
using MediatR;

public sealed record RegisterCommand(RegisterRequest Request) : IRequest<RegisterResponse>;
