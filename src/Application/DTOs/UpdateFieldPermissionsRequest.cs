public class UpdateFieldPermissionsRequest
{
    public string UserId { get; set; } = string.Empty;
    public List<string> Permissions { get; set; } = new();
}