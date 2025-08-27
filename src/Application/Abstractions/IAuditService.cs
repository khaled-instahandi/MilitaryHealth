using System.Threading;
using System.Threading.Tasks;

public interface IAuditService
{
    Task LogAsync<TEntity>(TEntity entity, string action, TEntity? oldEntity = null, CancellationToken ct = default) where TEntity : class;
}
