using Infrastructure.Persistence;
using Infrastructure.Persistence.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

public class AuditService : IAuditService
{
    private readonly AppDbContext _db;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuditService(AppDbContext db, IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
    }

    private int? CurrentUserId
    {
        get
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null) return null;

            var claim = user.FindFirst("sub")
                        ?? user.FindFirst(ClaimTypes.NameIdentifier)
                        ?? user.FindFirst("id");

            return claim != null && int.TryParse(claim.Value, out var id) ? id : null;
        }
    }

    private string GetEntityKey<TEntity>(TEntity entity) where TEntity : class
    {
        var keyProperty = _db.Model.FindEntityType(typeof(TEntity))?.FindPrimaryKey()?.Properties.FirstOrDefault();
        return keyProperty != null ? _db.Entry(entity).Property(keyProperty.Name)?.CurrentValue?.ToString() ?? "" : "";
    }

    private string? GetChangedColumns<TEntity>(TEntity? oldEntity, TEntity newEntity)
    {
        if (oldEntity == null) return null;

        var changes = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => !Equals(p.GetValue(oldEntity), p.GetValue(newEntity)))
            .Select(p => p.Name)
            .ToArray();

        return changes.Length > 0 ? string.Join(",", changes) : null;
    }

    private string? GetOldValues<TEntity>(TEntity? oldEntity, string[]? changedColumns)
    {
        if (oldEntity == null || changedColumns == null) return null;

        var dict = changedColumns.ToDictionary(
            col => col,
            col => oldEntity.GetType().GetProperty(col)?.GetValue(oldEntity)
        );

        return JsonSerializer.Serialize(dict);
    }

    public async Task LogAsync<TEntity>(TEntity entity, string action, TEntity? oldEntity = null, CancellationToken ct = default) where TEntity : class
    {
        var changedColumns = oldEntity != null ? GetChangedColumns(oldEntity, entity)?.Split(',') : null;

        var audit = new AuditLog
        {
            UserId = CurrentUserId,
            EntityName = typeof(TEntity).Name,
            EntityKey = GetEntityKey(entity),
            Action = action,
            ChangedColumns = changedColumns != null ? string.Join(",", changedColumns) : null,
            OldValues = GetOldValues(oldEntity, changedColumns),
            NewValues = JsonSerializer.Serialize(entity),
            Timestamp = DateTime.UtcNow
        };

        await _db.Set<AuditLog>().AddAsync(audit, ct);
        await _db.SaveChangesAsync(ct);
    }
}
