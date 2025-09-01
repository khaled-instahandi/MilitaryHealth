public interface IApplicantService
{
    Task<ApplicantsStatisticsDto> GetStatisticsAsync(CancellationToken ct);
    Task<ApplicantDetailsDto?> GetApplicantDetailsAsync(string id, CancellationToken ct = default);
    Task<ApplicantDetailsDto?> GetApplicantAsync(string id, CancellationToken ct = default);



}
