using MapsterMapper;
using MediatR;
using System.Linq.Expressions;

public class GenericQueryHandler<TEntity, TDto> :
    IRequestHandler<GetEntityByIdQuery<TEntity, TDto>, TDto?>,
    IRequestHandler<GetEntitiesQuery<TEntity, TDto>, PagedResult<TDto>>
    where TEntity : class
{
    private readonly IPagedRepository<TEntity> _repo;
    private readonly IMapper _mapper;

    public GenericQueryHandler(IPagedRepository<TEntity> repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<TDto?> Handle(GetEntityByIdQuery<TEntity, TDto> request, CancellationToken ct)
    {
        var entity = await _repo.GetByIdAsync(request.Id, ct);
        return entity == null ? default : _mapper.Map<TDto>(entity);
    }

    public async Task<PagedResult<TDto>> Handle(GetEntitiesQuery<TEntity, TDto> request, CancellationToken ct)
    {
        // افترض عندك قائمة الـ includes
        var includes = request.Includes ?? Array.Empty<Expression<Func<TEntity, object>>>();

        return await _repo.GetPagedAsync(
            request.Filter,
            e => _mapper.Map<TDto>(e),
            request.SortBy,
            request.SortDesc,
            request.Page,
            request.PageSize,
            ct,
            includes
        );
    }

}
