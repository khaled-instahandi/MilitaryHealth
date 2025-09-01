using Application.DTOs;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

public class DoctorQueryService : IDoctorQueryService
{
    private readonly AppDbContext _db;

    public DoctorQueryService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<DoctorDto?> GetByIdAsync(int doctorId, CancellationToken ct)
    {
        return await _db.Doctors
            .Where(d => d.DoctorID == doctorId)
            .Select(d => new DoctorDto
            {
                DoctorID = d.DoctorID,
                FullName = d.FullName,
                SpecializationID = d.SpecializationID,
                ContractTypeID = d.ContractTypeID,
                Code = d.Code,

            })
            .FirstOrDefaultAsync(ct);
    }
}
