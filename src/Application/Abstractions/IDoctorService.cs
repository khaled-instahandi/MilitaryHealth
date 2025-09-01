using Application.DTOs;
using Infrastructure.Persistence.Models;


public interface IDoctorService
{
    Task<DoctorDto> CreateDoctorWithUserAsync(DoctorRequest req, CancellationToken ct);
}
