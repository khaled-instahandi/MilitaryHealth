using Application.DTOs;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

public class ArchiveService : IArchiveService
{
    private readonly AppDbContext _db;

    public ArchiveService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<ArchiveDto> ArchiveFinalDecisionAsync(FinalDecisionDto finalDecision, CancellationToken ct = default)
    {
        if (finalDecision == null)
            throw new ArgumentNullException(nameof(finalDecision));

        var applicant = await _db.Applicants
            .FirstOrDefaultAsync(a => a.FileNumber == finalDecision.ApplicantFileNumber, ct);

        if (applicant == null)
            throw new InvalidOperationException("Applicant not found for archiving.");

        var archive = new Archive
        {
            ApplicantID = applicant.ApplicantID,
            DecisionID = finalDecision.DecisionID,
            FileNumber = GenerateArchiveFileNumber(),
            ApplicantFileNumber = finalDecision.ApplicantFileNumber,
            ArchiveDate = DateTime.UtcNow,
            DigitalCopy = null
        };

        await _db.Archives.AddAsync(archive, ct);
        await _db.SaveChangesAsync(ct);

        // نرجّع DTO
        return new ArchiveDto
        {
            ApplicantID = applicant.ApplicantID,
            DecisionID = finalDecision.DecisionID,
            FileNumber = archive.FileNumber,
            ApplicantFileNumber = finalDecision.ApplicantFileNumber,
            ArchiveDate = archive.ArchiveDate,
            DigitalCopy = null
        };
    }

    private string GenerateArchiveFileNumber()
    {
        return $"ARC-{DateTime.UtcNow:yyyyMMddHHmmss}";
    }
}
