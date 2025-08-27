using System.Security.Claims;

public interface IFieldPolicyService
{
    bool IsAllowed(ClaimsPrincipal user, string entity, string field, string action); // Read | Write
}
