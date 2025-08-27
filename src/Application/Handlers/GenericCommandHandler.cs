using MapsterMapper;
using MediatR;
using Application.Abstractions;

public class GenericCommandHandler<TEntity, TDto> :
    IRequestHandler<CreateEntityCommand<TEntity, TDto>, TDto>,
    IRequestHandler<UpdateEntityCommand<TEntity, TDto>, TDto>,
    IRequestHandler<DeleteEntityCommand<TEntity>, bool>
    where TEntity : class
{
    private readonly IRepository<TEntity> _repo;
    private readonly IMapper _mapper;
    private readonly IFileNumberGenerator<TEntity>? _fileNumberGenerator;

    public GenericCommandHandler(
        IRepository<TEntity> repo,
        IMapper mapper,
        IFileNumberGenerator<TEntity>? fileNumberGenerator = null)
    {
        _repo = repo;
        _mapper = mapper;
        _fileNumberGenerator = fileNumberGenerator;
    }

    public async Task<TDto> Handle(CreateEntityCommand<TEntity, TDto> request, CancellationToken ct)
    {
        var entity = _mapper.Map<TEntity>(request.Dto);

        // توليد FileNumber إذا موجود
        if (entity.GetType().Name.Contains("Applicant") && _fileNumberGenerator != null)
        {
            var prop = typeof(TEntity).GetProperty("FileNumber");
            if (prop != null && prop.CanWrite)
            {
                var fileNumber = await _fileNumberGenerator.GenerateNextAsync(ct);
                prop.SetValue(entity, fileNumber);
            }
        }

       
        await _repo.AddAsync(entity, ct);
        return _mapper.Map<TDto>(entity);
    }

    public async Task<TDto> Handle(UpdateEntityCommand<TEntity, TDto> request, CancellationToken ct)
    {
        var entity = await _repo.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException($"{typeof(TEntity).Name} not found.");

        var dto = request.Dto;

        // update only if value sent
        foreach (var prop in typeof(TDto).GetProperties())
        {
            var newValue = prop.GetValue(dto);
            if (newValue != null)
            {
                var entityProp = typeof(TEntity).GetProperty(prop.Name);
                if (entityProp != null)
                    entityProp.SetValue(entity, newValue);
            }
        }

        await _repo.UpdateAsync(entity, ct);
        return _mapper.Map<TDto>(entity);
    }


    public async Task<bool> Handle(DeleteEntityCommand<TEntity> request, CancellationToken ct)
    {
        var entity = await _repo.GetByIdAsync(request.Id, ct);
        if (entity == null) return false;

        await _repo.DeleteAsync(entity, ct);
        return true;
    }
}
