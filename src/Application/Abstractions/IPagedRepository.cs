using System.Linq.Expressions;

public interface IPagedRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    Task<PagedResult<TDto>> GetPagedAsync<TDto>(
        Expression<Func<TEntity, bool>>? filter = null,
        Expression<Func<TEntity, TDto>>? select = null,
        string? sortBy = null,
        bool sortDesc = false,
        int page = 1,
        int pageSize = 20,
        CancellationToken ct = default,

         params Expression<Func<TEntity, object>>[] includes);
   
}
