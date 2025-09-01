using Application.DTOs;

public interface IArchiveService
{
    Task<ArchiveDto> ArchiveFinalDecisionAsync(FinalDecisionDto finalDecision, CancellationToken ct = default);
}
