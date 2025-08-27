using System.Linq.Expressions;

public record GetEntityByIdQuery<TEntity, TDto>(object Id) : IQuery<TDto?>;
public record GetEntitiesQuery<TEntity, TDto>(
    Expression<Func<TEntity, bool>>? Filter = null,
    Expression<Func<TEntity, TDto>>? Select = null,
    string? SortBy = null,
    bool SortDesc = false,
    int Page = 1,
    int PageSize = 20,
    Expression<Func<TEntity, object>>[]? Includes = null  // ✅ بدل Expressions
) : IQuery<PagedResult<TDto>>;
