using Infrastructure.Identity;

public interface IFieldPermissionService
{
    bool CanRead(ApplicationUser user, string entity, string field);
    bool CanWrite(ApplicationUser user, string entity, string field);
}
