using MediatR;

public record UpdateFieldPermissionsCommand(string userId, List<FieldPermissionDto> permissions) : IRequest<bool>;
