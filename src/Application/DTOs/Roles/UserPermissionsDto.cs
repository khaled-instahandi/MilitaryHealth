public class UserPermissionsDto
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<FieldPermissionDto> FieldPermissions { get; set; } = new();
}