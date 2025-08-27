using Application.Common.Extensions;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text.Json;

public class Repository<TEntity> : IPagedRepository<TEntity> where TEntity : class
{
    protected readonly AppDbContext _db;
    protected readonly DbSet<TEntity> _set;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Repository(AppDbContext db, IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        _set = _db.Set<TEntity>();
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

    private string GetEntityKey(TEntity entity)
    {
        var keyProperty = _db.Model.FindEntityType(typeof(TEntity))?.FindPrimaryKey()?.Properties.FirstOrDefault();
        return keyProperty != null ? _db.Entry(entity).Property(keyProperty.Name)?.CurrentValue?.ToString() ?? "" : "";
    }

    private string[]? GetChangedColumns(TEntity oldEntity, TEntity newEntity)
    {
        var changes = typeof(TEntity)
            .GetProperties()
            .Where(p => !Equals(p.GetValue(oldEntity), p.GetValue(newEntity)))
            .Select(p => p.Name)
            .ToArray();

        return changes.Length > 0 ? changes : null;
    }

    private string? GetOldValues(TEntity oldEntity, string[]? changedColumns)
    {
        if (changedColumns == null) return null;

        var dict = changedColumns.ToDictionary(
            col => col,
            col => oldEntity.GetType().GetProperty(col)?.GetValue(oldEntity)
        );

        return JsonSerializer.Serialize(dict);
    }

    private async Task LogAuditAsync(TEntity entity, string action, TEntity? oldEntity = null)
    {
        var changedColumns = oldEntity != null ? GetChangedColumns(oldEntity, entity) : null;

        var audit = new AuditLog
        {
            UserId = CurrentUserId,
            EntityName = typeof(TEntity).Name,
            EntityKey = GetEntityKey(entity),
            Action = action,
            ChangedColumns = changedColumns != null ? string.Join(",", changedColumns) : null,
            OldValues = oldEntity != null ? GetOldValues(oldEntity, changedColumns) : null,
            NewValues = JsonSerializer.Serialize(entity),
            Timestamp = DateTime.UtcNow
        };

        await _db.Set<AuditLog>().AddAsync(audit);
    }

    public async Task AddAsync(TEntity entity, CancellationToken ct = default)
    {
        await _set.AddAsync(entity, ct);
        await LogAuditAsync(entity, "Create");
        await _db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken ct = default)
    {
        var keyProperty = _db.Model.FindEntityType(typeof(TEntity))?.FindPrimaryKey()?.Properties.FirstOrDefault();
        if (keyProperty == null)
            throw new InvalidOperationException("Entity has no primary key defined.");

        var keyValue = _db.Entry(entity).Property(keyProperty.Name).CurrentValue;
        if (keyValue == null)
            throw new InvalidOperationException("Entity key value is null.");

        // اجلب الـ oldEntity من قاعدة البيانات باستخدام FindAsync (أفضل من استخدام LINQ مع _db.Entry)
        var oldEntity = await _set.FindAsync(new object[] { keyValue }, ct);

        if (oldEntity == null)
            throw new InvalidOperationException("Entity not found in database.");

        _set.Update(entity);
        await LogAuditAsync(entity, "Update", oldEntity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken ct = default)
    {
        await LogAuditAsync(entity, "Delete", entity);
        _set.Remove(entity);
        await _db.SaveChangesAsync(ct);
    }

    public IQueryable<TEntity> Query() => _set.AsNoTracking();

    public async Task<TEntity?> GetByIdAsync(object id, CancellationToken ct = default)
        => await _set.FindAsync(new object[] { id }, ct);

    public async Task<PagedResult<TDto>> GetPagedAsync<TDto>(
        Expression<Func<TEntity, bool>>? filter,
        Expression<Func<TEntity, TDto>>? selector,
        string? sortBy,
        bool sortDesc,
        int page,
        int pageSize,
        CancellationToken ct = default,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _set;

        if (filter != null)
            query = query.Where(filter);

        if (includes != null && includes.Any())
            query = includes.Aggregate(query, (current, include) => current.Include(include));

        if (!string.IsNullOrEmpty(sortBy))
            query = query.OrderByProperty(sortBy, sortDesc);

        var totalCount = await query.CountAsync(ct);

        List<TDto> items;

        if (selector != null)
            items = await query.Select(selector).ToListAsync(ct);
        else
            items = query.Cast<TDto>().ToList(); // fallback

        return new PagedResult<TDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }
}
