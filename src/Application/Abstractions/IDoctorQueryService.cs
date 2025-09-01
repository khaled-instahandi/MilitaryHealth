using Application.DTOs;

public interface IDoctorQueryService
{
    Task<DoctorDto?> GetByIdAsync(int doctorId, CancellationToken ct);
}
