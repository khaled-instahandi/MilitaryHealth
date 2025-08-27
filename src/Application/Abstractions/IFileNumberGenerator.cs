// Application/Abstractions/IFileNumberGenerator.cs
public interface IFileNumberGenerator<TEntity>
{
    Task<string> GenerateNextAsync(CancellationToken ct = default);
}
