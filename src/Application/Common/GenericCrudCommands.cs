public record CreateEntityCommand<TEntity, TDto>(TDto Dto) : ICommand<TDto>;
public record UpdateEntityCommand<TEntity, TDto>(object Id, TDto Dto) : ICommand<TDto>;
public record DeleteEntityCommand<TEntity>(object Id) : ICommand<bool>;
