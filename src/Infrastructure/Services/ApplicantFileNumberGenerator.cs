using Infrastructure.Persistence;
using Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicantFileNumberGenerator : IFileNumberGenerator<Applicant>
{
    private readonly AppDbContext _db;
    public ApplicantFileNumberGenerator(AppDbContext db) => _db = db;

    public async Task<string> GenerateNextAsync(CancellationToken ct = default)
    {
        var last = await _db.Set<Applicant>()
                            .OrderByDescending(a => a.ApplicantID)
                            .FirstOrDefaultAsync(ct);

        var nextNumber = 1;
        if (last != null && !string.IsNullOrWhiteSpace(last.FileNumber) && last.FileNumber.Length > 1)
        {
            var numeric = last.FileNumber.Substring(1);
            if (int.TryParse(numeric, out var n)) nextNumber = n + 1;
        }

        return $"F{nextNumber:D7}";
    }
}
