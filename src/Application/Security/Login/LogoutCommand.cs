using MediatR;

public record LogoutCommand(LogoutRequest Request) : IRequest<bool>;
