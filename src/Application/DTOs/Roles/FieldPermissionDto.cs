public class FieldPermissionDto
{
    public string Entity { get; set; } = string.Empty;
    public List<string> Fields { get; set; } = new();
    public string PermissionType { get; set; } = "Read"; // Read / Write
}