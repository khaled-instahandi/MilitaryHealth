using Application.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class GetApplicantHandler : IRequestHandler<GetApplicantQuery, ApplicantDetailsDto?>
{
    private readonly IApplicantService _applicantService;

    public GetApplicantHandler(IApplicantService applicantService)
    {
        _applicantService = applicantService;
    }

    public async Task<ApplicantDetailsDto?> Handle(GetApplicantQuery request, CancellationToken cancellationToken)
    {
        var details = await _applicantService.GetApplicantAsync(
            request.Id.ToString(),
            cancellationToken
        );

        return details;
    }
}
