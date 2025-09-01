using MediatR;

public class GetApplicantsStatisticsHandler : IRequestHandler<GetApplicantsStatisticsQuery, ApplicantsStatisticsDto>
{
    private readonly IApplicantService _applicantService;

    public GetApplicantsStatisticsHandler(IApplicantService applicantService)
    {
        _applicantService = applicantService;
    }

    public async Task<ApplicantsStatisticsDto> Handle(GetApplicantsStatisticsQuery request, CancellationToken ct)
    {
        return await _applicantService.GetStatisticsAsync(ct);
    }
}